using System;

namespace ToDoList.ViewModels.Helpers;

public interface IDependencyInjector
{
    public ImmutableDictionary<Type, IService> Services { get; }
    public IDependencyInjector Register<TType>() where TType : IService;
    public IDependencyInjector Register<TInterface, TType>() where TType : IService;
    public void Build();
}