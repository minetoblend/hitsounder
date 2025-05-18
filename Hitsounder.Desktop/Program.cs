using osu.Framework.Platform;
using osu.Framework;
using Hitsounder.Game;

namespace Hitsounder.Desktop
{
    public static class Program
    {
        public static void Main()
        {
            using (GameHost host = Host.GetSuitableDesktopHost(@"Hitsounder"))
            using (osu.Framework.Game game = new HitsounderGame())
                host.Run(game);
        }
    }
}
