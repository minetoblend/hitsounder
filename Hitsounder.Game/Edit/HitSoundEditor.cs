using Hitsounder.Game.Core;
using Hitsounder.Game.Edit.Patterns;
using Hitsounder.Game.Edit.Samples;
using Hitsounder.Game.Graphics;
using Hitsounder.Game.Input;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;

namespace Hitsounder.Game.Edit;

public partial class HitSoundEditor(Project project) : Screen
{
    [Cached]
    private readonly Project project = project;

    [BackgroundDependencyLoader]
    private void load()
    {
        AddInternal(new HitsounderContextMenuContainer
        {
            RelativeSizeAxes = Axes.Both,
            Child = new DragOperationHandler
            {
                RelativeSizeAxes = Axes.Both,
                Child = new BorderLayout
                {
                    Left = new SampleBrowser(),
                    Center = new PatternEditor(),
                }
            }
        });
    }
}
