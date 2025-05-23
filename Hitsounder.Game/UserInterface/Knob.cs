using System;
using System.Numerics;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Audio.Sample;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using Vector2 = osuTK.Vector2;

namespace Hitsounder.Game.UserInterface;

public partial class Knob<T> : CompositeDrawable, IHasCurrentValue<T>, IHasContextMenu
    where T : struct, INumber<T>, IMinMaxValue<T>
{
    private readonly BindableNumberWithCurrent<T> current = new BindableNumberWithCurrent<T>();

    public Bindable<T> Current
    {
        get => current.Current;
        set => current.Current = value;
    }

    private Drawable dial = null!;

    protected Sample Sample = null!;

    private Container rangeIndicators = null!;

    [BackgroundDependencyLoader]
    private void load(AudioManager audio, TextureStore textures)
    {
        Sample = audio.Samples.Get("UI/notch-tick");

        Size = new Vector2(24);
        InternalChildren =
        [
            rangeIndicators = new Container { RelativeSizeAxes = Axes.Both, },
            new Container
            {
                RelativeSizeAxes = Axes.Both,
                Children =
                [
                    new Sprite
                    {
                        RelativeSizeAxes = Axes.Both,
                        Texture = textures.Get("UI/knob-base"),
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                    },
                    dial = new Sprite
                    {
                        RelativeSizeAxes = Axes.Both,
                        Texture = textures.Get("UI/knob-dial"),
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                    },
                ]
            },
        ];
    }

    private void createRangeIndicators()
    {
        rangeIndicators.Clear();

        addIndicator(0);
        addIndicator(1);

        float defaultProgress = float.CreateChecked((current.Default - current.MinValue) / (current.MaxValue - current.MinValue));

        if (defaultProgress is >= 0.05f and <= 0.95f)
            addIndicator(defaultProgress);

        void addIndicator(float progress)
        {
            progress = 1 - progress;

            float angle = MathF.PI * (0.25f - progress * 1.5f);

            var position = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * 0.45f;

            rangeIndicators.Add(new FastCircle
            {
                Position = position,
                Colour = Color4Extensions.FromHex("#A5A5AE"),
                Alpha = 0.8f,
                Size = new Vector2(1.5f),
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativePositionAxes = Axes.Both,
            });
        }
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();

        current.BindValueChanged(_ =>
        {
            dial.RotateTo(-135 + NormalizedValue * 270, 200, Easing.OutExpo);
        }, true);
        dial.FinishTransforms();

        createRangeIndicators();
        current.DefaultChanged += _ => Scheduler.AddOnce(createRangeIndicators);
    }

    protected float NormalizedValue
    {
        get
        {
            if (!current.HasDefinedRange)
            {
                throw new InvalidOperationException($"A {nameof(Knob<T>)}'s {nameof(Current)} must have user-defined {nameof(BindableNumber<T>.MinValue)}"
                                                    + $" and {nameof(BindableNumber<T>.MaxValue)} to produce a valid {nameof(NormalizedValue)}.");
            }

            return current.NormalizedValue;
        }
    }

    public T StepSize;

    private double lastPlayback;

    private void playSampleDebounced()
    {
        if (Time.Current - lastPlayback > 35)
        {
            PlaySample();
            lastPlayback = Time.Current;
        }
    }

    protected virtual void PlaySample()
    {
        Sample.Frequency.Value = 1 + (NormalizedValue - 0.5) * 0.1;
        Sample.Play();
    }

    protected override bool OnScroll(ScrollEvent e)
    {
        if (StepSize != default)
        {
            var previous = current.Value;

            current.Value += (StepSize * T.CreateChecked(Math.Sign(-e.ScrollDelta.Y)));

            if (previous != current.Value)
                playSampleDebounced();

            return true;
        }

        return base.OnScroll(e);
    }

    public MenuItem[]? ContextMenuItems =>
    [
        new MenuItem("Reset", () => current.SetDefault())
    ];

    protected override bool OnDragStart(DragStartEvent e) => true;

    protected override void OnDrag(DragEvent e)
    {
        base.OnDrag(e);

        var previous = current.Value;

        current.Value += T.CreateChecked(-e.Delta.Y / 250f * float.CreateChecked(current.MaxValue - current.MinValue));

        if (previous != current.Value)
            playSampleDebounced();
    }
}
