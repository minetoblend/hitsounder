using Hitsounder.Game.Screens.Menu;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Screens;

namespace Hitsounder.Game
{
    public partial class HitsounderGame : HitsounderGameBase
    {
        private ScreenStack screenStack = null!;

        [BackgroundDependencyLoader]
        private void load()
        {
            Child = new BasicContextMenuContainer
            {
                RelativeSizeAxes = Axes.Both,
                Child = screenStack = new ScreenStack { RelativeSizeAxes = Axes.Both }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            screenStack.Push(new MainMenu());
        }

        private void importBeatmap()
        {
        }
    }
}
