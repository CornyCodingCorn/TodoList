using Avalonia;
using Avalonia.Controls.Primitives;
using ToDoList.Models;

namespace ToDoList.Controls;

public class TaskItem : TemplatedControl
{
    public static readonly StyledProperty<TaskModelStatus> ModelStatusProperty =
        AvaloniaProperty.Register<TaskItem, TaskModelStatus>(nameof(ModelStatus), defaultValue: TaskModelStatus.NotStarted);
    public static readonly StyledProperty<bool> IsEditingProperty =
        AvaloniaProperty.Register<TaskItem, bool>(nameof(IsEditing));

    public TaskModelStatus ModelStatus
    {
        get => GetValue(ModelStatusProperty);
        set => SetValue(ModelStatusProperty, value);
    }
    public bool IsEditing
    {
        get => GetValue(IsEditingProperty);
        set => SetValue(IsEditingProperty, value);
    }
}