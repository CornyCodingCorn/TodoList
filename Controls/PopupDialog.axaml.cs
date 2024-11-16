using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Styling;

namespace ToDoList.Controls;

public class PopupDialog : ContentControl
{
    #region StyledProperty

    public static readonly StyledProperty<string> TitleProperty = AvaloniaProperty.Register<PopupDialog, string>(
        nameof(Title));

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    
    public static readonly StyledProperty<ICommand?> AcceptCommandProperty = AvaloniaProperty.Register<PopupDialog, ICommand?>(
        nameof(AcceptCommand));

    public ICommand? AcceptCommand
    {
        get => GetValue(AcceptCommandProperty);
        set => SetValue(AcceptCommandProperty, value);
    }

    public static readonly StyledProperty<ICommand?> CancelCommandProperty = AvaloniaProperty.Register<PopupDialog, ICommand?>(
        nameof(CancelCommand));

    public ICommand? CancelCommand
    {
        get => GetValue(CancelCommandProperty);
        set => SetValue(CancelCommandProperty, value);
    }

    public static readonly StyledProperty<string> AcceptButtonTextProperty = AvaloniaProperty.Register<PopupDialog, string>(
        nameof(AcceptButtonText), defaultValue: "Accept");

    public string AcceptButtonText
    {
        get => GetValue(AcceptButtonTextProperty);
        set => SetValue(AcceptButtonTextProperty, value);
    }

    public static readonly StyledProperty<string> CancelButtonTextProperty = AvaloniaProperty.Register<PopupDialog, string>(
        nameof(CancelButtonText), defaultValue: "Cancel");

    public string CancelButtonText
    {
        get => GetValue(CancelButtonTextProperty);
        set => SetValue(CancelButtonTextProperty, value);
    }

    public static readonly StyledProperty<bool> ShowAcceptButtonProperty = AvaloniaProperty.Register<PopupDialog, bool>(
        nameof(ShowAcceptButton), defaultValue: true);

    public bool ShowAcceptButton
    {
        get => GetValue(ShowAcceptButtonProperty);
        set => SetValue(ShowAcceptButtonProperty, value);
    }

    public static readonly StyledProperty<bool> ShowCancelButtonProperty = AvaloniaProperty.Register<PopupDialog, bool>(
        nameof(ShowCancelButton), defaultValue: true);

    public bool ShowCancelButton
    {
        get => GetValue(ShowCancelButtonProperty);
        set => SetValue(ShowCancelButtonProperty, value);
    }
    #endregion
}