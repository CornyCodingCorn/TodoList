using System;
using System.Threading;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls.Primitives;
using CommunityToolkit.Mvvm.Input;
using ToDoList.Models;
using Timer = System.Timers.Timer;

namespace ToDoList.Controls;

public partial class TaskItem : TemplatedControl
{
    public static readonly StyledProperty<ICommand> DeleteCommandProperty =
        AvaloniaProperty.Register<TaskItem, ICommand>(nameof(DeleteCommand), defaultValue: new  RelayCommand(() => {}));

    public static readonly DirectProperty<TaskItem, string> TimeStringProperty =
        AvaloniaProperty.RegisterDirect<TaskItem, string>(nameof(TimeString), o => o.TimeString);

    private static readonly Timer Clock = new ();

    public string TimeString
    {
        get => _timeString;
        private set => SetAndRaise(TimeStringProperty, ref _timeString, value);
    }

    private string _timeString = "00:00:00";
    private TaskModel _taskModel = null!;
    private long _lastRecordedTime;
    private readonly PeriodicTimer _periodicTimer = new (TimeSpan.FromMilliseconds(500));

    public TaskItem()
    {
        Clock.Enabled = true;
        Clock.Interval = 500;
        Clock.Start();
    }

    public ICommand DeleteCommand
    {
        get => GetValue(DeleteCommandProperty);
        set => SetValue(DeleteCommandProperty, value);
    }

    [RelayCommand]
    private void UpdateStatus()
    {
        _taskModel.Status = (TaskModelStatus)((int)(_taskModel.Status + 1) % Enum.GetValues<TaskModelStatus>().Length);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _taskModel = DataContext as TaskModel ?? throw new Exception("DataContext of TaskItem is not TaskModel"); 
        SetupTimer();
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
        if (_taskModel.Status != TaskModelStatus.Started)
        {
            _lastRecordedTime = DateTime.Now.Ticks;
            return;
        }
        
        var newRecordedTime = DateTime.Now.Ticks;
        _taskModel.TimeSpent += newRecordedTime - _lastRecordedTime;
        
        var time = new DateTime(_taskModel.TimeSpent);
        // var oldStr = TimeString;
        TimeString = time.ToString("HH:mm:ss");
        Console.WriteLine(TimeString);
        // RaisePropertyChanged(TimeStringProperty, oldStr, TimeString);
        
        _lastRecordedTime = newRecordedTime;
    }
}