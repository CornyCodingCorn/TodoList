using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace ToDoList.Controls;

public class PopupDialog : ContentControl
{
    #region StyledProperty
    public static readonly StyledProperty<ICommand?> AcceptCommandProperty = AvaloniaProperty.Register<PopupDialog, ICommand?>(
        nameof(AcceptCommand));

    public ICommand? AcceptCommand
    {
        get => GetValue(AcceptCommandProperty);
        set => SetValue(AcceptCommandProperty, value);
    }

    public static readonly StyledProperty<ICommand?> RejectCommandProperty = AvaloniaProperty.Register<PopupDialog, ICommand?>(
        nameof(RejectCommand));

    public ICommand? RejectCommand
    {
        get => GetValue(RejectCommandProperty);
        set => SetValue(RejectCommandProperty, value);
    }
    #endregion
}