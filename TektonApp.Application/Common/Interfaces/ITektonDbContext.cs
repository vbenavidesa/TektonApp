using Microsoft.EntityFrameworkCore;
using TektonApp.Domain.Entities;

namespace TektonApp.Application.Common.Interfaces
{
    public interface ITektonDbContext
    {
        #region Interface DbSets
        DbSet<ProductMaster> ProductMasters { get; set; }
        #endregion

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
