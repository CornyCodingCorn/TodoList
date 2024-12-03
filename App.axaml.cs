using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using ToDoList.ViewModels;
using ToDoList.ViewModels.Helpers;
using ToDoList.Views;

namespace ToDoList;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            BindingPlugins.DataValidators.RemoveAt(0);
            
            Program.DependencyInjector
                .Register<MainWindowViewModel>()
                .Register<MainViewModel>()
                .Register<ArchiveViewModel>()
                .Register<DashboardViewModel>()
                .Register<InformationViewModel>()
                .Register<PlanningViewModel>()
                .Register<PopupPresenterViewModel>()
                .Register<SettingsViewModel>()
                .Register<TasksTabViewModel>()
                .Build();
            
            desktop.MainWindow = new MainWindow
            {
                DataContext = Program.DependencyInjector.Services.Get(typeof(MainWindowViewModel))
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}