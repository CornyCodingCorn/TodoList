using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ToDoList.Models;

public enum TaskStatus
{
    NotStarted,
    Started,
    Done
}

public class Task
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string Description { get; set; } = string.Empty;
    public TaskStatus Status { get; set; }
}