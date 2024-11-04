using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Styling;
using ToDoList.Models;

namespace ToDoList.Controls;

public class TaskStatusIcon : TemplatedControl
{
    public static readonly StyledProperty<TaskModelStatus> ModelStatusProperty =
        AvaloniaProperty.Register<TaskStatusIcon, TaskModelStatus>(nameof(ModelStatus), defaultValue: TaskModelStatus.NotStarted);
    public static readonly StyledProperty<Bitmap> IconProperty =
        AvaloniaProperty.Register<TaskStatusIcon, Bitmap>(nameof(Icon), defaultValue: new Bitmap(AssetLoader.Open(new Uri(DefaultUri))));
    public static readonly StyledProperty<Bitmap> OldIconProperty =
        AvaloniaProperty.Register<TaskStatusIcon, Bitmap>(nameof(OldIcon), defaultValue: new Bitmap(AssetLoader.Open(new Uri(DefaultUri))));

    private const string AnimatedImageName = "AnimImg";
    private const string ImageName = "Image";
    
    private Animation _fadeOutAnimation = null!;
    private Animation _fadeInAnimation = null!;
    private Animation _runningAnimation = null!;
    private Image? _animImg;
    private Image? _image;
    
    private const string DefaultUri = "avares://ToDoList/Assets/Icons/cross-64.png";
    private readonly string[] _imagesUri = [
        "avares://ToDoList/Assets/Icons/cross-64.png",
        "avares://ToDoList/Assets/Icons/gear-64.png",
        "avares://ToDoList/Assets/Icons/accept-64.png"
    ];

    public TaskStatusIcon()
    {
        CreateFadeInAnimation();
        CreateFadeOutAnimation();
        CreateRunningAnimation();
    }

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

    public TaskModelStatus ModelStatus
    {
        get => GetValue(ModelStatusProperty);
        set => SetValue(ModelStatusProperty, value);
    }

    protected async override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property != ModelStatusProperty) return;

        OldIcon = Icon;
        Icon = new Bitmap(AssetLoader.Open(new Uri(_imagesUri[(int)ModelStatus])));

        if (_image is null || _animImg is null) return;
        await _fadeOutAnimation.RunAsync(this);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _animImg = e.NameScope.Find<Image>(AnimatedImageName) ?? throw new Exception($"Can't find control with name {AnimatedImageName}");
        _image = e.NameScope.Find<Image>(ImageName) ?? throw new Exception($"Can't find control with name {ImageName}");
    }

    private void CreateFadeOutAnimation()
    {
        _fadeOutAnimation = new Animation
        {
            Duration = TimeSpan.FromMilliseconds(5000),
            IterationCount = new IterationCount(1),
            SpeedRatio = 1
        };

        var keyframe1 = new KeyFrame
        {
            Cue = new Cue(0),
        };
        // keyframe1.Setters.Add(new Setter(ScaleTransform.ScaleXProperty, 1.0));
        // keyframe1.Setters.Add(new Setter(ScaleTransform.ScaleXProperty, 1.0));
        keyframe1.Setters.Add(new Setter(RotateTransform.AngleProperty, 0));

        var keyframe2 = new KeyFrame
        {
            Cue = new Cue(1)
        };
        // keyframe2.Setters.Add(new Setter(ScaleTransform.ScaleXProperty, 0));
        // keyframe2.Setters.Add(new Setter(ScaleTransform.ScaleYProperty, 0));
        keyframe2.Setters.Add(new Setter(RotateTransform.AngleProperty, 360));
        
        _fadeOutAnimation.Children.Add(keyframe1);
        _fadeOutAnimation.Children.Add(keyframe2);
    }

    private void CreateFadeInAnimation()
    {
        _fadeInAnimation = new Animation
        {
            Duration = TimeSpan.FromMilliseconds(10000),
        };

        {
            var keyframe1 = new KeyFrame
            {
                Cue = new Cue(0)
            };
            keyframe1.Setters.Add(new Setter(ScaleTransform.ScaleXProperty, 0));
            keyframe1.Setters.Add(new Setter(ScaleTransform.ScaleYProperty, 0));
            keyframe1.Setters.Add(new Setter(RotateTransform.AngleProperty, 180));
            _fadeInAnimation.Children.Add(keyframe1);
        }

        {
            var keyframe2 = new KeyFrame
                
            {
                Cue = new Cue(0.5)
            };
            keyframe2.Setters.Add(new Setter(ScaleTransform.ScaleXProperty, 0));
            keyframe2.Setters.Add(new Setter(ScaleTransform.ScaleYProperty, 0));
            keyframe2.Setters.Add(new Setter(RotateTransform.AngleProperty, 180));
            _fadeInAnimation.Children.Add(keyframe2);
        }

        {
            var keyframe3 = new KeyFrame
            {
                Cue = new Cue(0.85),
            };
            keyframe3.Setters.Add(new Setter(ScaleTransform.ScaleXProperty, 1.3));
            keyframe3.Setters.Add(new Setter(ScaleTransform.ScaleXProperty, 1.3));
            keyframe3.Setters.Add(new Setter(RotateTransform.AngleProperty, 360));
            _fadeInAnimation.Children.Add(keyframe3);
        }

        {
            var keyframe4 = new KeyFrame
            {
                Cue = new Cue(1)
            };
            keyframe4.Setters.Add(new Setter(ScaleTransform.ScaleXProperty, 1));
            keyframe4.Setters.Add(new Setter(ScaleTransform.ScaleYProperty, 1));
            _fadeInAnimation.Children.Add(keyframe4);
        }
    }

    private void CreateRunningAnimation()
    {
        
    }
}