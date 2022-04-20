using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TektonApp.Application.Common.Interfaces;
using TektonApp.Common.Constants;

namespace TektonApp.Infrastructure.Persistence.Factories
{
    public class TektonDbContextBuilder : ITektonDbContextBuilder
    {
        private readonly IDomainEventService _domainEventService;
        public IConfiguration Configuration { get; }
        public TektonDbContextBuilder(IConfiguration configuration, IDomainEventService domainEventService)
        {
            _domainEventService = domainEventService;
            Configuration = configuration;
        }

        public ITektonDbContext Build()
        {
            var options = GetContextOptions<TektonDbContext>(AppSettingsConstants.DefaultConnection, AppSettingsConstants.UseInMemoryDatabase);
            var context = new TektonDbContext(options, Configuration, _domainEventService);
            return context;
        }

        private static DbContextOptions<TContext> GetContextOptions<TContext>(string connectionString, bool isUseInMemoryDatabase) where TContext : DbContext
        {
            if (isUseInMemoryDatabase)
                return new DbContextOptionsBuilder<TContext>()
                    .Options;

            return new DbContextOptionsBuilder<TContext>()
                .UseSqlServer(connectionString)
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .Options;
        }
    }
}
