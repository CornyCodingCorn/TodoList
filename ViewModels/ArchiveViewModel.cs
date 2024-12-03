using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ToDoList.Models;
using ToDoList.ViewModels.Helpers;
using ToDoList.Views;

namespace ToDoList.ViewModels;

public partial class ArchiveViewModel : ViewModelBase, ITaskManagingViewModel
{
    private const int DelayForAnimation = 400;

    [ObservableProperty] private DateTimeOffset _selectedDate = DateTimeOffset.Now;

    // This is for showing task of selected date
    [ObservableProperty] private ObservableCollection<TaskItemViewModel> _archivedTasks = [];

    // This is for loaded task so the app don't have to load it again
    private readonly List<TaskItemViewModel> _loadedTasks = [];
    private MainWindowViewModel _mainWindowViewModel = null!;
    private TasksTabViewModel _tasksTabViewModel = null!;

    public override void Initialize(ImmutableDictionary<Type, IService> services)
    {
        _mainWindowViewModel = services.Get<MainWindowViewModel>(typeof(MainWindowViewModel));
        _tasksTabViewModel = services.Get<TasksTabViewModel>(typeof(TasksTabViewModel));
        LoadTasks();
    }

    public void AddTask(TaskModel task)
    {
        var taskViewModel = new TaskItemViewModel(task, DeleteTaskCommand, _mainWindowViewModel);
        taskViewModel.Unarchived += HandleUnarchivingTask;
        _loadedTasks.Add(taskViewModel);
        UpdateArchivedTasks();
    }

    public void LoadTasks()
    {
        AddTask(new TaskModel
        {
            Name = "Archived task",
            Description = "Archive task in archive tab",
            Status = TaskModelStatus.Done,
            IsArchived = true,
            CompleteDate = new DateTime(2024, 11, 22, 12, 30, 0, DateTimeKind.Local),
        });
        AddTask(new TaskModel
        {
            Name = "Archived task",
            Description = "Archive task in archive tab",
            Status = TaskModelStatus.Done,
            IsArchived = true,
            CompleteDate = new DateTime(2024, 11, 22, 9, 30, 0, DateTimeKind.Local)
        });
        AddTask(new TaskModel
        {
            Name = "Archived task",
            Description = "Archive task in archive tab",
            Status = TaskModelStatus.Done,
            IsArchived = true,
            CompleteDate = new DateTime(2024, 11, 21, 14, 30, 0, DateTimeKind.Local),
        });
        AddTask(new TaskModel
        {
            Name = "Archived task",
            Description = "Archive task in archive tab",
            Status = TaskModelStatus.Done,
            IsArchived = true,
            CompleteDate = new DateTime(2024, 11, 20, 12, 30, 0, DateTimeKind.Local),
        });
        AddTask(new TaskModel
        {
            Name = "Archived task",
            Description = "Archive task in archive tab",
            Status = TaskModelStatus.Done,
            IsArchived = true,
            CompleteDate = new DateTime(2024, 09, 01, 12, 30, 0, DateTimeKind.Local),
        });
    }

    public void DeleteTask(TaskItemViewModel task)
    {
        ArchivedTasks.Remove(task);
        _loadedTasks.Remove(task);
    }

    [RelayCommand]
    public async Task DeleteTaskAsync(TaskItemViewModel task)
    {
        var result = await _mainWindowViewModel.ShowYesNoDialogAsync("Confirmation",
            $"Are you sure you want to remove archived task named {task.Model.Name}");
        if (result == 0)
            return;

        DeleteTask(task);
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.PropertyName != nameof(SelectedDate)) return;

        UpdateArchivedTasks();
    }

    private void UpdateArchivedTasks()
    {
        ArchivedTasks.Clear();
        var taskInSelectedDate = _loadedTasks.Where(task => SelectedDate.Date.Equals(task.Model.CompleteDate?.Date));
        foreach (var task in taskInSelectedDate)
        {
            ArchivedTasks.Add(task);
        }
    }

    private async void HandleUnarchivingTask(TaskItemViewModel task)
    {
        try
        {
            // Move it to task tab
            await Task.Delay(DelayForAnimation);
            _tasksTabViewModel.AddTask(task.Model);
            ArchivedTasks.Remove(task);
            _loadedTasks.Remove(task);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Logger.Sink?.Log(LogEventLevel.Error, "Task", this, e.Message);
        }
    }
}