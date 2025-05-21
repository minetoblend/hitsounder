using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace Hitsounder.Game.Graphics.Containers;

public interface ITimeline : IDrawable
{
    public double StartTime { get; }

    public double TotalDuration { get; }

    public float DurationToSize(double duration);

    public void ApplyToContent<T>(Container<T> content) where T : Drawable;
}
