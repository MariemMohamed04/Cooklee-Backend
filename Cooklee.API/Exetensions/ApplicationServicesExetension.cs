using Cooklee.API.Errors;
using Cooklee.Core;
using Cooklee.Core.Helpers;
using Cooklee.Data.Repository.Contract;
using Cooklee.Infrastructure.Repositories;
using Cooklee.Service.Abstracts;
using Cooklee.Service.Implemetaions;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Cooklee.API.Exetensions
{
    public static class ApplicationServicesExetension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            //services.AddScoped(typeof(IUserRepository<>), typeof(UserRepository<>));
            services.AddScoped<IMealRepository, MealRepository>();
            services.AddScoped<IMealService, MealService>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddCoreDependencies();

			services.Configure<ApiBehaviorOptions>(options =>
            {
                // State for model so we're changing its default state from invalid to this
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    // get dictionary ModelState key : name of parameter, value : array of errors
                    // 
                    var errors = actionContext.ModelState
                        .Where(p => p.Value.Errors.Count() > 0) // key value pair, not valid state parameters
                        .SelectMany(p => p.Value.Errors) // from each parameter array of errors, el error 3obara 3n object
                        .Select(e => e.ErrorMessage)
                        .ToArray(); // convert messages into array

                    var validationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    // BadRequest() won't work so we are talknig to helper methods
                    return new BadRequestObjectResult(validationErrorResponse);
                };
            });
            return services;
        }
    }
}
