using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;

namespace ToDoList.Views;

public partial class TasksTabView : UserControl
{
    public static readonly StyledProperty<object> ConfirmationDialogProperty = AvaloniaProperty.Register<TasksTabView, object>(
        nameof(ConfirmationDialog));

    public object ConfirmationDialog
    {
        get => GetValue(ConfirmationDialogProperty);
        set => SetValue(ConfirmationDialogProperty, value);
    }
    public TasksTabView()
    {
        InitializeComponent();
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        Resources.TryGetResource("ConfirmationDialogPopup", null, out var dialog);
        ConfirmationDialog = dialog ?? throw new Exception("ConfirmationDialog is null for tasks tab view is null");
        ((Control)ConfirmationDialog).DataContext = DataContext;
    }
}