using System.Collections.Generic;
using Hitsounder.Game.Core.Patterns;
using Hitsounder.Game.Graphics.Containers;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osuTK.Input;

namespace Hitsounder.Game.Edit.Patterns;

[Cached]
public partial class PatternTimeline : LayeredTimeline<PatternLayer>
{
    public PatternTimeline()
        : base(180)
    {
        TimelineContainer.AddRange([
            new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = Color4Extensions.FromHex("#111118"),
            },
            new PatternTickDisplay(),
        ]);

        LayersFlow.Padding = new MarginPadding { Top = 12 };
        LayersFlow.Insert(int.MaxValue, new PatternEndInsertionDropArea().With(d =>
        {
            HeaderContainer.Add(d.CreateProxy());
        }));
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();

        Scheduler.AddDelayed(() =>
        {
            LayersFlow.LayoutDuration = 200;
            LayersFlow.LayoutEasing = Easing.OutExpo;
        }, 1);
    }

    protected override TimelineLayer<PatternLayer> CreateLayer(PatternLayer item) => new PatternTimelineLayer(item);

    private static readonly IList<Key> layer_keys =
    [
        Key.A,
        Key.S,
        Key.D,
        Key.F,
        Key.G,
        Key.H,
        Key.J,
        Key.K,
        Key.L,
        Key.Semicolon,
        Key.Quote,
    ];

    protected override bool OnKeyDown(KeyDownEvent e)
    {
        int index = layer_keys.IndexOf(e.Key);

        if (index >= 0 && index < Layers.Count)
        {
            Layers[index].Play();
            return true;
        }

        return base.OnKeyDown(e);
    }
}
