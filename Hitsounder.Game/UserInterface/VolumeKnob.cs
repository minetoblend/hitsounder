using osu.Framework.Graphics.Cursor;
using osu.Framework.Input.Events;
using osu.Framework.Localisation;

namespace Hitsounder.Game.UserInterface;

public partial class VolumeKnob : Knob<float>, IHasTooltip
{
    public LocalisableString TooltipText => $"Volume: {Current.Value:P0}";

    protected override bool OnHover(HoverEvent e) => true;

    protected override void PlaySample()
    {
        Sample.Volume.Value = Current.Value;
        base.PlaySample();
    }
}
