using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using ToDoList.Controls;

namespace ToDoList.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private HamburgerMenuItem _selectedMenuItem = null!;
    [ObservableProperty] private ObservableCollection<HamburgerMenuItem> _bodyMenuItems = null!;
    [ObservableProperty] private ObservableCollection<HamburgerMenuItem> _footerMenuItems = null!;
    [ObservableProperty] private ViewModelBase _currentViewModel = null!;

    private readonly List<ViewModelBase> _viewModels = [];

    public MainViewModel()
    {
        LoadItems();
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.PropertyName == nameof(SelectedMenuItem)) HandleSelectedMenuItemChanged();
    }

    private void HandleSelectedMenuItemChanged()
    {
        if (_viewModels.All(x => x.GetType() != SelectedMenuItem.Type))
        {
            _viewModels.Add(Activator.CreateInstance(SelectedMenuItem.Type) as ViewModelBase
                            ?? throw new InvalidOperationException(
                                "HamburgerMenuItem has type that isn't from ViewModelBase"));
        }

        CurrentViewModel = _viewModels.Single(x => x.GetType() == SelectedMenuItem.Type);
    }

    private void LoadItems()
    {
        BodyMenuItems =
        [
            new("Dashboard", GetMenuIconUrl("dashboard-128.png"), typeof(DashboardViewModel)),
            new("Tasks", GetMenuIconUrl("tasks-128.png"), typeof(TasksTabViewModel)),
            new("Planning", GetMenuIconUrl("calendar-128.png"), typeof(PlanningViewModel)),
            new("Archive", GetMenuIconUrl("archive-128.png"), typeof(ArchiveViewModel)),
        ];
        FooterMenuItems =
        [
            new("Settings", GetMenuIconUrl("settings-128.png"), typeof(SettingsViewModel)),
            new("Information", GetMenuIconUrl("information-128.png"), typeof(InformationViewModel))
        ];
    }

    private string GetMenuIconUrl(string imageFileName)
    {
        return $"avares://ToDoList/Assets/MenuIcons/{imageFileName}";
    }
}