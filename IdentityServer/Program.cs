using IdentityServer;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseIdentityServer();

app.MapControllers();

app.Run();