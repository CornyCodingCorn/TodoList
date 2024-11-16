using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using Avalonia.Rendering;
using CommunityToolkit.Mvvm.Input;
using ShimSkiaSharp;

namespace ToDoList.Controls;

public partial class PopupPresenter : TemplatedControlForAnimation
{
    #region Styled properties

    public static readonly StyledProperty<IDataTemplate?> ContentTemplateProperty =
        AvaloniaProperty.Register<PopupPresenter, IDataTemplate?>(
            nameof(ContentTemplate));

    public IDataTemplate? ContentTemplate
    {
        get => GetValue(ContentTemplateProperty);
        set => SetValue(ContentTemplateProperty, value);
    }

    public static readonly StyledProperty<object?> ContentProperty = AvaloniaProperty.Register<PopupPresenter, object?>(
        nameof(Content));

    [Content]
    [DependsOn(nameof(ContentTemplate))]
    public object? Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    public static readonly StyledProperty<object?> PopupContentProperty =
        AvaloniaProperty.Register<PopupPresenter, object?>(
            nameof(PopupContent));

    public object? PopupContent
    {
        get => GetValue(PopupContentProperty);
        set => SetValue(PopupContentProperty, value);
    }

    public static readonly StyledProperty<bool> IsShowingDialogProperty =
        AvaloniaProperty.Register<PopupPresenter, bool>(
            nameof(IsShowingDialog));

    public bool IsShowingDialog
    {
        get => GetValue(IsShowingDialogProperty);
        set => SetValue(IsShowingDialogProperty, value);
    }

    #endregion

    private static PopupPresenter? _instance;

    private static PopupPresenter Instance
    {
        get => _instance ?? throw new NullReferenceException("Popup Presenter instance was not initialized!");
        set
        {
            if (_instance is not null) throw new Exception("Popup Presenter instance was already initialized!");
            _instance = value;
        }
    }

    public static void ShowPopup()
    {
        Instance.StartAnimating();
        Instance.IsShowingDialog = true;
    }

    public static void HidePopup()
    {
        Instance.StartAnimating();
        Instance.IsShowingDialog = false;
    }

    [RelayCommand]
    private void RejectDialog()
    {
        Console.WriteLine("Rejected dialog option");
        Instance.IsShowingDialog = false;
    }

    public PopupPresenter()
    {
        Instance = this;
        var dialog = new PopupDialog();
        Instance.PopupContent = dialog;
        dialog.Content = new TextBlock { Text = "Hello this is a text box" };
        dialog.AcceptCommand = dialog.RejectCommand = Instance.RejectDialogCommand;
    }
}