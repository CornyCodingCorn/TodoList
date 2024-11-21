using System;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;

namespace ToDoList.Converter;

[Flags]
public enum DoubleToThicknessType
{
    Top = 0b1000,
    Left = 0b0100,
    Bottom = 0b0010,
    Right = 0b0001,
    TopBottom = Top | Bottom,
    LeftRight = Left | Right,
    TopLeft = Top | Left,
    BottomRight = Bottom | Right,
    TopRight = Top | Right,
    BottomLeft = Bottom | Left,
    All = Top | Left | Bottom | Right
}

public class DoubleToThicknessConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value?.GetType() != typeof(double)) throw new Exception("DoubleToThicknessConverter can only convert from a double");
        if (parameter?.GetType() != typeof(DoubleToThicknessType)) throw new Exception("DoubleToThicknessConverter can only accept DoubleToThicknessType as a parameter");
        var doubleValue = (double)value;
        var type = (DoubleToThicknessType)parameter;
        double[] thicknessDouble = [0, 0, 0, 0];
        
        if ((type & DoubleToThicknessType.Top) != 0)
            thicknessDouble[0] = doubleValue;
        if ((type & DoubleToThicknessType.Left) != 0)
            thicknessDouble[1] = doubleValue;
        if ((type & DoubleToThicknessType.Bottom) != 0)
            thicknessDouble[2] = doubleValue;
        if ((type & DoubleToThicknessType.Right) != 0)
            thicknessDouble[3] = doubleValue;
        
        return new Thickness(thicknessDouble[1], thicknessDouble[0], thicknessDouble[3], thicknessDouble[2]);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value?.GetType() != typeof(Thickness)) throw new Exception("DoubleToThicknessConverter can only convert from a Thickness");
        if (parameter?.GetType() != typeof(DoubleToThicknessType)) throw new Exception("DoubleToThicknessConverter can only accept DoubleToThicknessType as a parameter");

        var type = (DoubleToThicknessType)parameter;
        var thickness = (Thickness)value;
        return type switch
        {
            DoubleToThicknessType.Top or DoubleToThicknessType.TopBottom => thickness.Top,
            DoubleToThicknessType.Bottom => thickness.Bottom,
            DoubleToThicknessType.Right or DoubleToThicknessType.BottomRight or DoubleToThicknessType.TopRight => thickness.Right,
            _ => thickness.Left
        };
    }
}