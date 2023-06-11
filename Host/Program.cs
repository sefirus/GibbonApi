using System.Security.Claims;
using DataAccess;
using Host;
using Host.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using WebApi.Controllers;

var builder = WebApplication.CreateBuilder(args);
IdentityModelEventSource.ShowPII = true; //Add this line

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services
    .AddControllers()
    .AddApplicationPart(typeof(WorkspaceController).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer token **_only_**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer", // must be lowercase
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, Array.Empty<string>() }
    });
});

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:7030";
        options.Audience = "GibbonApi";
        options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
        options.Events = new JwtBearerEvents()
        {
            OnTokenValidated = async context =>
            {
                var dbContext = context.HttpContext.RequestServices.GetRequiredService<GibbonDbContext>();

                var routeData = context.HttpContext.GetRouteData();
                var workspaceIdRouteDate = routeData?.Values["workspaceId"];

                if (workspaceIdRouteDate == null 
                    || !Guid.TryParse(workspaceIdRouteDate.ToString(), out var workspaceId))
                {
                    return;
                }
                var userIdClaim = context.Principal.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return;
                }

                if(!Guid.TryParse(userIdClaim.Value, out var userId))
                {
                   return; 
                }
                
                var userPermissionOnWorkspace = await dbContext.WorkspacePermissions
                    .Where(wp => wp.UserId == userId
                                 && wp.WorkspaceId == workspaceId)
                    .Select(wp => wp.WorkspaceRole.Name)
                    .FirstOrDefaultAsync();
                if (userPermissionOnWorkspace == null)
                {
                    context.Fail("Unauthorized");
                    return;
                }

                var workspaceRoleClaim = new Claim(ClaimTypes.Role, userPermissionOnWorkspace);

                ((ClaimsIdentity)context.Principal.Identity).AddClaims(new[] { workspaceRoleClaim });
            }
        };

    });

builder.Services.AddDbContext<GibbonDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();