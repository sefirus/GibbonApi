using DataAccess;
using FluentValidation;
using FluentValidation.AspNetCore;
using Host;
using Host.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using WebApi.Controllers;
using WebApi.Validators.SchemaValidators;

var builder = WebApplication.CreateBuilder(args);
IdentityModelEventSource.ShowPII = true; //Add this line

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddApplicationMappers();
builder.Services
    .AddControllers()
    .AddApplicationPart(typeof(WorkspaceController).Assembly);
builder.Services.ConfigureSwagger();
builder.Services.ConfigureWorkspacePermissionsAuth();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateSchemaObjectViewModelValidator>();
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