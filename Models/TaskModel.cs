using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ToDoList.Models;

public enum TaskModelStatus
{
    NotStarted,
    Started,
    Done
}

public partial class TaskModel : ObservableObject
{
    [ObservableProperty] private string _id = string.Empty;
    [ObservableProperty] private string _name = string.Empty;
    [ObservableProperty] private string _description = string.Empty;
    [ObservableProperty] private DateTime? _completeDate = null;
    [ObservableProperty] private bool _isArchived;
    [ObservableProperty] private TaskModelStatus _status = TaskModelStatus.NotStarted;
    [ObservableProperty] private long _timeSpent = 0;
}