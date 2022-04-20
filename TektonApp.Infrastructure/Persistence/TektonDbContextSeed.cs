using TektonApp.Application.Common.Interfaces;

namespace TektonApp.Infrastructure.Persistence
{
    public static class TektonDbContextSeed
    {
        public static async Task SeedBaseData(ITektonDbContext context)
        {
            await SeedDataAsync(context);
        }

        private static async Task SeedDataAsync(ITektonDbContext context)
        {

        }
    }
}
