using Hitsounder.Game.Graphics;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;

namespace Hitsounder.Game.Edit.Samples;

public partial class SampleBrowserGroupHeader : CompositeDrawable
{
    private readonly BindableBool expanded = new BindableBool();

    public SampleBrowserGroupHeader(string title, Bindable<bool> expanded, MarginPadding indentPadding)
    {
        this.expanded.BindTo(expanded);
        RelativeSizeAxes = Axes.X;
        AutoSizeAxes = Axes.Y;

        InternalChildren =
        [
            new Container
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Padding = indentPadding,
                Child = new GridContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    RowDimensions = [new Dimension(GridSizeMode.AutoSize)],
                    ColumnDimensions = [new Dimension(GridSizeMode.AutoSize), new Dimension()],
                    Padding = new MarginPadding(4),
                    Content = new[]
                    {
                        new Drawable[]
                        {
                            new SpriteIcon
                            {
                                Size = new Vector2(14),
                                Icon = FontAwesome.Regular.Folder,
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                                Margin = new MarginPadding { Right = 4 }
                            },
                            new SpriteText
                            {
                                Text = title,
                                Font = new FontUsage(size: 16f),
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                                RelativeSizeAxes = Axes.X,
                                Truncate = true
                            }
                        }
                    }
                },
            },
            new HoverHighlight(),
        ];
    }

    protected override bool OnClick(ClickEvent e)
    {
        expanded.Toggle();
        return true;
    }
}
