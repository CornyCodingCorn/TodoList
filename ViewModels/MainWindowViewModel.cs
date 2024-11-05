using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ToDoList.Models;

namespace ToDoList.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<TaskModel> _tasks = [];
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
        Tasks.Add(new TaskModel
        {
            Name = "New task",
            Description = NewTaskDescription,
        });
    }

    [RelayCommand]
    private void DeleteTask(TaskModel taskModel)
    {
        Tasks.Remove(taskModel);
    }

    private void LoadTasks()
    {
        // TODO: Add method to load from file for database
        Tasks.Add(new TaskModel { Id = "0", Description = "This is some random description for this one", Status = TaskModelStatus.Done, Name = "Task done" });
        Tasks.Add(new TaskModel { Id = "1", Description = "This is some jsdaofjasodfjosdjfosdjfosdjafodsjfosdjfosdjfosdjfodsjfosdjfodsjfodsjfodsjfosdajfosdajfosdjf\njdsfoijsdaofjsdofjodsjfdsofjodsjf\njdsojfosdajfosdjfodsjfojsdf\nfjdsofjsdofjosdjfosdjfosdj", Status = TaskModelStatus.NotStarted, Name = "Not done task"});
        Tasks.Add(new TaskModel { Id = "2", Description = "This is some jsdaofjasodfjosdjfosdjfosdjafodsjfosdjfosdjfosdjfodsjfosdjfodsjfodsjfodsjfosdajfosdajfosdjf\njdsfoijsdaofjsdofjodsjfdsofjodsjf\njdsojfosdajfosdjfodsjfojsdf\nfjdsofjsdofjosdjfosdjfosdj", Status = TaskModelStatus.Started, Name = "Started task 1"});
        Tasks.Add(new TaskModel { Id = "3", Description = "This is some jsdaofjasodfjosdjfosdjfosdjafodsjfosdjfosdjfosdjfodsjfosdjfodsjfodsjfodsjfosdajfosdajfosdjf\njdsfoijsdaofjsdofjodsjfdsofjodsjf\njdsojfosdajfosdjfodsjfojsdf\nfjdsofjsdofjosdjfosdjfosdj", Status = TaskModelStatus.Started, Name = "Started task 2"});
        Tasks.Add(new TaskModel { Id = "4", Description = "This is some jsdaofjasodfjosdjfosdjfosdjafodsjfosdjfosdjfosdjfodsjfosdjfodsjfodsjfodsjfosdajfosdajfosdjf\njdsfoijsdaofjsdofjodsjfdsofjodsjf\njdsojfosdajfosdjfodsjfojsdf\nfjdsofjsdofjosdjfosdjfosdj", Status = TaskModelStatus.NotStarted, Name = "Not done task"});
        Tasks.Add(new TaskModel { Id = "5", Description = "This is some jsdaofjasodfjosdjfosdjfosdjafodsjfosdjfosdjfosdjfodsjfosdjfodsjfodsjfodsjfosdajfosdajfosdjf\njdsfoijsdaofjsdofjodsjfdsofjodsjf\njdsojfosdajfosdjfodsjfojsdf\nfjdsofjsdofjosdjfosdjfosdj", Status = TaskModelStatus.NotStarted, Name = "Not done task"});
    }
}
