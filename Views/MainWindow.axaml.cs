using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;

namespace ToDoList.Views;

public partial class MainWindow : Window
{
    public static readonly StyledProperty<object> ConfirmationDialogProperty = AvaloniaProperty.Register<TasksTabView, object>(
        nameof(ConfirmationDialog));

    public object ConfirmationDialog
    {
        get => GetValue(ConfirmationDialogProperty);
        set => SetValue(ConfirmationDialogProperty, value);
    }

    public static readonly StyledProperty<object> YesNoPopupDialogProperty = AvaloniaProperty.Register<MainWindow, object>(
        nameof(YesNoPopupDialog));

    public object YesNoPopupDialog
    {
        get => GetValue(YesNoPopupDialogProperty);
        set => SetValue(YesNoPopupDialogProperty, value);
    }
    
    public MainWindow()
    {
        InitializeComponent();

#if DEBUG
        this.AttachDevTools(new KeyGesture(Key.F9));
#endif
    }
    
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        Resources.TryGetResource("YesNoPopupDialog", null, out var yesNoDialog);
        YesNoPopupDialog = yesNoDialog ?? throw new Exception("YesNoPopupDialog is null for tasks tab view is null");
        ((Control)YesNoPopupDialog).DataContext = DataContext;
        
        Resources.TryGetResource("ConfirmationPopupDialog", null, out var confirmationDialog);
        ConfirmationDialog = confirmationDialog ?? throw new Exception("ConfirmationDialog is null for tasks tab view is null");
        ((Control)ConfirmationDialog).DataContext = DataContext;
    }
}