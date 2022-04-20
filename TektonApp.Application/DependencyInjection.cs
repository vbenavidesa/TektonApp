using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TektonApp.Common.Constants;

namespace TektonApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AppSettingsConstants.SetConfiguration(configuration);
            return services;
        }
    }
}
