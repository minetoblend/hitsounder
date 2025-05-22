using System;
using Hitsounder.Game.Core;
using Microsoft.EntityFrameworkCore;
using osu.Framework.Logging;
using osu.Framework.Platform;

namespace Hitsounder.Game.Database;

public class DbAccess : IDisposable
{
    public DbAccess(Storage storage)
    {
        Context = new AppDbContext(storage);

        Logger.Log("Applying database migrations");
        Context.Database.Migrate();
    }

    public AppDbContext Context { get; }

    public DbSet<ProjectInfo> Projects => Context.Projects;

    public void Dispose()
    {
        Context.Dispose();
    }

    public void Write(Action<AppDbContext> action)
    {
        action(Context);
        Context.SaveChanges();
    }

    public T Write<T>(Func<AppDbContext, T> action)
    {
        var transaction = Context.Database.BeginTransaction();

        try
        {
            var result = action(Context);

            Context.SaveChanges();

            transaction.Commit();

            return result;
        }
        catch
        {
            transaction.Rollback();

            throw;
        }
    }
}
