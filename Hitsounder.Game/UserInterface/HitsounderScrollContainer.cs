using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;

namespace Hitsounder.Game.UserInterface;

public partial class HitsounderScrollContainer : BasicScrollContainer
{
    public bool HandleScrollEvents = true;

    protected override bool OnScroll(ScrollEvent e)
    {
        if (!HandleScrollEvents)
            return false;

        return base.OnScroll(e);
    }
}
