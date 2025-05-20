using osu.Framework.Graphics;

namespace Hitsounder.Game.Input;

public interface IDragEventHandler : IDrawable;

public interface IDragEventHandler<T> : IDragEventHandler
{
    public bool OnDragEnter(EditorDragEvent<T> e);

    public void OnDragLeave(EditorDragEvent<T> e);

    public bool OnDrop(EditorDragEvent<T> e);
}
