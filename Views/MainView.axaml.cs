using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ToDoList.ViewModels;

namespace ToDoList.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        DataContext = Program.DependencyInjector.Services.Get(typeof(MainViewModel));
    }
}