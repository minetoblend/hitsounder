using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;
using osuTK;

namespace Hitsounder.Game.UserInterface;

public partial class HitsounderMenu : Menu
{
    public HitsounderMenu(Direction direction, bool topLevelMenu = false)
        : base(direction, topLevelMenu)
    {
        MaskingContainer.CornerRadius = 4;
    }

    protected override Menu CreateSubMenu() => new HitsounderMenu(Direction.Vertical);

    protected override DrawableMenuItem CreateDrawableMenuItem(MenuItem item) => new HitsounderMenuItem(item);

    protected override ScrollContainer<Drawable> CreateScrollContainer(Direction direction) => new BasicScrollContainer { ScrollbarVisible = false };

    protected override void UpdateSize(Vector2 newSize)
    {
        if (Direction == Direction.Vertical)
        {
            Width = newSize.X;

            this.ResizeHeightTo(newSize.Y, 200, Easing.OutExpo);
        }
        else
        {
            Height = newSize.Y;

            this.ResizeWidthTo(newSize.X, 200, Easing.OutExpo);
        }
    }

    protected override void AnimateOpen()
    {
        base.AnimateOpen();
    }

    protected override void AnimateClose()
    {
        this.ResizeHeightTo(0, 200, Easing.OutExpo)
            .FadeOut(200);
    }
}
