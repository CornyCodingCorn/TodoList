using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ToDoList.Controls;
using ToDoList.Models;

namespace ToDoList.ViewModels;

public partial class TasksTabViewModel : ViewModelBase, ITaskManagingViewModel
{
    public static TasksTabViewModel? Instance { get; private set; } 
    private const string DefaultTaskDescription = "Edit this task to add description";

    [ObservableProperty] private ObservableCollection<TaskItemViewModel> _tasks = [];

    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(AddTaskCommand))]
    private string _newTaskName = string.Empty;

    private const int TimeBeforeMovingTaskToArchive = 300000;
    private const int TimeForRemoveAnimation = 400;
    private TaskItemViewModel? _taskToDelete;

    private bool CanAddTask => NewTaskName != string.Empty;

    public TasksTabViewModel()
    {
        LoadTasks();
        Instance = this;
    }
    
    public void LoadTasks()
    {
        // TODO: Add method to load from file for database
        CreateTask("0", "Task done", "This is some random description for this one", TaskModelStatus.Done);
        CreateTask("1", "Not Done Task",
            "This is some jsdaofjasodfjosdjfosdjfosdjafodsjfosdjfosdjfosdjfodsjfosdjfodsjfodsjfodsjfosdajfosdajfosdjf\njdsfoijsdaofjsdofjodsjfdsofjodsjf\njdsojfosdajfosdjfodsjfojsdf\nfjdsofjsdofjosdjfosdjfosdj");
        CreateTask("2", "Started Task 1",
            "This is some jsdaofjasodfjosdjfosdjfosdjafodsjfosdjfosdjfosdjfodsjfosdjfodsjfodsjfodsjfosdajfosdajfosdjf\njdsfoijsdaofjsdofjodsjfdsofjodsjf\njdsojfosdajfosdjfodsjfojsdf\nfjdsofjsdofjosdjfosdjfosdj",
            TaskModelStatus.Started);
        CreateTask("3", "Started Task 2",
            "This is some jsdaofjasodfjosdjfosdjfosdjafodsjfosdjfosdjfosdjfodsjfosdjfodsjfodsjfodsjfosdajfosdajfosdjf\njdsfoijsdaofjsdofjodsjfdsofjodsjf\njdsojfosdajfosdjfodsjfojsdf\nfjdsofjsdofjosdjfosdjfosdj",
            TaskModelStatus.Started);
        CreateTask("4", "Not Done Task",
            "This is some jsdaofjasodfjosdjfosdjfosdjafodsjfosdjfosdjfosdjfodsjfosdjfodsjfodsjfodsjfosdajfosdajfosdjf\njdsfoijsdaofjsdofjodsjfdsofjodsjf\njdsojfosdajfosdjfodsjfojsdf\nfjdsofjsdofjosdjfosdjfosdj");
        CreateTask("5", "Not Done Task",
            "This is some jsdaofjasodfjosdjfosdjfosdjafodsjfosdjfosdjfosdjfodsjfosdjfodsjfodsjfodsjfosdajfosdajfosdjf\njdsfoijsdaofjsdofjodsjfdsofjodsjf\njdsojfosdajfosdjfodsjfojsdf\nfjdsofjsdofjosdjfosdjfosdj");
    }

    public void DeleteTask(TaskItemViewModel task)
    {
        Tasks.Remove(task);
    }
    
    [RelayCommand]
    public async Task DeleteTaskAsync(TaskItemViewModel task)
    {
        _taskToDelete = task;
        var popupTitle = "Confirmation";
        var popupMessage = $"Are you sure you want to delete {task.Model.Name} task?";

        var result =
            await MainWindowViewModel.ShowYesNoDialogAsync(popupTitle, popupMessage);
        if (result == 0)
        {
            _taskToDelete = null;
            return;
        }
        
        // Why did I do it like this :| - CodingCorn
        if (_taskToDelete is null)
            return;
        _taskToDelete.IsDeleting = true;
        await Task.Delay(TimeForRemoveAnimation);
        DeleteTask(task);
    }

    public void AddTask(TaskModel task)
    {
        var taskViewModel = new TaskItemViewModel(task, DeleteTaskCommand);
        taskViewModel.Archived += HandleTaskArchivedEvent;
        taskViewModel.TimeBeforeArchiving = TimeBeforeMovingTaskToArchive;

        Tasks.Add(taskViewModel);
    }

    [RelayCommand(CanExecute = nameof(CanAddTask))]
    private void AddTask()
    {
        CreateTask(Guid.NewGuid().ToString(), NewTaskName, DefaultTaskDescription);
        NewTaskName = string.Empty;
    }
    
    private void CreateTask(string id, string name, string description,
        TaskModelStatus status = TaskModelStatus.NotStarted)
    {
        var model = new TaskModel
        {
            Id = id,
            Description = description,
            Status = status,
            Name = name,
        };

        AddTask(model);
    }
    

    private async void HandleTaskArchivedEvent(TaskItemViewModel viewmodel)
    {
        try
        {
            await Task.Delay(TimeForRemoveAnimation);
            // Change later to actually moving it to archive
            ArchiveViewModel.Instance?.AddTask(viewmodel.Model);
            Tasks.Remove(viewmodel);
            Logger.Sink?.Log(LogEventLevel.Information, "Task", this, $"Task with name {viewmodel.Model.Name} has been archived");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Logger.Sink?.Log(LogEventLevel.Error, "Task", this, e.Message);
        }
    }
}