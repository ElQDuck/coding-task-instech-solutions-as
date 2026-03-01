using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Claims.BusinessLogic
{
    /// <summary>
    /// Extension methods for setting up business logic and persistence services in an <see cref="IServiceCollection"/>.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds business logic services and persistence to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to add services to.</param>
        /// <param name="sqlConnectionString">The SQL Server connection string.</param>
        /// <param name="mongoConnectionString">The MongoDB connection string.</param>
        /// <param name="mongoDatabaseName">The name of the MongoDB database.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddBusinessLogicAndPersistence(
            this IServiceCollection services, 
            string sqlConnectionString, 
            string mongoConnectionString, 
            string mongoDatabaseName)
        {
            // Register BusinessLogic services
            services.AddScoped<Interfaces.IClaimsService, Services.ClaimsService>();
            services.AddScoped<Interfaces.ICoversService, Services.CoversService>();
            services.AddScoped<Interfaces.IPremiumComputeService, Services.PremiumComputeService>();
            
            // Register Premium Calculation Strategies
            services.AddScoped<Interfaces.ICoverPremiumStrategy, Services.Strategies.YachtPremiumStrategy>();
            services.AddScoped<Interfaces.ICoverPremiumStrategy, Services.Strategies.PassengerShipPremiumStrategy>();
            services.AddScoped<Interfaces.ICoverPremiumStrategy, Services.Strategies.TankerPremiumStrategy>();

            // Scan for Database assembly and register Repositories
            // We assume the Database assembly is in the same directory as the executing assembly
            var basePath = AppContext.BaseDirectory;
            var databaseAssemblyPath = Path.Combine(basePath, "Claims.Database.dll");
            
            if (File.Exists(databaseAssemblyPath))
            {
                var databaseAssembly = Assembly.LoadFrom(databaseAssemblyPath);
                
                // Let the Database layer define its own DI setup extension to keep EF Core dependencies out of BusinessLogic
                // Find a static class named "DatabaseDependencyInjection" or similar
                var dbDiType = databaseAssembly.GetTypes()
                    .FirstOrDefault(t => t.IsClass && t.IsAbstract && t.IsSealed && t.Name == "DatabaseDependencyInjection");

                if (dbDiType != null)
                {
                    var addDatabaseMethod = dbDiType.GetMethod("AddDatabaseServices", BindingFlags.Static | BindingFlags.Public);
                    if (addDatabaseMethod != null)
                    {
                        addDatabaseMethod.Invoke(null, new object[] { services, sqlConnectionString, mongoConnectionString, mongoDatabaseName });
                    }
                }
            }
            else
            {
                // Fallback or warning - in a real app, maybe log this
                Console.WriteLine($"WARNING: Could not find database assembly at {databaseAssemblyPath}");
            }

            return services;
        }
    }
}
