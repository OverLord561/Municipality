using Microsoft.EntityFrameworkCore;
using AzureLogger;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repositories.EntityFramework.Repositories
{
    public class LoggableRepository<TEntity> : Repository<TEntity> where TEntity : class, new()
    {
        private readonly ICosmosLogger _logger;
        public LoggableRepository(DbContext context, ICosmosLogger logger)
            : base(context)
        {            
            _logger = logger;
        }
        
        public override (IEnumerable<TEntity>, int) Get(int page, int size)
        {
            _logger.Insert("Get");
            return base.Get(page, size);
        }
        

    }
}
