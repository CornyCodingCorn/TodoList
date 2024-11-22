using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExCSS;
using ToDoList.Models;

namespace ToDoList.ViewModels;

public partial class TaskItemViewModel : ViewModelBase, IDisposable
{
    [ObservableProperty] private TaskModel _model;
    [ObservableProperty] private string _timeString = "00:00:00";
    [ObservableProperty] private bool _isEditing;
    [ObservableProperty] private bool _isExpanded;
    [ObservableProperty] private bool _isDeleting;
    [ObservableProperty] private string _editingDescription = string.Empty;
    [ObservableProperty] private ICommand _deleteCommand;

    public event Action<TaskItemViewModel>? Archived;
    public int TimeBeforeArchiving { get; set; } = 5000;

    private long _lastRecordedTime;
    private readonly PeriodicTimer _periodicTimer = new(TimeSpan.FromMilliseconds(500));

    public TaskItemViewModel() : this(new TaskModel
    {
        Id = "0",
        Name = "Design task",
        Description = "Description for design task to use",
        CompleteDate = DateTime.Now,
        IsArchived = true
    }, new RelayCommand(() => Console.WriteLine("Executing DeleteCommand")))
    {
        if (!Design.IsDesignMode) throw new InvalidOperationException("TaskItemViewModel default constructor is only for design mode");
    }
    
    public TaskItemViewModel(TaskModel model, ICommand deleteCommand)
    {
        Model = model;
        DeleteCommand = deleteCommand;
        SetupTimer();
    }

    public void Dispose()
    {
        _periodicTimer.Dispose();
        GC.SuppressFinalize(this);
    }

    [RelayCommand]
    private void StartEditing()
    {
        EditingDescription = Model.Description;
        IsEditing = true;
    }

    [RelayCommand]
    private async Task StopEditing(bool discard = false)
    {
        if (discard && Model.Description != EditingDescription && !await GetUserDiscardConfirmation()) return;

        IsEditing = false;
        if (discard) return;

        Model.Description = EditingDescription;
    }

    [RelayCommand]
    private void UpdateStatus(string isBackward = "False")
    {
        if (isBackward == "True")
        {
            var statusAsInt = (int)Model.Status - 1;
            if (statusAsInt < 0)
                statusAsInt = Enum.GetValues<TaskModelStatus>().Length - 1;

            Model.Status = (TaskModelStatus)statusAsInt;
        }
        else
            Model.Status = (TaskModelStatus)((int)(Model.Status + 1) % Enum.GetValues<TaskModelStatus>().Length);
        
        CheckCompletion();
    }

    private async void CheckCompletion()
    {
        try
        {
            if (Model.Status != TaskModelStatus.Done) return;
        
            Model.CompleteDate = DateTime.Now;
            await Task.Delay(TimeBeforeArchiving);
            if (Model.Status != TaskModelStatus.Done) return;
            Model.IsArchived = true;
            
            Archived?.Invoke(this);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private async void SetupTimer()
    {
        try
        {
            _lastRecordedTime = DateTime.Now.Ticks;
            while (true)
            {
                await _periodicTimer.WaitForNextTickAsync();
                UpdateTime();
            }
        }
        catch (Exception)
        {
            // Ignore
        }
    }

    private void UpdateTime()
    {
        if (Model.Status != TaskModelStatus.Started)
        {
            _lastRecordedTime = DateTime.Now.Ticks;
            return;
        }

        var newRecordedTime = DateTime.Now.Ticks;
        Model.TimeSpent += newRecordedTime - _lastRecordedTime;

        var time = new DateTime(Model.TimeSpent);
        TimeString = time.ToString("HH:mm:ss");

        _lastRecordedTime = newRecordedTime;
    }

    // Return true if still discard
    private async Task<bool> GetUserDiscardConfirmation()
    {
        var result =
            await MainWindowViewModel.ShowYesNoDialogAsync("Discard Edit", "Are you sure you want discard this edit?");
        return result != 0;
    }
}