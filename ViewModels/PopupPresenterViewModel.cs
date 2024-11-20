using System;
using System.Threading.Tasks;
using Avalonia.Logging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ToDoList.ViewModels;

public partial class PopupPresenterViewModel : ViewModelBase
{
    private static PopupPresenterViewModel? _instance;

    [ObservableProperty] private bool _isShowingDialog;
    [ObservableProperty] private object? _popupContent;
    [ObservableProperty] private bool? _isStartedAnimating;

    private static TaskCompletionSource? _taskCompletionSource;
    
    private static PopupPresenterViewModel Instance
    {
        get => _instance ?? throw new NullReferenceException("Popup Presenter instance was not initialized!");
        set
        {
            if (_instance is not null) throw new Exception("Popup Presenter instance was already initialized!");
            _instance = value;
        }
    }

    public static async Task ShowPopupAsync(object? content = null)
    {
        if (content is not null)
            SetPopupContent(content);
        else
            Logger.Sink?.Log(LogEventLevel.Warning, "Popup", Instance, "Showing old popup content because content is null.");   
        
        Instance.IsStartedAnimating = true;
        Instance.IsShowingDialog = true;

        _taskCompletionSource = new TaskCompletionSource();
        await _taskCompletionSource.Task;
    }

    public static void HidePopup()
    {
        Instance.IsStartedAnimating = true;
        Instance.IsShowingDialog = false;
        _taskCompletionSource?.SetResult();
        _taskCompletionSource = null;
    }

    public static object? GetPopupContent()
    {
        return Instance.PopupContent;
    }

    public static bool SetPopupContent(object? content)
    {
        if (Instance.IsShowingDialog) return false;
        Instance.PopupContent = content;
        return true;
    }

    public PopupPresenterViewModel()
    {
        Instance = this;
    }
}