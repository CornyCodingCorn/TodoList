using CommunityToolkit.Mvvm.Input;
using ToDoList.Controls;

namespace ToDoList.ViewModels;

public partial class DashboardViewModel : ViewModelBase
{
    [RelayCommand]
    private void ShowPopupDialog()
    {
        PopupPresenterViewModel.ShowPopupAsync();
    }
}