using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ToDoList.Controls;
using ToDoList.ViewModels.Helpers;

namespace ToDoList.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private enum DialogType
    {
        YesNo,
        Confirmation
    }

    [ObservableProperty] private string _popupTitle = "Default title";
    [ObservableProperty] private string _popupMessage = "Default message";
    [ObservableProperty] private object? _yesNoPopupDialog;
    [ObservableProperty] private object? _confirmationPopupDialog;
    [ObservableProperty] private PopupPresenterViewModel _popupPresenterViewModel = null!;
    [ObservableProperty] private MainViewModel _mainViewModel = null!;
    
    private int _popupResultValue;

    public async Task<int> ShowConfirmationDialogAsync(string title, string message)
    {
        return await ShowDialogAsync(title, message, DialogType.Confirmation);
    }

    public async Task<int> ShowYesNoDialogAsync(string title, string message)
    {
        return await ShowDialogAsync(title, message, DialogType.YesNo);
    }

    public override void Initialize(ImmutableDictionary<Type, IService> services)
    {
        PopupPresenterViewModel = services.Get(typeof(PopupPresenterViewModel)) as PopupPresenterViewModel ?? throw new NullReferenceException("PopupPresenter was not initialized!");
        MainViewModel = services.Get<MainViewModel>(typeof(MainViewModel));
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