using Core.Entities;
using DataAccess;
using IdentityModel;
using IdentityServer.Services;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer;

public static class StartupExtensions
{
    public static void AddServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<GibbonDbContext>(opts => 
            opts.UseNpgsql(config.GetConnectionString("DefaultConnection")));

        services.AddIdentity<User, IdentityRole<Guid>>(opts =>
            {
                opts.Password.RequireDigit = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<GibbonDbContext>()
            .AddDefaultTokenProviders();

        var assembly = typeof(Program).Assembly.GetName().Name;

        services.AddIdentityServer()
            .AddDeveloperSigningCredential()
            .AddOperationalStore(opts =>
            {
                opts.ConfigureDbContext = builder => builder.UseNpgsql(config.GetConnectionString("DefaultConnection"),
                    opt => opt.MigrationsAssembly(assembly));
            })
            .AddInMemoryIdentityResources(GetIdentityResources())
            .AddInMemoryApiResources(GetApiResources())
            .AddInMemoryApiScopes(GetApiScopes())
            .AddInMemoryClients(GetClients())
            .AddAspNetIdentity<User>();

        services.AddScoped<IProfileService, ProfileService>();
    }
    
    public static IEnumerable<IdentityResource> GetIdentityResources()
        => new List<IdentityResource>()
        {
            new IdentityResources.OpenId(),
            new IdentityResource(
                name: "profile",
                userClaims: new []{"name"},
                displayName: "Profile data about user")
        };

    public static IEnumerable<ApiResource> GetApiResources()
        => new List<ApiResource>()
        {
            new ApiResource("GibbonApi", "Gibbon API")
            {
                Scopes = {"apiAccess"}
            } 
        };
    
    public static IEnumerable<ApiScope> GetApiScopes()
        =>  new[]
        {
            new ApiScope("apiAccess", "Access Gibbon Api")
        };

    public static IEnumerable<Client> GetClients()
        => new[]
        {
            // dev client that has access to all scopes of api (for tests in postman etc.)
            new Client()
            {
                RequireConsent = false,
                ClientId = "postman_client",
                ClientName = "Postman Client",
                AllowedScopes = {"apiAccess", "openid", "profile"},
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowOfflineAccess = true, // enable refresh tokens
                RefreshTokenUsage = TokenUsage.OneTimeOnly,
                ClientSecrets = {new Secret("tests_client_secret".ToSha256())},
                AccessTokenLifetime = 6000,
            },
            new Client()
            {
                RequireConsent = false,
                ClientId = "angular_client",
                ClientName = "Angular Client",
                AllowedScopes = {"apiAccess", "openid", "profile"},
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowOfflineAccess = true, // enable refresh tokens
                RefreshTokenUsage = TokenUsage.OneTimeOnly,
                ClientSecrets = {new Secret("angular_client_secret".ToSha256())},
                AccessTokenLifetime = 6000,
            }
        };
}