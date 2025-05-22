using Hitsounder.Game.Core;
using Hitsounder.Game.UserInterface;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;

namespace Hitsounder.Game.Edit.Samples;

public partial class SampleBrowser : CompositeDrawable
{
    [Resolved]
    private Project project { get; set; } = null!;

    private readonly FillFlowContainer itemsFlow;

    public SampleBrowser()
    {
        Width = 200;
        RelativeSizeAxes = Axes.Y;

        InternalChildren =
        [
            new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = Color4Extensions.FromHex("#222228")
            },
            new HitsounderScrollContainer
            {
                RelativeSizeAxes = Axes.Both,
                Child = itemsFlow = new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Direction = FillDirection.Vertical,
                }
            }
        ];
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();

        populate();
    }

    private void populate()
    {
        itemsFlow.Clear();

        foreach (var collection in project.SampleCollections)
            itemsFlow.Add(new SampleBrowserDirectory(collection));
    }
}
