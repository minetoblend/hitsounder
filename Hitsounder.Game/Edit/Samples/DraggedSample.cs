using Hitsounder.Game.Core.Samples;
using Hitsounder.Game.Input;
using Hitsounder.Game.Utils;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace Hitsounder.Game.Edit.Samples;

public sealed partial class DraggedSample(ISampleEntry sample) : CompositeDrawable, IDragRepresentation
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
        xPos.Current = xPos.Previous = position.X;
        yPos.Current = yPos.Previous = position.Y;

        Position = position;

        this.FadeInFromZero(100)
            .ScaleTo(0.5f)
            .ScaleTo(1f, 500, Easing.OutElasticHalf);
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
            this.FadeOut(200)
                .MoveToOffset(new Vector2(0, 20), 200, Easing.In);
        }
    }

    private readonly SecondOrderDynamics xPos = new SecondOrderDynamics(0, frequency: 3f, damping: 0.75f, response: 1);
    private readonly SecondOrderDynamics yPos = new SecondOrderDynamics(0, frequency: 3f, damping: 0.75f, response: 1);
    private readonly SecondOrderDynamics rotation = new SecondOrderDynamics(0, frequency: 3f, damping: 0.75f, response: 1);

    public void UpdatePosition(Vector2 position)
    {
        Rotation = rotation.Update(Time.Elapsed, (position.X - X) * 0.1f);

        Position = new Vector2(
            xPos.Update(Time.Elapsed, position.X),
            yPos.Update(Time.Elapsed, position.Y)
        );
    }
}
