using System;
using Hitsounder.Game.Core.Samples;
using Hitsounder.Game.Input;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace Hitsounder.Game.Edit.Samples;

public sealed partial class DraggedSample(ISampleCollectionEntry sample) : CompositeDrawable, IDragRepresentation
{
    [BackgroundDependencyLoader]
    private void load()
    {
        AutoSizeAxes = Axes.Both;
        Origin = Anchor.TopCentre;

        InternalChild = new FillFlowContainer
        {
            AutoSizeAxes = Axes.Both,
            Direction = FillDirection.Horizontal,
            Padding = new MarginPadding(4) { Left = 14 },
            Children =
            [
                new SpriteIcon
                {
                    Size = new Vector2(14),
                    Icon = FontAwesome.Regular.FileAudio,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Margin = new MarginPadding { Right = 4 }
                },
                new SpriteText
                {
                    Text = sample.Name,
                    Font = new FontUsage(size: 16f),
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                }
            ]
        };
    }

    public void OnDragStart(Vector2 position)
    {
        Position = position;

        this.FadeInFromZero(100)
            .ScaleTo(0.5f)
            .ScaleTo(1f, 300, Easing.OutElasticHalf);
    }

    public void OnDragEnd(bool dropped)
    {
        if (dropped)
        {
            this.FadeOut(50)
                .ScaleTo(0.25f, 50, Easing.In);
        }
        else
        {
            this.FadeOut(100)
                .MoveToOffset(new Vector2(0, 20), 100, Easing.In);
        }
    }

    public void UpdatePosition(Vector2 position)
    {
        Position = Vector2.Lerp(position, Position, (float)Math.Exp(-0.02 * Time.Elapsed));
    }
}
