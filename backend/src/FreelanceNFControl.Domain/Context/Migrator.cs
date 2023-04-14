using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FreelanceNFControl.Domain.DbContext
{
    public class Migrator
    {
        private readonly IServiceProvider _serviceProvider;

        public Migrator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Migrate()
        {
            using var scope = _serviceProvider.CreateScope();

            var dbContext = scope.ServiceProvider.GetService<AppDbContext>()
                ?? throw new NullReferenceException();

            await dbContext.Database.MigrateAsync();
        }
    }
}
