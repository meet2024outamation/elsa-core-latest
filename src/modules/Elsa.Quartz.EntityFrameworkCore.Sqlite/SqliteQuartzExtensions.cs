﻿using Elsa.EntityFrameworkCore.Common;
using Elsa.Quartz.EntityFrameworkCore.Sqlite;
using Elsa.Quartz.Features;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

// ReSharper disable once CheckNamespace
namespace Elsa.EntityFrameworkCore.Extensions;

/// <summary>
/// Provides extensions to configure EF Core to use SQLite.
/// </summary>
[PublicAPI]
public static class SqliteQuartzExtensions
{
    /// <summary>
    /// Configures the <see cref="QuartzFeature"/> to use the SQLite job store.
    /// </summary>
    public static QuartzFeature UseSqlite(this QuartzFeature feature, string connectionString = Constants.DefaultConnectionString)
    {
        feature.Services.AddDbContextFactory<SqliteQuartzDbContext>(options =>
        {
            // Use SQLite migrations.
            options.UseSqlite(connectionString, sqlite => { sqlite.MigrationsAssembly(typeof(SqliteQuartzDbContext).Assembly.GetName().Name); });
        });

        feature.ConfigureQuartz += quartz =>
        {
            quartz.UsePersistentStore(store =>
            {
                store.UseNewtonsoftJsonSerializer();
                store.UseMicrosoftSQLite(connectionString);
            });
        };

        // Configure the Quartz hosted service to run migrations.
        feature.Module.ConfigureHostedService<RunMigrationsHostedService<SqliteQuartzDbContext>>(-100);

        return feature;
    }
}