using System;
using Hitsounder.Game.Core.Samples;
using Hitsounder.Game.Input;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace Hitsounder.Game.Edit.Samples;

public sealed partial class DraggedSample(IHitSoundSample sample) : CompositeDrawable, IDragRepresentation
{
    [BackgroundDependencyLoader]
    private void load()
    {
        Width = 150;
        AutoSizeAxes = Axes.Y;

        InternalChild = new GridContainer
        {
            RelativeSizeAxes = Axes.X,
            AutoSizeAxes = Axes.Y,
            RowDimensions = [new Dimension(GridSizeMode.AutoSize)],
            ColumnDimensions = [new Dimension(GridSizeMode.AutoSize), new Dimension()],
            Padding = new MarginPadding(4) { Left = 14 },
            Content = new[]
            {
                new Drawable[]
                {
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
                        RelativeSizeAxes = Axes.X,
                        Truncate = true
                    }
                }
            },
        };
    }

    public void OnDragStart(Vector2 position)
    {
        Position = position;
    }

    public void OnDragEnd()
    {
    }

    public void UpdatePosition(Vector2 position)
    {
        Position = Vector2.Lerp(position, Position, (float)Math.Exp(-0.02 * Time.Elapsed));
    }
}
