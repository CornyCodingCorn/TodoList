using System;

namespace ToDoList.ViewModels.Helpers;

public interface IService
{
    public void Initialize(ImmutableDictionary<Type, IService> services);
}