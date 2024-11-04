using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ToDoList.Models;

namespace ToDoList.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<Task> _tasks = [];
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(AddTaskCommand))]
    private string _newTaskDescription = string.Empty;
    
    private bool CanAddTask => NewTaskDescription != string.Empty;

    public MainWindowViewModel()
    {
        LoadTasks();
    }

    [RelayCommand(CanExecute = nameof(CanAddTask))]
    private void AddTask()
    {
        Tasks.Add(new Task
        {
            Name = "New task",
            Description = NewTaskDescription,
        });
    }

    [RelayCommand]
    private void DeleteTask(Task task)
    {
        Tasks.Remove(task);
    }

    private void LoadTasks()
    {
        // TODO: Add method to load from file for database
        Tasks.Add(new Task { Id = "0", Description = "Task 1", Status = TaskStatus.Done, Name = "Task done" });
        Tasks.Add(new Task { Id = "1", Description = "Task 2", Status = TaskStatus.NotStarted, Name = "Not done task"});
    }
}
