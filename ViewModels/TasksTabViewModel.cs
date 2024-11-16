using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ToDoList.Controls;
using ToDoList.Models;

namespace ToDoList.ViewModels;

public partial class TasksTabViewModel : ViewModelBase
{
    [ObservableProperty] private ObservableCollection<TaskItemViewModel> _tasks = [];
    [ObservableProperty] private object? _confirmationDialog;
    [ObservableProperty] private string _confirmationDialogTitle = string.Empty;
    [ObservableProperty] private string? _confirmationDialogMessage = string.Empty;
    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(AddTaskCommand))]
    
    private string _newTaskName = string.Empty;
    private TaskItemViewModel? _taskToDelete;

    private bool CanAddTask => NewTaskName != string.Empty;

    public TasksTabViewModel()
    {
        LoadTasks();
    }

    [RelayCommand(CanExecute = nameof(CanAddTask))]
    private void AddTask()
    {
        Tasks.Add(new TaskItemViewModel(new TaskModel
        {
            Name = NewTaskName,
            Description = "Edit to add description for this task",
        }, ShowDeleteTaskConfirmationCommand));

        NewTaskName = string.Empty;
    }

    [RelayCommand]
    private void ShowDeleteTaskConfirmation(TaskItemViewModel taskViewModel)
    {
        _taskToDelete = taskViewModel;
        ConfirmationDialogTitle = "Confirmation";
        ConfirmationDialogMessage = $"Are you sure you want to delete {taskViewModel.Model.Name} task?";
        if (ConfirmationDialog is null)
        {
            Logger.TryGet(LogEventLevel.Warning, "Deleting task without confirmation because dialog is null");
            DeleteTask();
            return;
        }
        PopupPresenter.ShowPopup(ConfirmationDialog);
    }

    [RelayCommand]
    private void DeleteTask()
    {
        if (_taskToDelete is not null)
            Tasks.Remove(_taskToDelete);
        PopupPresenter.HidePopup();
    }

    [RelayCommand]
    private void CancelDeleteTask()
    {
        _taskToDelete = null;
        PopupPresenter.HidePopup();
    }

    private void LoadTasks()
    {
        // TODO: Add method to load from file for database
        AddNewTask("0", "Task done", "This is some random description for this one", TaskModelStatus.Done);
        AddNewTask("1", "Not Done Task",
            "This is some jsdaofjasodfjosdjfosdjfosdjafodsjfosdjfosdjfosdjfodsjfosdjfodsjfodsjfodsjfosdajfosdajfosdjf\njdsfoijsdaofjsdofjodsjfdsofjodsjf\njdsojfosdajfosdjfodsjfojsdf\nfjdsofjsdofjosdjfosdjfosdj");
        AddNewTask("2", "Started Task 1",
            "This is some jsdaofjasodfjosdjfosdjfosdjafodsjfosdjfosdjfosdjfodsjfosdjfodsjfodsjfodsjfosdajfosdajfosdjf\njdsfoijsdaofjsdofjodsjfdsofjodsjf\njdsojfosdajfosdjfodsjfojsdf\nfjdsofjsdofjosdjfosdjfosdj",
            TaskModelStatus.Started);
        AddNewTask("3", "Started Task 2",
            "This is some jsdaofjasodfjosdjfosdjfosdjafodsjfosdjfosdjfosdjfodsjfosdjfodsjfodsjfodsjfosdajfosdajfosdjf\njdsfoijsdaofjsdofjodsjfdsofjodsjf\njdsojfosdajfosdjfodsjfojsdf\nfjdsofjsdofjosdjfosdjfosdj",
            TaskModelStatus.Started);
        AddNewTask("4", "Not Done Task",
            "This is some jsdaofjasodfjosdjfosdjfosdjafodsjfosdjfosdjfosdjfodsjfosdjfodsjfodsjfodsjfosdajfosdajfosdjf\njdsfoijsdaofjsdofjodsjfdsofjodsjf\njdsojfosdajfosdjfodsjfojsdf\nfjdsofjsdofjosdjfosdjfosdj");
        AddNewTask("5", "Not Done Task",
            "This is some jsdaofjasodfjosdjfosdjfosdjafodsjfosdjfosdjfosdjfodsjfosdjfodsjfodsjfodsjfosdajfosdajfosdjf\njdsfoijsdaofjsdofjodsjfdsofjodsjf\njdsojfosdajfosdjfodsjfojsdf\nfjdsofjsdofjosdjfosdjfosdj");
    }

    private void AddNewTask(string id, string name, string description,
        TaskModelStatus status = TaskModelStatus.NotStarted)
    {
        Tasks.Add(new TaskItemViewModel(new TaskModel
        {
            Id = id,
            Description = description,
            Status = status,
            Name = name
        }, ShowDeleteTaskConfirmationCommand));
    }
}