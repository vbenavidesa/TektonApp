using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TektonApp.Application.Common.Interfaces;
using TektonApp.Common;
using TektonApp.Domain.Entities;

namespace TektonApp.Infrastructure.Persistence
{
    public partial class TektonDbContext : DbContext, ITektonDbContext
    {
        public IConfiguration Configuration { get; }
        private readonly IDomainEventService _domainEventService;
        public TektonDbContext(DbContextOptions<TektonDbContext> options, IConfiguration configuration, IDomainEventService domainEventService) : base(options)
        {
            Configuration = configuration;
            _domainEventService = domainEventService;
        }

        #region Implementations for DbSets
        public virtual DbSet<ProductMaster> ProductMasters { get; set; }
        #endregion

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                var entity = entry.Entity;
                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.State = "A";
                        entity.CreatedBy = "personal.email@email.com";
                        entity.CreatedDate = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entity.State = "U";
                        entry.Property(x => x.CreatedBy).IsModified = false;
                        entry.Property(x => x.CreatedDate).IsModified = false;
                        entity.UpdatedBy = "personal.email@email.com";
                        entity.UpdatedDate = DateTime.Now;
                        break;

                    case EntityState.Deleted:
                        // https://www.ryansouthgate.com/2019/01/07/entity-framework-core-soft-delete/
                        // Set the entity to unchanged (if we mark the whole entity as Modified, every field gets sent to Db as an update)
                        entry.State = EntityState.Unchanged;

                        // Only update the IsDeleted flag - only this will get sent to the Db
                        entity.State = "D";
                        entry.Property(x => x.CreatedBy).IsModified = false;
                        entry.Property(x => x.CreatedDate).IsModified = false;
                        entry.Property(x => x.UpdatedBy).IsModified = false;
                        entry.Property(x => x.UpdatedDate).IsModified = false;
                        entity.DeletedBy = "personal.email@email.com";
                        entity.DeletedDate = DateTime.Now;
                        break;
                }
            }
            var result = await base.SaveChangesAsync(cancellationToken);
            await DispatchEvents();
            return result;
        }

        private async Task DispatchEvents()
        {
            while (true)
            {
                var domainEventEntity = ChangeTracker.Entries<IHasDomainEvent>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .Where(domainEvent => !domainEvent.IsPublished)
                    .FirstOrDefault();

                if (domainEventEntity == null) break;

                domainEventEntity.IsPublished = true;

                await _domainEventService.Publish(domainEventEntity);
            }
        }
    }
}
