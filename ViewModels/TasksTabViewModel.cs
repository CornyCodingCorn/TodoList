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
    private const string DefaultTaskDescription = "Edit this task to add description";

    [ObservableProperty] private ObservableCollection<TaskItemViewModel> _tasks = [];
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
        AddTask(Guid.NewGuid().ToString(), NewTaskName, DefaultTaskDescription);
        NewTaskName = string.Empty;
    }

    [RelayCommand]
    private async Task ShowDeleteTaskConfirmation(TaskItemViewModel taskViewModel)
    {
        _taskToDelete = taskViewModel;
        var popupTitle = "Confirmation";
        var popupMessage = $"Are you sure you want to delete {taskViewModel.Model.Name} task?";

        var result =
            await MainWindowViewModel.ShowYesNoDialogAsync(popupTitle, popupMessage);
        switch (result)
        {
            case 0:
                _taskToDelete = null;
                break;
            case 1:
                if (_taskToDelete is not null)
                    Tasks.Remove(_taskToDelete);
                break;
        }
    }
    private void LoadTasks()
    {
        // TODO: Add method to load from file for database
        AddTask("0", "Task done", "This is some random description for this one", TaskModelStatus.Done);
        AddTask("1", "Not Done Task",
            "This is some jsdaofjasodfjosdjfosdjfosdjafodsjfosdjfosdjfosdjfodsjfosdjfodsjfodsjfodsjfosdajfosdajfosdjf\njdsfoijsdaofjsdofjodsjfdsofjodsjf\njdsojfosdajfosdjfodsjfojsdf\nfjdsofjsdofjosdjfosdjfosdj");
        AddTask("2", "Started Task 1",
            "This is some jsdaofjasodfjosdjfosdjfosdjafodsjfosdjfosdjfosdjfodsjfosdjfodsjfodsjfodsjfosdajfosdajfosdjf\njdsfoijsdaofjsdofjodsjfdsofjodsjf\njdsojfosdajfosdjfodsjfojsdf\nfjdsofjsdofjosdjfosdjfosdj",
            TaskModelStatus.Started);
        AddTask("3", "Started Task 2",
            "This is some jsdaofjasodfjosdjfosdjfosdjafodsjfosdjfosdjfosdjfodsjfosdjfodsjfodsjfodsjfosdajfosdajfosdjf\njdsfoijsdaofjsdofjodsjfdsofjodsjf\njdsojfosdajfosdjfodsjfojsdf\nfjdsofjsdofjosdjfosdjfosdj",
            TaskModelStatus.Started);
        AddTask("4", "Not Done Task",
            "This is some jsdaofjasodfjosdjfosdjfosdjafodsjfosdjfosdjfosdjfodsjfosdjfodsjfodsjfodsjfosdajfosdajfosdjf\njdsfoijsdaofjsdofjodsjfdsofjodsjf\njdsojfosdajfosdjfodsjfojsdf\nfjdsofjsdofjosdjfosdjfosdj");
        AddTask("5", "Not Done Task",
            "This is some jsdaofjasodfjosdjfosdjfosdjafodsjfosdjfosdjfosdjfodsjfosdjfodsjfodsjfodsjfosdajfosdajfosdjf\njdsfoijsdaofjsdofjodsjfdsofjodsjf\njdsojfosdajfosdjfodsjfojsdf\nfjdsofjsdofjosdjfosdjfosdj");
    }

    private void AddTask(string id, string name, string description,
        TaskModelStatus status = TaskModelStatus.NotStarted)
    {
        var taskViewModel = new TaskItemViewModel(new TaskModel
        {
            Id = id,
            Description = description,
            Status = status,
            Name = name
        }, ShowDeleteTaskConfirmationCommand);

        Tasks.Add(taskViewModel);
    }
}