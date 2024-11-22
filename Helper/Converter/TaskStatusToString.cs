using System;
using System.Globalization;
using Avalonia.Data.Converters;
using ToDoList.Models;

namespace ToDoList.Helper.Converter;

public class TaskStatusToString : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is TaskModelStatus status)
            return status switch
            {
                TaskModelStatus.NotStarted => "NotStarted",
                TaskModelStatus.Started => "Started",
                TaskModelStatus.Done => "Done",
                _ => throw new ArgumentOutOfRangeException()
            };

        return "Unknown";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}