using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.EntityFramework.Configuration
{
    internal abstract class DbEntityConfiguration<TEntity> where TEntity : class
    {
        public abstract void Configure(EntityTypeBuilder<TEntity> entity);
    }
}
