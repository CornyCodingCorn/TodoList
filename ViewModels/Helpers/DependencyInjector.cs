using System;
using System.Collections.Generic;
using System.Linq;

namespace ToDoList.ViewModels.Helpers;

public class DependencyInjector : IDependencyInjector
{
    public ImmutableDictionary<Type, IService> Services { get; }
    private readonly Dictionary<Type, IService> _services = new();

    public DependencyInjector()
    {
        Services = new ImmutableDictionary<Type, IService>(_services);
    }
    
    public IDependencyInjector Register<TType>() where TType : IService
    {
        var newObj = (IService?)Activator.CreateInstance(typeof(TType)) ?? throw new Exception($"Failed to instantiate type {typeof(TType).Name}");
        _services.Add(typeof(TType), newObj);
        return this;
    }

    public IDependencyInjector Register<TInterface, TType>() where TType : IService
    {
        var newObj = (IService?)Activator.CreateInstance(typeof(TType)) ?? throw new Exception($"Failed to instantiate type {typeof(TType).Name}");
        _services.Add(typeof(TInterface), newObj);
        return this;
    }

    public void Build()
    {
        foreach (var instances in Services.Values)
        {
            instances.Initialize(Services);
        }
    }
}