using osu.Framework.Bindables;

namespace Hitsounder.Game.Core.Patterns;

public class Pattern
{
    public BindableList<PatternLayer> Layers = new BindableList<PatternLayer>();
}
