using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace ToDoList.Converter;

public class NotBooleanConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not bool bValue || targetType != typeof(bool))
            throw new Exception("This converter only works on boolean types");
        return !bValue;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not bool bValue || targetType != typeof(bool))
            throw new Exception("This converter only works on boolean types");
        return !bValue;
    }
}