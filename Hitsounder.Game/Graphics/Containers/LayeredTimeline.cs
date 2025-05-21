using System;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;

namespace Hitsounder.Game.Graphics.Containers;

[Cached]
public abstract partial class LayeredTimeline<TModel> : CompositeDrawable
    where TModel : notnull
{
    private readonly LayerContainer layerContainer;

    protected FillFlowContainer<RearrangeableListItem<TModel>> LayersFlow => layerContainer.ListContainer;

    public BindableList<TModel> Layers => layerContainer.Items;

    public readonly float HeaderWidth;

    protected virtual Drawable CreateBackground() => new Box
    {
        RelativeSizeAxes = Axes.Both,
        Colour = Color4Extensions.FromHex("#333339")
    };

    protected LayeredTimeline(float headerWidth)
    {
        HeaderWidth = headerWidth;
        RelativeSizeAxes = Axes.Both;

        Container headerContainer;

        InternalChildren =
        [
            layerContainer = new LayerContainer(this)
            {
                RelativeSizeAxes = Axes.Both,
            },
            headerContainer = new Container
            {
                RelativeSizeAxes = Axes.Y,
                Width = headerWidth,
                Child = CreateBackground().With(d => d.Depth = float.MaxValue)
            }
        ];

        layerContainer.OnNewLayer += layer =>
        {
            layer.OnLoadComplete += _ =>
            {
                headerContainer.Add(layer.HeaderProxy);
            };
        };
    }

    protected abstract TimelineLayer<TModel> CreateLayer(TModel item);

    protected abstract ScrollContainer<Drawable> CreateScrollContainer();

    protected partial class LayerContainer(LayeredTimeline<TModel> parent) : RearrangeableListContainer<TModel>
    {
        public event Action<TimelineLayer<TModel>>? OnNewLayer;

        protected override ScrollContainer<Drawable> CreateScrollContainer() => parent.CreateScrollContainer();

        protected override RearrangeableListItem<TModel> CreateDrawable(TModel item)
        {
            var layer = parent.CreateLayer(item);
            OnNewLayer?.Invoke(layer);
            return layer;
        }

        public new FillFlowContainer<RearrangeableListItem<TModel>> ListContainer => base.ListContainer;
    }
}
