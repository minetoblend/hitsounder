using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace Hitsounder.Game.Edit.Samples;

[Cached]
public abstract partial class SampleBrowserItem : CompositeDrawable
{
    [Resolved]
    private SampleBrowserItem? parentItem { get; set; }

    protected int IndentLevel { get; private set; }

    protected MarginPadding IndentPadding => new MarginPadding { Left = IndentLevel * 10 };

    [BackgroundDependencyLoader]
    private void load()
    {
        AutoSizeAxes = Axes.Y;
        RelativeSizeAxes = Axes.X;

        IndentLevel = parentItem != null ? parentItem.IndentLevel + 1 : 0;
    }
}
