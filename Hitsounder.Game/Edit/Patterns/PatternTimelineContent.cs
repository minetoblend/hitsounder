using Hitsounder.Game.Core.Patterns;
using Hitsounder.Game.Graphics.Containers;
using osu.Framework.Allocation;

namespace Hitsounder.Game.Edit.Patterns;

public partial class PatternTimelineContent(PatternLayer layer) : TimelineContent
{
    [BackgroundDependencyLoader]
    private void load()
    {
        // Add(new DrawableSample(layer)
        // {
        //     X = 100
        // });
    }

    protected override void Update()
    {
        base.Update();
    }
}
