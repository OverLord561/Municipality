using Repositories.EntityFramework.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Repositories.EntityFramework
{
    internal static class ModelBuilderExtensions
    {
        public static void AddConfiguration<TEntity>(this ModelBuilder modelBuilder, DbEntityConfiguration<TEntity> entityConfiguration) where TEntity : class
        {
            modelBuilder.Entity<TEntity>(entityConfiguration.Configure);
        }
    }
}
