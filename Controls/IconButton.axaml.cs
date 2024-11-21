using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Converters;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Metadata;
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

    public static readonly StyledProperty<double> IconSizeProperty = AvaloniaProperty.Register<IconButton, double>(
        nameof(IconSize), defaultValue: 14);

    public double IconSize
    {
        get => GetValue(IconSizeProperty);
        set => SetValue(IconSizeProperty, value);
    }

    public static readonly StyledProperty<double> ImagePaddingProperty = AvaloniaProperty.Register<IconButton, double>(
        nameof(ImagePadding), defaultValue: 5);

    public double ImagePadding
    {
        get => GetValue(ImagePaddingProperty);
        set => SetValue(ImagePaddingProperty, value);
    }

    public static readonly StyledProperty<double> ImageDisabledOpacityProperty = AvaloniaProperty.Register<IconButton, double>(
        nameof(ImageDisabledOpacity), defaultValue: 0.3);

    public double ImageDisabledOpacity
    {
        get => GetValue(ImageDisabledOpacityProperty);
        set => SetValue(ImageDisabledOpacityProperty, value);
    }
}