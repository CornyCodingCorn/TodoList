using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ToDoList.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private enum DialogType
    {
        YesNo,
        Confirmation
    }
    
    private static MainWindowViewModel? _instance;
    public static MainWindowViewModel Instance
    {
        get => _instance ?? throw new NullReferenceException("MainWindowViewModel instance was not initialized!");
        set
        {
            if (_instance is not null) throw new Exception("MainWindowViewModel instance was already initialized!");
            _instance = value;
        }
    }

    [ObservableProperty] private string _popupTitle = "Default title";
    [ObservableProperty] private string _popupMessage = "Default message";
    [ObservableProperty] private object? _yesNoPopupDialog;
    [ObservableProperty] private object? _confirmationPopupDialog;
    
    private int _popupResultValue;

    public static async Task<int> ShowConfirmationDialogAsync(string title, string message)
    {
        return await Instance.ShowDialogAsync(title, message, DialogType.Confirmation);
    }

    public static async Task<int> ShowYesNoDialogAsync(string title, string message)
    {
        return await Instance.ShowDialogAsync(title, message, DialogType.YesNo);
    }

    public MainWindowViewModel()
    {
        Instance = this;
    }

    [RelayCommand]
    private void HandlePopupConfirmation(int value)
    {
        _popupResultValue = value;
        PopupPresenterViewModel.HidePopup();
    }
    
    private async Task<int> ShowDialogAsync(string title, string message, DialogType dialogType)
    {
        PopupTitle = title;
        PopupMessage = message;
        
        await PopupPresenterViewModel.ShowPopupAsync(dialogType switch
        {
            DialogType.YesNo => YesNoPopupDialog,
            DialogType.Confirmation => ConfirmationPopupDialog,
            _ => throw new ArgumentOutOfRangeException(nameof(dialogType))
        });
        
        return _popupResultValue;
    }
}