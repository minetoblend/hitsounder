using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;

namespace Hitsounder.Game.UserInterface;

public partial class HitsounderMenuItem : Menu.DrawableMenuItem
{
    public HitsounderMenuItem(MenuItem item)
        : base(item)
    {
        BackgroundColour = Color4Extensions.FromHex("#222228");
        BackgroundColour = Color4Extensions.FromHex("#333339");
    }

    protected override Drawable CreateContent() => new SpriteText
    {
        Anchor = Anchor.CentreLeft,
        Origin = Anchor.CentreLeft,
        Padding = new MarginPadding(2) { Horizontal = 8 },
        Font = new FontUsage(size: 14),
    };
}
