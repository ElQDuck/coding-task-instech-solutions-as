using Claims.BusinessLogic.Interfaces;
using Claims.Database.Auditing;
using Claims.Database.Context;
using Claims.Database.Repositories;
using Claims.Database.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using DbContext = Claims.Database.Context.DbContext;

namespace Claims.Database
{
    /// <summary>
    /// Extension methods for setting up database-related services in an <see cref="IServiceCollection"/>.
    /// </summary>
    public static class DatabaseDependencyInjection
    {
        /// <summary>
        /// Adds database services and repositories to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to add services to.</param>
        /// <param name="sqlConnectionString">The SQL Server connection string.</param>
        /// <param name="mongoConnectionString">The MongoDB connection string.</param>
        /// <param name="mongoDatabaseName">The name of the MongoDB database.</param>
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
            
            services.AddSingleton<IAuditChannel, AuditChannel>();
            services.AddSingleton<IAuditerService, AuditerService>();
            // Task 3: Using a Background service which can be replaced by e.g.
            // MqttAuditBackgroundService listening on topic e.g. audit/logs
            services.AddHostedService<AuditBackgroundService>();
            
            services.AddScoped<IMigrationService, MigrationService>();
        }
    }
}
