using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TektonApp.Application.Common.Interfaces;
using TektonApp.Infrastructure.Extensions;
using TektonApp.Infrastructure.Persistence.Extensions;
using TektonApp.Infrastructure.Persistence.Factories;

namespace TektonApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITektonDbContextBuilder, TektonDbContextBuilder>();
            services.AddScoped(provider => provider.GetTektonDbContext());


            services.RegisterApplicationServices(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
