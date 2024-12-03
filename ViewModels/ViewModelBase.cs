using System;
using CommunityToolkit.Mvvm.ComponentModel;
using ToDoList.ViewModels.Helpers;

namespace ToDoList.ViewModels;

public class ViewModelBase : ObservableObject, IService
{
    public virtual void Initialize(ImmutableDictionary<Type, IService> services)
    {
    }
}