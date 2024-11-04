using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.DAL;

public interface IRepository
{
    public IEnumerable<Task> GetTasks();
    public bool SaveTask(Task task);
    public bool SaveTasks(IEnumerable<Task> tasks);
    public bool DeleteTask(Task task);
    public bool DeleteTask(int id);

    public bool Save();
}