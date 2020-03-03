namespace DatabaseMigrator
{
    using System;
    using CustomLogic.Database;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class DatabaseInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            var dbConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings:dbConnectionString");
            services.AddDbContext<DatabaseMigratorContext>(options =>
            {
                options.UseSqlServer(dbConnectionString);
            });
        }
    }
}