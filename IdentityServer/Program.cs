using IdentityServer;

var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.UseIdentityServer();

app.MapControllers();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080"; // Default to 8080 if PORT not set
app.Urls.Add($"http://*:{port}");

app.Run();