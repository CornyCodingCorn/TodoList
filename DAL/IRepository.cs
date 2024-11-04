using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.DAL;

public interface IRepository
{
    public IEnumerable<TaskModel> GetTasks();
    public bool SaveTask(TaskModel taskModel);
    public bool SaveTasks(IEnumerable<TaskModel> tasks);
    public bool DeleteTask(TaskModel taskModel);
    public bool DeleteTask(int id);

    public bool Save();
}