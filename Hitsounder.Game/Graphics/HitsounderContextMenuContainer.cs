using Hitsounder.Game.UserInterface;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.UserInterface;

namespace Hitsounder.Game.Graphics;

public partial class HitsounderContextMenuContainer : BasicContextMenuContainer
{
    protected override Menu CreateMenu() => new HitsounderMenu(Direction.Vertical);
}
