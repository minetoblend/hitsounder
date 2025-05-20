using Hitsounder.Game.Core.Patterns;
using Hitsounder.Game.UserInterface;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;

namespace Hitsounder.Game.Edit.Patterns;

[Cached]
public partial class PatternLayerContainer : RearrangeableListContainer<PatternLayer>
{
    protected override ScrollContainer<Drawable> CreateScrollContainer() => new HitsounderScrollContainer
    {
        HandleScrollEvents = false,
        ScrollbarAnchor = Anchor.TopLeft,
    };

    protected override RearrangeableListItem<PatternLayer> CreateDrawable(PatternLayer item)
    {
        if (item is DummyPatternLayer dummyLayer)
            return new PatternLayerPlaceholder(dummyLayer);

        return new PatternLayerHeader(item);
    }

    public PatternLayerContainer()
    {
        AddInternal(new Box
        {
            RelativeSizeAxes = Axes.Both,
            Colour = Color4Extensions.FromHex("#333339"),
            Depth = 1,
        });
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();

        Scheduler.AddDelayed(() =>
        {
            ListContainer.LayoutDuration = 200;
            ListContainer.LayoutEasing = Easing.OutExpo;
        }, 1);
    }
}
