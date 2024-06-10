using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using System.Runtime.CompilerServices;

namespace Cooklee.API.Exetensions
{
    public static class AccountServiceExtension
    {
        public static IServiceCollection AddAccountServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            }).AddCookie()
              .AddGoogle(options =>
              {
                  IConfiguration GoogleAuthentication = configuration.GetSection("Authentication:Google");
                  options.ClientId = GoogleAuthentication["ClientId"];
                  options.ClientSecret = GoogleAuthentication["ClientSecret"];
                  options.CallbackPath = "/signin-google";  // Ensure this matches the URI registered in Google API Console
              });

            return services;
        }
    }

}
