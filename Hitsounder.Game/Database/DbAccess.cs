using Microsoft.EntityFrameworkCore;
using osu.Framework.Logging;
using osu.Framework.Platform;

namespace Hitsounder.Game.Database;

public class DbAccess
{
    public DbAccess(Storage storage)
    {
        Context = new AppDbContext(storage);

        Logger.Log("Applying database migrations");
        Context.Database.Migrate();
    }

    public AppDbContext Context { get; }
}
