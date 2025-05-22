using osu.Framework.Graphics.Textures;

namespace Hitsounder.Game;

public class HitsounderIcons(ITextureStore textures)
{
    public Texture Clap => textures.Get("Icons/clap");
    public Texture Finish => textures.Get("Icons/finish");
    public Texture Whistle => textures.Get("Icons/whistle");
}
