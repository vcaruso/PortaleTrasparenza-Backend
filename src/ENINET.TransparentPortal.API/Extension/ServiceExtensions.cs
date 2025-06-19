using ENINET.TransparentPortal.API.Services.AuthService;
using ENINET.TransparentPortal.Repository;
using ENINET.TransparentPortal.Repository.Contract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using System.Security.Claims;



namespace ENINET.TransparentPortal.API.Extension;

public static class ServiceExtensions
{
    public static void ConfigureRepositoryManager(this IServiceCollection services) => services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static IServiceCollection ConfigureAzureADAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(options =>
            {
                configuration.Bind("AzureAd", options);
                options.TokenValidationParameters.NameClaimType = "name";
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {

                        if (context.Principal != null)
                        {

                            var userid = context.Principal.Claims.Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");
                            if (userid != null && userid.Any())
                            {
                                var auth = services.BuildServiceProvider().GetRequiredService<IAuthService>();
                                var groups = auth.GetUserGroup(userid.First().Value);
                                var authorizedSites = auth.GetAuthorizedSites(userid.First().Value);
                                IEnumerable<System.Security.Claims.Claim> newClaims = context.Principal.Claims;
                                if (groups.Length > 0)
                                {

                                    foreach (var group in groups)
                                    {
                                        newClaims = newClaims.Append(new Claim(ClaimTypes.Role, group));


                                    }

                                }
                                // Permessi
                                foreach (var perm in auth.GetUserPermission(userid.First().Value))
                                {
                                    newClaims = newClaims.Append(new Claim(ClaimTypes.Role, perm));
                                }
                                // Siti Autorizzati
                                if (authorizedSites.Length > 0)
                                {
                                    foreach (var site in authorizedSites)
                                    {
                                        newClaims = newClaims.Append(new Claim("TransparentSites", site));
                                    }

                                }
                                context.Principal = new ClaimsPrincipal(new ClaimsIdentity(newClaims));



                            }



                        }


                        return Task.CompletedTask;
                    }
                };

            }, options => { configuration.Bind("AzureAd", options); });

        services.AddAuthorization(config =>
        {

            config.AddPolicy("AuthZPolicy", policyBuilder =>
                policyBuilder.Requirements.Add(new ScopeAuthorizationRequirement() { RequiredScopesConfigurationKey = $"AzureAd:Scopes" }));

            //config.AddPolicy("LabvantageViewer", policyBuilder => policyBuilder.RequireClaim(ClaimTypes.Role, new string[] { ApplicationPermissionConfiguration.LabvantgeViewer, ApplicationPermissionConfiguration.LabvantageBullettin }));
            /*
            var groupPermission = new Dictionary<string, string[]>
            {
                // Administrators
                {
                    ApplicationGroupConfiguration.Administrators,new string[]{
                            ApplicationPermissionConfiguration.AddUser,
                            ApplicationPermissionConfiguration.DeleteUser,
                            ApplicationPermissionConfiguration.ApplicationUsersManage,

                }},
                // Contributors
                {
                    ApplicationGroupConfiguration.Contributors, new string[] {

                    }
                }


            };


            foreach (var group in groupPermission)
            {
                config.AddPolicy(group.Key, policyBuilder => policyBuilder.RequireClaim(ClaimTypes.Role, group.Value));
            }
            */



        });

        return services;
    }


}

