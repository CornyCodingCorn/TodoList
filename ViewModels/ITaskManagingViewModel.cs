using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.ViewModels;

public interface ITaskManagingViewModel
{
    public void AddTask(TaskModel task);
    public void LoadTasks();

    protected void DeleteTask(TaskItemViewModel task);
    protected Task DeleteTaskAsync(TaskItemViewModel task);
}