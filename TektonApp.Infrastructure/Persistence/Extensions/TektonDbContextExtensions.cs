using Microsoft.Extensions.DependencyInjection;
using TektonApp.Application.Common.Interfaces;

namespace TektonApp.Infrastructure.Persistence.Extensions
{
    public static class TektonDbContextExtensions
    {
        public static ITektonDbContext GetTektonDbContext(this IServiceProvider provider)
        {
            var builder = provider.GetService<ITektonDbContextBuilder>();
            return builder.Build();
        }
    }
}
