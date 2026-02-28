using Claims.BusinessLogic.Interfaces;
using Claims.Database.Auditing;
using Claims.Database.Context;
using Claims.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using DbContext = Claims.Database.Context.DbContext;

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
            // Register IMongoClient as a singleton
            services.AddSingleton<IMongoClient>(_ => new MongoClient(mongoConnectionString));

            services.AddDbContext<AuditContext>(options =>
                options.UseSqlServer(sqlConnectionString));

            services.AddDbContext<DbContext>((sp, options) =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                options.UseMongoDB(client, mongoDatabaseName);
            });

            // Register Repositories and Services
            services.AddScoped<IClaimsRepository, ClaimsRepository>();
            services.AddScoped<ICoversRepository, CoversRepository>();
            services.AddScoped<IAuditRepository, AuditRepository>();
            services.AddScoped<IAuditerService, AuditerService>();
            services.AddScoped<IMigrationService, MigrationService>();
        }
    }
}
