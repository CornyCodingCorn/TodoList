using System;
using System.Threading;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Media;
using Avalonia.Styling;

namespace ToDoList.Controls;

public partial class TaskStatusIcon
{
    private void CreateFadeOutAnimation()
    {
        _fadeOutAnimation = new Animation
        {
            Duration = TimeSpan.FromMilliseconds(350d),
            IterationCount = new IterationCount(1u),
            FillMode = FillMode.None,
            SpeedRatio = 1d
        };

        {
            var keyframe1 = new KeyFrame
            {
                Cue = new Cue(0),
            };
            keyframe1.Setters.Add(new Setter(ScaleTransform.ScaleXProperty, DefaultScale));
            keyframe1.Setters.Add(new Setter(ScaleTransform.ScaleYProperty, DefaultScale));
            keyframe1.Setters.Add(new Setter(RotateTransform.AngleProperty, 0d));
            keyframe1.Setters.Add(new Setter(IsVisibleProperty, true));
            _fadeOutAnimation.Children.Add(keyframe1);
        }

        {
            var keyframe2 = new KeyFrame
            {
                Cue = new Cue(1)
            };
            keyframe2.Setters.Add(new Setter(ScaleTransform.ScaleXProperty, 0d));
            keyframe2.Setters.Add(new Setter(ScaleTransform.ScaleYProperty, 0d));
            keyframe2.Setters.Add(new Setter(RotateTransform.AngleProperty, 360d));
            _fadeOutAnimation.Children.Add(keyframe2);
        }
    }

    private void CreateFadeInAnimation()
    {
        _fadeInAnimation = new Animation
        {
            Duration = TimeSpan.FromMilliseconds(700d),
            IterationCount = new  IterationCount(1u),
            FillMode = FillMode.Forward,
            SpeedRatio = 1d
        };

        {
            var keyframe1 = new KeyFrame
            {
                Cue = new Cue(0)
            };
            keyframe1.Setters.Add(new Setter(ScaleTransform.ScaleXProperty, 0d));
            keyframe1.Setters.Add(new Setter(ScaleTransform.ScaleYProperty, 0d));
            keyframe1.Setters.Add(new Setter(RotateTransform.AngleProperty, -180d));
            keyframe1.Setters.Add(new Setter(IsVisibleProperty, false));
            _fadeInAnimation.Children.Add(keyframe1);
        }

        {
            var keyframe2 = new KeyFrame

            {
                Cue = new Cue(0.5)
            };
            keyframe2.Setters.Add(new Setter(ScaleTransform.ScaleXProperty, 0d));
            keyframe2.Setters.Add(new Setter(ScaleTransform.ScaleYProperty, 0d));
            keyframe2.Setters.Add(new Setter(RotateTransform.AngleProperty, -180d));
            keyframe2.Setters.Add(new Setter(IsVisibleProperty, true));
            _fadeInAnimation.Children.Add(keyframe2);
        }

        {
            var keyframe3 = new KeyFrame
            {
                Cue = new Cue(0.85),
            };
            keyframe3.Setters.Add(new Setter(ScaleTransform.ScaleXProperty, DefaultScale * 1.2));
            keyframe3.Setters.Add(new Setter(ScaleTransform.ScaleYProperty, DefaultScale * 1.2));
            keyframe3.Setters.Add(new Setter(RotateTransform.AngleProperty, 0d));
            _fadeInAnimation.Children.Add(keyframe3);
        }

        {
            var keyframe4 = new KeyFrame
            {
                Cue = new Cue(1)
            };
            keyframe4.Setters.Add(new Setter(ScaleTransform.ScaleXProperty, DefaultScale));
            keyframe4.Setters.Add(new Setter(ScaleTransform.ScaleYProperty, DefaultScale));
            _fadeInAnimation.Children.Add(keyframe4);
        }
    }

    private void CreateRunningAnimation()
    {
        _runningAnimation = new Animation
        {
            FillMode = FillMode.None,
            Duration = TimeSpan.FromMilliseconds(3000d),
            IterationCount = new IterationCount(1u),
            Easing = new QuarticEaseInOut()
        };

        {
            var keyframe = new KeyFrame
            {
                Cue = new Cue(0.0)
            };
            keyframe.Setters.Add(new Setter(ScaleTransform.ScaleXProperty, DefaultScale));
            keyframe.Setters.Add(new Setter(ScaleTransform.ScaleYProperty, DefaultScale));
            keyframe.Setters.Add(new Setter(RotateTransform.AngleProperty, 0d));
            
            _runningAnimation.Children.Add(keyframe);
        }
        {
            var keyframe = new KeyFrame
            {
                Cue = new Cue(0.1)
            };
            keyframe.Setters.Add(new Setter(ScaleTransform.ScaleXProperty, DefaultScale));
            keyframe.Setters.Add(new Setter(ScaleTransform.ScaleYProperty, DefaultScale));
            keyframe.Setters.Add(new Setter(RotateTransform.AngleProperty, -45d));
            
            _runningAnimation.Children.Add(keyframe);
        }
        {
            var keyframe = new KeyFrame
            {
                Cue = new Cue(0.5)
            };
            keyframe.Setters.Add(new Setter(ScaleTransform.ScaleXProperty, DefaultScale * 1.2));
            keyframe.Setters.Add(new Setter(ScaleTransform.ScaleYProperty, DefaultScale * 1.2));
            keyframe.Setters.Add(new Setter(RotateTransform.AngleProperty, 180d));
            
            _runningAnimation.Children.Add(keyframe);
        }
        {
            var keyframe = new KeyFrame()
            {
                Cue = new Cue(1)
            };
            keyframe.Setters.Add(new Setter(ScaleTransform.ScaleXProperty, DefaultScale));
            keyframe.Setters.Add(new Setter(ScaleTransform.ScaleYProperty, DefaultScale));
            keyframe.Setters.Add(new Setter(RotateTransform.AngleProperty, 360d));
            
            _runningAnimation.Children.Add(keyframe);
        }
    }
}