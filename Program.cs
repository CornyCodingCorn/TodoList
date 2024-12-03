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
        BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
    }

    public static IDependencyInjector DependencyInjector { get; } = new DependencyInjector();
    
    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}