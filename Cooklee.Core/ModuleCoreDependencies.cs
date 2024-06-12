using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Core
{
	public static class ModuleCoreDependencies
	{
		public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
		{
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
			return services;
		}
	}
}
