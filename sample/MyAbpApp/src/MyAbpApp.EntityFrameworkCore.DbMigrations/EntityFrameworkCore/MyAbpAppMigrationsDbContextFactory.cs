﻿using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MyAbpApp.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class MyAbpAppMigrationsDbContextFactory : IDesignTimeDbContextFactory<MyAbpAppMigrationsDbContext>
    {
        public MyAbpAppMigrationsDbContext CreateDbContext(string[] args)
        {
            MyAbpAppEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<MyAbpAppMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new MyAbpAppMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../MyAbpApp.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
