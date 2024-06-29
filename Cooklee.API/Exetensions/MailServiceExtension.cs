

using Cooklee.Core.Helpers;
using Cooklee.Data.Service.Contract;
using Cooklee.Service.Services;

namespace Cooklee.API.Exetensions
{
    public static class MailServiceExtension
    {
        public static IServiceCollection AddMailServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailSetting>(configuration.GetSection("MailSetting"));
            return services;
        }
    }
}
