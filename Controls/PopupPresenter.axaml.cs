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
using ToDoList.ViewModels;

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

    public static readonly StyledProperty<bool> IsStartedAnimatingProperty = AvaloniaProperty.Register<PopupPresenter, bool>(
        nameof(IsStartedAnimating));

    public bool IsStartedAnimating
    {
        get => GetValue(IsStartedAnimatingProperty);
        set => SetValue(IsStartedAnimatingProperty, value);
    }
    #endregion

    public PopupPresenter()
    {
        DataContext = Program.DependencyInjector.Services.Get(typeof(PopupPresenterViewModel));
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property != IsStartedAnimatingProperty)
            return;
        
        StartAnimating();
    }
}