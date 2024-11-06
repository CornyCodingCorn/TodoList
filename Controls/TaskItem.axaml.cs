using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.LogicalTree;
using CommunityToolkit.Mvvm.Input;
using ToDoList.Models;
using Timer = System.Timers.Timer;

namespace ToDoList.Controls;

public partial class TaskItem : TemplatedControl
{
    public static readonly StyledProperty<ICommand> DeleteCommandProperty =
        AvaloniaProperty.Register<TaskItem, ICommand>(nameof(DeleteCommand), defaultValue: new  RelayCommand(() => {}));
    
    public static readonly StyledProperty<TaskModelStatus> ModelStatusProperty =
        AvaloniaProperty.Register<TaskItem, TaskModelStatus>(nameof(ModelStatus), defaultValue: TaskModelStatus.NotStarted);
    
    public static readonly StyledProperty<long> TimeSpentProperty =
        AvaloniaProperty.Register<TaskItem, long>(nameof(TimeSpent), defaultValue: 0);

    public static readonly DirectProperty<TaskItem, string> TimeStringProperty =
        AvaloniaProperty.RegisterDirect<TaskItem, string>(nameof(TimeString), o => o.TimeString);

    private static readonly Timer Clock = new ();

    public string TimeString
    {
        get => _timeString;
        private set => SetAndRaise(TimeStringProperty, ref _timeString, value);
    }

    public long TimeSpent
    {
        get => GetValue(TimeSpentProperty);
        set => SetValue(TimeSpentProperty, value);
    }

    public TaskModelStatus ModelStatus
    {
        get => GetValue(ModelStatusProperty);
        set => SetValue(ModelStatusProperty, value);
    }

    private string _timeString = "00:00:00";
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
    private void UpdateStatus(string isBackward = "False")
    {
        if (isBackward == "True")
        {
            var statusAsInt = (int)ModelStatus - 1;
            if (statusAsInt < 0)
                statusAsInt = Enum.GetValues<TaskModelStatus>().Length - 1;
            
            ModelStatus = (TaskModelStatus)statusAsInt;
            return;
        }
        
        ModelStatus = (TaskModelStatus)((int)(ModelStatus + 1) % Enum.GetValues<TaskModelStatus>().Length);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
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
        if (ModelStatus != TaskModelStatus.Started)
        {
            _lastRecordedTime = DateTime.Now.Ticks;
            return;
        }
        
        var newRecordedTime = DateTime.Now.Ticks;
        TimeSpent += newRecordedTime - _lastRecordedTime;
        
        var time = new DateTime(TimeSpent);
        TimeString = time.ToString(time.Hour > 0 ? "HH:mm:ss" : "mm:ss");

        _lastRecordedTime = newRecordedTime;
    }
}