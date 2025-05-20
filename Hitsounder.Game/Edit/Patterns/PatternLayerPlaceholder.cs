using Hitsounder.Game.Core.Patterns;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace Hitsounder.Game.Edit.Patterns;

public partial class PatternLayerPlaceholder : RearrangeableListItem<PatternLayer>
{
    public PatternLayerPlaceholder(DummyPatternLayer item)
        : base(item)
    {
        RelativeSizeAxes = Axes.X;
        Height = 10;
        AddInternal(new Box
        {
            RelativeSizeAxes = Axes.Both,
            Alpha = 0.25f,
        });
    }

    protected override bool IsDraggableAt(Vector2 screenSpacePos) => false;
}
