

using Cooklee.Core.Helpers;
using Cooklee.Data.Service.Contract;

namespace Cooklee.API.Exetensions
{
    public static class MailServiceExtension
    {
        public static IServiceCollection AddMailServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailSetting>(configuration.GetSection("MailSetting"));
            services.AddTransient<IEmailService, EmailSetting>();
            return services;
        }
    }
}
