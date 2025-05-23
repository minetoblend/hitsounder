using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;

namespace Hitsounder.Game.Graphics;

public partial class PanelCutout : CompositeDrawable
{
    [BackgroundDependencyLoader]
    private void load(TextureStore textures)
    {
        InternalChildren =
        [
            new NineSliceSprite
            {
                RelativeSizeAxes = Axes.Both,
                Texture = textures.Get("UI/panel-cutout"),
                TextureInset = new MarginPadding(6),
            },
            new NineSliceSprite
            {
                RelativeSizeAxes = Axes.Both,
                Texture = textures.Get("UI/panel-specular"),
                TextureInset = new MarginPadding(6),
            },
        ];
    }
}
