
using Cooklee.API.Exetensions;
using Cooklee.API.Middlewares;
using Cooklee.Data.Entities.Identity;
using Cooklee.Data.Service.Contract;
using Cooklee.Infrastructure.Data;
using Cooklee.Infrastructure.DataSeed;
using Cooklee.Infrastructure.Repositories;
using Cooklee.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using static Cooklee.Data.Repository.Contract.IHomePageMealsRep;
using static Cooklee.Service.Services.HomePageMealsService;

namespace Cooklee.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;


            builder.Services.AddControllers();
            builder.Services.AddSwaggerServices();

            #region DbContext
            builder.Services.AddDbContext<CookleeDbContext>(op =>
            {
                op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            #endregion

            #region Redis
            builder.Services.AddSingleton<IConnectionMultiplexer>(option =>
            {
                var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(configuration);
            });
            #endregion

            builder.Services.AddControllers().AddNewtonsoftJson(op=>op.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            builder.Services.AddApplicationServices();
            builder.Services.AddIdentityServices();
            builder.Services.AddAccountServices(configuration);
            builder.Services.AddMailServices(configuration);

            #region CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", options =>
                {
                    options
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
                });
            });
            #endregion
            // Register the repositories
            builder.Services.AddScoped<IMealsRepo, HomePageMealsRepo>();

            // Register the services
            builder.Services.AddScoped<IHomePageMealsService, MealsService>();
            builder.Services.AddSingleton<IEmailService>(provider =>
            new EmailService(
                builder.Configuration["Email:SmtpServer"],
                int.Parse(builder.Configuration["Email:SmtpPort"]),
                builder.Configuration["Email:SmtpUser"],
                builder.Configuration["Email:SmtpPass"]
            ));

            var app = builder.Build();

            #region Images
            //var staticPath = Path.Combine(Environment.CurrentDirectory, "Images");
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(staticPath),
            //    RequestPath = "/Images"
            //});
            #endregion

            #region Scope
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var _dbContext = services.GetRequiredService<CookleeDbContext>();
                var _manager = services.GetRequiredService<UserManager<AppUser>>();
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger<Program>();

                try
                {
                    await _dbContext.Database.MigrateAsync();
                    var _userManager = services.GetRequiredService<UserManager<AppUser>>();
                    await AppIdentityDbContextDataSeed.SeedUserAsync(_userManager);
                    await CookleeContextSeed.SeedAsync(_dbContext);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error has occurred during applying the migration.");
                }
                finally
                {
                    scope.Dispose();
                }
            }
            #endregion

            #region Middleware
            app.UseMiddleware<ExceptionMiddleware>();
            #endregion

            if (app.Environment.IsDevelopment())
            {
                app.UseswaggerMiddlewares();
            }

            #region CORS
            app.UseCors("MyPolicy");
            #endregion

            // Redirect
            // Static formatting kestral will return the value.
            app.UseStatusCodePagesWithRedirects("errors/{0}");

            app.UseHttpsRedirection();

            app.MapControllers();

            app.UseAuthentication();

            app.UseAuthorization();

            await app.RunAsync();
        }
    }
}
