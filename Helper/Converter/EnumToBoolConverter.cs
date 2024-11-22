using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace ToDoList.Helper.Converter;

public class EnumToBoolConverter<T> : IValueConverter where T : Enum
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not T enumValue) throw new Exception("Value is not of type enum");
        if (targetType.IsAssignableTo(typeof(bool))) throw new Exception("Target type is not of type boolean");
        if (parameter is not T param) throw new Exception("Parameter is not of type enum");

        return enumValue.Equals(param);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new Exception("You're not supposed to use this converter in 2 ways binding");
    }
}