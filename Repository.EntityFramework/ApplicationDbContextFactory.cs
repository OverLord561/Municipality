using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Repositories.EntityFramework
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        private readonly IConfiguration _config;
        //public ApplicationDbContextFactory(IConfiguration configuration)
        //{
        //    _config = configuration;
        //}

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            //builder.UseSqlServer(_config.GetConnectionString("Defa‌​ultConnection"));
            
              builder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=Municipality;Integrated Security=True;MultipleActiveResultSets=true;");

            return new ApplicationDbContext(builder.Options);
        }
    }
}
