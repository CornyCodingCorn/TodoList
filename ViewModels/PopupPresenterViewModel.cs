using System;
using System.Threading.Tasks;
using Avalonia.Logging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ToDoList.ViewModels;

public partial class PopupPresenterViewModel : ViewModelBase
{
    [ObservableProperty] private bool _isShowingDialog;
    [ObservableProperty] private object? _popupContent;
    [ObservableProperty] private bool? _isStartedAnimating;

    private static TaskCompletionSource? _taskCompletionSource;
    
    public async Task ShowPopupAsync(object? content = null)
    {
        if (content is not null)
            SetPopupContent(content);
        else
            Logger.Sink?.Log(LogEventLevel.Warning, "Popup", this, "Showing old popup content because content is null.");   
        
        IsStartedAnimating = true;
        IsShowingDialog = true;

        _taskCompletionSource = new TaskCompletionSource();
        await _taskCompletionSource.Task;
    }

    public void HidePopup()
    {
        IsStartedAnimating = true;
        IsShowingDialog = false;
        _taskCompletionSource?.SetResult();
        _taskCompletionSource = null;
    }

    public object? GetPopupContent()
    {
        return PopupContent;
    }

    public bool SetPopupContent(object? content)
    {
        if (IsShowingDialog) return false;
        PopupContent = content;
        return true;
    }
}