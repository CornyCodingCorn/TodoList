using System;
using System.Globalization;
using Avalonia.Data.Converters;
using ToDoList.Models;

namespace ToDoList.Converter;

public class TaskStatusToString : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is TaskStatus status)
            return status switch
            {
                TaskStatus.NotStarted => "NotStarted",
                TaskStatus.Started => "Started",
                TaskStatus.Done => "Done",
                _ => throw new ArgumentOutOfRangeException()
            };

        return "Unknown";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}