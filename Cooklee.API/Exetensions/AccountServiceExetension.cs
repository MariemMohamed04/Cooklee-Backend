using Microsoft.AspNetCore.Authentication.Google;
using System.Runtime.CompilerServices;

namespace Cooklee.API.Exetensions
{
    public static class AccountServiceExtension
    {
        public static IServiceCollection AddAccountServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            }).AddGoogle(options =>
            {
                IConfiguration GoogleAuthentication = configuration.GetSection("Authentication:Google");
                options.ClientId = GoogleAuthentication["ClientId"];
                options.ClientSecret = GoogleAuthentication["ClientSecret"];
            });

            return services;
        }
    }
}
