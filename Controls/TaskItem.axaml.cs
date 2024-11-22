using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using ToDoList.Models;

namespace ToDoList.Controls;

public class TaskItem : TemplatedControl
{
    public static readonly StyledProperty<bool> IsArchivedProperty = AvaloniaProperty.Register<TaskItem, bool>(
        nameof(IsArchived));

    public bool IsArchived
    {
        get => GetValue(IsArchivedProperty);
        set => SetValue(IsArchivedProperty, value);
    }
    
    public static readonly StyledProperty<bool> IsDeletingProperty = AvaloniaProperty.Register<TaskItem, bool>(
        nameof(IsDeleting));

    public bool IsDeleting
    {
        get => GetValue(IsDeletingProperty);
        set => SetValue(IsDeletingProperty, value);
    }
    
    public static readonly StyledProperty<TaskModelStatus> ModelStatusProperty =
        AvaloniaProperty.Register<TaskItem, TaskModelStatus>(nameof(ModelStatus),
            defaultValue: TaskModelStatus.NotStarted);

    public static readonly StyledProperty<bool> IsEditingProperty =
        AvaloniaProperty.Register<TaskItem, bool>(nameof(IsEditing));

    public static readonly StyledProperty<bool> IsExpandedProperty = AvaloniaProperty.Register<TaskItem, bool>(
        nameof(IsExpanded));

    private Expander _expander = null!;
    private const string ExpanderAnimationClass = "runAnim";

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
    
    public bool IsExpanded
    {
        get => GetValue(IsExpandedProperty);
        set => SetValue(IsExpandedProperty, value);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _expander = e.NameScope.Get<Expander>("ExpanderPart") ??
                    throw new Exception("Expander not found in TaskItem control");
        _expander.PropertyChanged += HandleExpansionChangedEvent;
    }

    private void HandleExpansionChangedEvent(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property != Expander.IsExpandedProperty)
            return;

        if (!_expander.Classes.Contains(ExpanderAnimationClass))
        {
            _expander.Classes.Add(ExpanderAnimationClass);
            _expander.PropertyChanged -= HandleExpansionChangedEvent;
        }
    }
}