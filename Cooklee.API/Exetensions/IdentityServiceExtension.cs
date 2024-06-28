using Cooklee.Data.Entities.Identity;
using Cooklee.Data.Service.Contract;
using Cooklee.Infrastructure.Data;
using Cooklee.Service.Services;
using Microsoft.AspNetCore.Identity;

namespace Cooklee.API.Exetensions
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAuthService), typeof(AuthService));
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
            })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<CookleeDbContext>();

            //// Schema = Bearer
            //services.AddAuthentication("Bearer")
            //    .AddJwtBearer(options =>
            //    {
            //        options.ForwardSignIn
            //    }).AddJwtBearer("Bearer02", options =>
            //    {

            //    });
            return services;
        }
    }
}
