using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
using ToDoList.Models;
using Timer = System.Timers.Timer;

namespace ToDoList.Controls;

public partial class TaskStatusIcon : TemplatedControl
{
    public static readonly StyledProperty<TaskModelStatus> ModelStatusProperty =
        AvaloniaProperty.Register<TaskStatusIcon, TaskModelStatus>(nameof(ModelStatus),
            defaultValue: TaskModelStatus.NotStarted);

    public static readonly StyledProperty<Bitmap> IconProperty =
        AvaloniaProperty.Register<TaskStatusIcon, Bitmap>(nameof(Icon),
            defaultValue: new Bitmap(AssetLoader.Open(new Uri(DefaultUri))));

    public static readonly StyledProperty<Bitmap> OldIconProperty =
        AvaloniaProperty.Register<TaskStatusIcon, Bitmap>(nameof(OldIcon),
            defaultValue: new Bitmap(AssetLoader.Open(new Uri(DefaultUri))));

    public static readonly StyledProperty<Thickness> ImageMarginProperty =
        AvaloniaProperty.Register<TaskStatusIcon, Thickness>(nameof(ImageMargin), defaultValue: new Thickness(2));

    public static readonly DirectProperty<TaskStatusIcon, double> DefaultScaleProperty =
        AvaloniaProperty.RegisterDirect<TaskStatusIcon, double>(nameof(DefaultScale), o => o.DefaultScale);

    private static readonly PeriodicTimer AnimationTimer = new(TimeSpan.FromSeconds(10));
    
    private static event Action? OnAnimationTick;
    
    static TaskStatusIcon()
    {
        RunAnimationClock();
    }

    private static async void RunAnimationClock()
    {
        try
        {
            while (true)
            {
                await AnimationTimer.WaitForNextTickAsync();
            
                OnAnimationTick?.Invoke();
            
                await Task.Delay(500);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    public double DefaultScale => 1d;

    public Bitmap Icon
    {
        get => GetValue(IconProperty);
        private set => SetValue(IconProperty, value);
    }

    public Bitmap OldIcon
    {
        get => GetValue(OldIconProperty);
        private set => SetValue(OldIconProperty, value);
    }

    public Thickness ImageMargin
    {
        get => GetValue(ImageMarginProperty);
        set => SetValue(ImageMarginProperty, value);
    }

    private const string AnimatedImageName = "AnimImg";
    private const string ImageName = "Image";

    private Animation _fadeOutAnimation = null!;
    private Animation _fadeInAnimation = null!;
    private Animation _runningAnimation = null!;

    private CancellationTokenSource _cancellationTokenSource = null!;
    private CancellationToken _cancellationToken;

    private Image? _animImg;
    private Image? _image;

    private const string DefaultUri = "avares://ToDoList/Assets/Icons/cross-64.png";

    private readonly string[] _imagesUri =
    [
        "avares://ToDoList/Assets/Icons/cross-64.png",
        "avares://ToDoList/Assets/Icons/gear-64.png",
        "avares://ToDoList/Assets/Icons/accept-64.png"
    ];

    public TaskStatusIcon()
    {
        CreateFadeInAnimation();
        CreateFadeOutAnimation();

        CreateNewTokenSource();
    }

    public TaskModelStatus ModelStatus
    {
        get => GetValue(ModelStatusProperty);
        set => SetValue(ModelStatusProperty, value);
    }

    protected override async void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        try
        {
            base.OnPropertyChanged(change);
            if (change.Property != ModelStatusProperty) return;

            OldIcon = Icon;
            Icon = new Bitmap(AssetLoader.Open(new Uri(_imagesUri[(int)ModelStatus])));

            await _cancellationTokenSource.CancelAsync();
            CreateNewTokenSource();
            CreateFadeInAnimation();
            CreateFadeOutAnimation();

            if (_image is null || _animImg is null) return;
            var animTask1 = _fadeOutAnimation.RunAsync(_animImg, _cancellationToken);
            var animTask2 = _fadeInAnimation.RunAsync(_image, _cancellationToken);
            await Task.WhenAll(animTask1, animTask2);
        }
        catch (Exception)
        {
            // ignored
        }
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _animImg = e.NameScope.Find<Image>(AnimatedImageName) ??
                   throw new Exception($"Can't find control with name {AnimatedImageName}");
        _image = e.NameScope.Find<Image>(ImageName) ?? throw new Exception($"Can't find control with name {ImageName}");
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        OnAnimationTick += ExecuteRunningAnimation;
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        OnAnimationTick -= ExecuteRunningAnimation;
    }

    private void CreateNewTokenSource()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _cancellationToken = _cancellationTokenSource.Token;
    }

    private async void ExecuteRunningAnimation()
    {
        try
        {
            if (_image is null) return;
            _image.Classes.Add("RunningAnim");
            await Task.Delay(5000, _cancellationToken);
        }
        catch (Exception)
        {
            // Ignore
        }
        finally
        {
            _image?.Classes.Remove("RunningAnim");
        }
    }
}