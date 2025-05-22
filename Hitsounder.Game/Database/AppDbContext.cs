using System;
using System.IO;
using Hitsounder.Game.Core;
using Microsoft.EntityFrameworkCore;
using osu.Framework.Logging;
using osu.Framework.Platform;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Hitsounder.Game.Database;

public class AppDbContext : DbContext
{
    public string DbPath { get; }

    protected readonly Logger Logger;

    public AppDbContext(Storage? storage = null)
    {
        if (storage != null)
        {
            DbPath = storage.GetFullPath("data.db");
        }
        else
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            DbPath = Path.Join(path, "data.db");
        }

        Logger = Logger.GetLogger(LoggingTarget.Database);
    }

    public DbSet<ProjectInfo> Projects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.UseSqlite($"Data Source={DbPath}")
               .LogTo(message => Logger.Add(message), minimumLevel: LogLevel.Information);
}
