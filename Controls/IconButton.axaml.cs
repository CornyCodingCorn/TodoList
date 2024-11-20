using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Styling;

namespace ToDoList.Controls;

public enum IconButtonImagePosition
{
    Top,
    Left,
    Bottom,
    Right
}

public class IconButton : Button
{
    public static readonly StyledProperty<IconButtonImagePosition> ImagePositionProperty = AvaloniaProperty.Register<IconButton, IconButtonImagePosition>(
        nameof(ImagePosition));

    public IconButtonImagePosition ImagePosition
    {
        get => GetValue(ImagePositionProperty);
        set => SetValue(ImagePositionProperty, value);
    }
    
    public static readonly StyledProperty<IImage> ImageSourceProperty = AvaloniaProperty.Register<IconButton, IImage>(
        nameof(ImageSource));

    public IImage ImageSource
    {
        get => GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }
}