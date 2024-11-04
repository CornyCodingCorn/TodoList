using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using CommunityToolkit.Mvvm.Input;

namespace ToDoList.Controls;

public class TaskItem : TemplatedControl
{
    public static readonly StyledProperty<ICommand> DeleteCommandProperty =
        AvaloniaProperty.Register<TaskItem, ICommand>(nameof(DeleteCommand), defaultValue: new  RelayCommand(() => {}));

    public ICommand DeleteCommand
    {
        get => GetValue(DeleteCommandProperty);
        set => SetValue(DeleteCommandProperty, value);
    }
}