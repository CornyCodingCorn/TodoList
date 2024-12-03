using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using ToDoList.Controls;
using ToDoList.ViewModels.Helpers;

namespace ToDoList.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private HamburgerMenuItem _selectedMenuItem = null!;
    [ObservableProperty] private ObservableCollection<HamburgerMenuItem> _bodyMenuItems = null!;
    [ObservableProperty] private ObservableCollection<HamburgerMenuItem> _footerMenuItems = null!;
    [ObservableProperty] private IService _currentViewModel = null!;

    private readonly Dictionary<Type, IService> _viewModels = new();

    public override void Initialize(ImmutableDictionary<Type, IService> services)
    {
        LoadItems();
        foreach(var item in BodyMenuItems.Concat(FooterMenuItems))
            _viewModels.Add(item.Type, services.Get(item.Type));
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.PropertyName == nameof(SelectedMenuItem)) HandleSelectedMenuItemChanged();
    }

    private void HandleSelectedMenuItemChanged()
    {
        if (!_viewModels.TryGetValue(SelectedMenuItem.Type, out var viewModel))
            throw new Exception("No view model fit the current selected item");
        CurrentViewModel = viewModel;
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