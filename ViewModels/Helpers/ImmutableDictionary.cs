using System.Collections.Generic;

namespace ToDoList.ViewModels.Helpers;

public class ImmutableDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
    where TKey : notnull
{
    public Dictionary<TKey, TValue>.ValueCollection Values => dictionary.Values;
    public Dictionary<TKey, TValue>.KeyCollection Keys => dictionary.Keys;

    public TValue Get(TKey key)
        => dictionary[key];
    
    public TReturn Get<TReturn>(TKey key) where TReturn : TValue
        => (TReturn?)dictionary[key] ?? throw new KeyNotFoundException();

    public bool TryGetValue(TKey key, out TValue? value)
        => dictionary.TryGetValue(key, out value);
}