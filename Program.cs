using Avalonia;
using System;
using ToDoList.ViewModels;
using ToDoList.ViewModels.Helpers;

namespace ToDoList;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        DependencyInjector
            .Register<MainWindowViewModel>()
            .Register<MainViewModel>()
            .Register<ArchiveViewModel>()
            .Register<DashboardViewModel>()
            .Register<InformationViewModel>()
            .Register<PlanningViewModel>()
            .Register<PopupPresenterViewModel>()
            .Register<SettingsViewModel>()
            .Register<TasksTabViewModel>();
        
        BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
    }

    public static IDependencyInjector DependencyInjector { get; set; } = new DependencyInjector();
    
    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}