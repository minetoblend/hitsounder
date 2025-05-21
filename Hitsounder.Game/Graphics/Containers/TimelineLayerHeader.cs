using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace Hitsounder.Game.Graphics.Containers;

public partial class TimelineLayerHeader : CompositeDrawable
{
    public virtual bool IsDraggableAt(Vector2 screenSpacePos) => false;

    public TimelineLayerHeader()
    {
        RelativeSizeAxes = Axes.Both;
    }

    protected override void Dispose(bool isDisposing)
    {
        if (LifetimeEnd == double.MaxValue)
            Expire();

        base.Dispose(isDisposing);
    }
}
