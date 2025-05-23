using Hitsounder.Game.Core;
using Hitsounder.Game.Database;
using Hitsounder.Game.Graphics;
using Hitsounder.Resources;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;
using osu.Game.Configuration;
using osu.Game.Resources;
using osuTK;

namespace Hitsounder.Game
{
    public partial class HitsounderGameBase : osu.Framework.Game
    {
        private Container<Drawable> content = null!;

        protected override Container<Drawable> Content => content;

        private DependencyContainer dependencies = null!;

        public ProjectManager ProjectManager { get; private set; } = null!;

        public Storage Storage { get; private set; } = null!;

        private DbAccess db = null!;

        protected SessionStatics SessionStatics { get; private set; } = null!;

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            Storage = host.Storage;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Resources.AddStore(new DllResourceStore(HitsounderResources.ResourceAssembly));
            Resources.AddStore(new DllResourceStore(OsuResources.ResourceAssembly));

            AddFont(Resources, @"Fonts/Inter/Inter-Regular");

            dependencies.CacheAs(Storage);

            dependencies.Cache(db = new DbAccess(Storage));

            dependencies.CacheAs(ProjectManager = new ProjectManager(Storage, db, Host, Resources, Audio));

            dependencies.Cache(SessionStatics = new SessionStatics());

            dependencies.Cache(new HitsounderIcons(Textures));

            base.Content.Add(new DrawSizePreservingFillContainer
            {
                TargetDrawSize = new Vector2(1366, 768),
                Child = content = new HitsounderTooltipContainer
                {
                    RelativeSizeAxes = Axes.Both,
                }
            });
        }

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
        {
            return dependencies = new DependencyContainer(base.CreateChildDependencies(parent));
        }

        protected override void Dispose(bool isDisposing)
        {
            db.Dispose();

            base.Dispose(isDisposing);
        }
    }
}
