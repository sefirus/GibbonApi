using IdentityServer;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();

var config = builder.Configuration;
builder.Services.AddServices(config);

string[] origins = builder.Configuration["AllowedOrigins"]?
    .Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries) 
    ?? Array.Empty<string>();
builder.Services.AddCors(corsOptions =>
    {
        corsOptions
            .AddDefaultPolicy(corsPolicyBuilder => 
                corsPolicyBuilder
                    .WithOrigins(origins)
                    .AllowAnyHeader()
                    .AllowAnyMethod());
    }
);

if (!builder.Environment.IsDevelopment())
{
    builder.Services.Configure<ForwardedHeadersOptions>(options =>
    {
        options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    });
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{ 
    app.UseHttpsRedirection();
    app.UseForwardedHeaders();
}
else
{
    app.UseForwardedHeaders();
}

app.UseCors();

app.UseAuthorization();

app.UseIdentityServer();

app.MapControllers();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080"; // Default to 8080 if PORT not set
app.Urls.Add($"http://*:{port}");

app.Run();
