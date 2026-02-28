using Claims.BusinessLogic.Interfaces;
using Claims.Database.Repositories;
using Claims.Database.Repositories.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using DbContext = Claims.Database.Repositories.DbContext;

namespace Claims.Database
{
    public static class DatabaseDependencyInjection
    {
        public static void AddDatabaseServices(
            IServiceCollection services,
            string sqlConnectionString,
            string mongoConnectionString,
            string mongoDatabaseName)
        {
            services.AddDbContext<AuditContext>(options =>
                options.UseSqlServer(sqlConnectionString));

            services.AddDbContext<DbContext>(options =>
            {
                var client = new MongoClient(mongoConnectionString);
                var database = client.GetDatabase(mongoDatabaseName);
                options.UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName);
            });

            // Register Repositories and Services
            services.AddScoped<IClaimsRepository, ClaimsRepository>();
            services.AddScoped<ICoversRepository, CoversRepository>();
            services.AddScoped<IAuditRepository, AuditRepository>();
            services.AddScoped<IMigrationService, MigrationService>();
        }
    }
}
