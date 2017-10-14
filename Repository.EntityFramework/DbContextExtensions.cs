using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Linq;

namespace Repositories.EntityFramework
{
    public static class DbContextExtensions
    {
        public static bool AllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(x => x.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(x => x.Key);

            return !total.Except(applied).Any();
        }
    }
}
