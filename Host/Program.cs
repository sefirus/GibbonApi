using System.Text.Json.Serialization;
using DataAccess;
using FluentValidation;
using FluentValidation.AspNetCore;
using Host;
using Host.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Serilog;
using WebApi.Controllers;
using WebApi.Validators.SchemaValidators;

var builder = WebApplication.CreateBuilder(args);
IdentityModelEventSource.ShowPII = true; //Add this line

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddApplicationServices();
builder.Services.AddApplicationMappers();
builder.Services
    .AddControllers()
    .AddApplicationPart(typeof(WorkspaceController).Assembly)
    .AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);
builder.Services.ConfigureSwagger();
builder.Services.ConfigureWorkspacePermissionsAuth();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddLogger(connectionString);
builder.Services.AddValidatorsFromAssemblyContaining<CreateSchemaObjectViewModelValidator>();
builder.Services.AddDbContext<GibbonDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});
builder.Host.UseSerilog(); // <-- Add this line
builder.Services.AddCors(corsOptions =>
    {
        corsOptions
            .AddDefaultPolicy(corsPolicyBuilder => 
                corsPolicyBuilder
                    .WithOrigins("http://localhost:4200", "https://localhost:5001", "http://localhost:5283")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

//app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080"; // Default to 8080 if PORT not set
app.Urls.Add($"http://*:{port}");

app.Run();
