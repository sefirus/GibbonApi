using System.Security.Claims;
using Application.Mappers;
using Application.Services;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using Core.ViewModels.Schema;
using DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NpgsqlTypes;
using Serilog;
using Serilog.Sinks.PostgreSQL;
using Serilog.Sinks.PostgreSQL.ColumnWriters;
using Log = Serilog.Log;

namespace Host;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddTransient<ICurrentUserService, CurrentUserService>();
        services.AddTransient<IWorkspaceService, WorkspaceService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<ISchemaService, SchemaService>();
    }

    public static void AddApplicationMappers(this IServiceCollection services)
    {
        services.AddTransient<IVmMapper<Dictionary<string, SchemaFieldViewModel>, List<SchemaField>>, SchemaObjectFieldsMapper>();
        services.AddTransient<IVmMapper<SchemaObject, SchemaObjectViewModel>, SchemaObjectToVmMapper>();
    }

    public static void AddLogger(this IServiceCollection services, string connectionString)
    {
        var columnOptions = new Dictionary<string, ColumnWriterBase>
        {
            { "Message", new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
            { "Level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
            { "Timestamp", new TimestampColumnWriter(NpgsqlDbType.TimestampTz) },
            { "Exception", new ExceptionColumnWriter(NpgsqlDbType.Text) },
        };

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Error()
            .WriteTo.PostgreSQL(
                connectionString: connectionString,
                tableName: "Logs",
                columnOptions: columnOptions,
                batchSizeLimit:1, needAutoCreateTable: true)
            .CreateLogger();
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
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
    }

    public static void ConfigureWorkspacePermissionsAuth(this IServiceCollection services)
    {
        services
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
    }
}