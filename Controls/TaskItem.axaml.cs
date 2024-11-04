using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using CommunityToolkit.Mvvm.Input;
using ToDoList.Models;

namespace ToDoList.Controls;

public partial class TaskItem : TemplatedControl
{
    public static readonly StyledProperty<ICommand> DeleteCommandProperty =
        AvaloniaProperty.Register<TaskItem, ICommand>(nameof(DeleteCommand), defaultValue: new  RelayCommand(() => {}));

    public ICommand DeleteCommand
    {
        get => GetValue(DeleteCommandProperty);
        set => SetValue(DeleteCommandProperty, value);
    }

    [RelayCommand]
    private void UpdateStatus()
    {
        if (DataContext is not TaskModel taskModel) return;

        taskModel.Status = (TaskModelStatus)((int)(taskModel.Status + 1) % Enum.GetValues<TaskModelStatus>().Length);
        Console.WriteLine($"Current status of {taskModel.Name} is {taskModel.Status}");
    }
}