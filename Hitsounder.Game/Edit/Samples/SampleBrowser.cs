using System.Collections.Generic;
using System.Linq;
using Hitsounder.Game.Core;
using Hitsounder.Game.Core.Samples;
using Hitsounder.Game.UserInterface;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;

namespace Hitsounder.Game.Edit.Samples;

public partial class SampleBrowser : CompositeDrawable
{
    [Resolved]
    private Project project { get; set; } = null!;

    private FillFlowContainer itemsFlow;

    public SampleBrowser()
    {
        Width = 200;
        RelativeSizeAxes = Axes.Y;

        InternalChildren =
        [
            new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = Color4Extensions.FromHex("#222228")
            },
            new HitsounderScrollContainer
            {
                RelativeSizeAxes = Axes.Both,
                Child = itemsFlow = new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Direction = FillDirection.Vertical,
                }
            }
        ];
    }

    private IEnumerable<ISampleCollection> collections = null!;

    [BackgroundDependencyLoader]
    private void load(SampleCollectionManager collectionManager, AudioManager audio)
    {
        collections = collectionManager.GetAll().Select(c => new DatabaseBackedSampleCollection(c, audio));
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();

        rebuildContent();
    }

    private void rebuildContent()
    {
        itemsFlow.Clear();

        foreach (var collection in project.SampleCollections)
            itemsFlow.Add(new SampleBrowserDirectory(SampleTree.FromCollection(collection)));

        foreach (var collection in collections)
        {
            itemsFlow.Add(new SampleBrowserDirectory(SampleTree.FromCollection(collection)));
        }
    }
}
