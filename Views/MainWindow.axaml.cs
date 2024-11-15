using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;

namespace ToDoList.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

#if DEBUG
        this.AttachDevTools(new KeyGesture(Key.F9));
#endif
    }
}