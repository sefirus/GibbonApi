using IdentityServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var config = builder.Configuration;
builder.Services.AddServices(config);
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

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.UseIdentityServer();

app.MapControllers();

app.Run();