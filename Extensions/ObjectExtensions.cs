using System;

namespace ToDoList.Extensions;

public static class ObjectExtensions
{
    public static bool TryGetPropertyValue<T>(this object obj, string propertyName, out T? value)
    {
        var result = obj.GetType().GetProperty(propertyName)?.GetValue(obj, null);
        value = (T?)result;
        return value is not null;
    }

    public static T GetPropertyValue<T>(this object obj, string propertyName)
    {
        var property = obj.GetType().GetProperty(propertyName);
        if (property is null) throw new Exception($"Property with name {propertyName} not found");

        // If type is nullable then return with type cast to T
        if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(Nullable<>))
            return (T)property.GetValue(obj, null)!;
        // If type is not nullable then cast to nullable and check if it's null or not
        // if null then throw exception to make sure that it's typesafe
        var result = (T?)property.GetValue(obj, null);
        if (result == null)
            throw new Exception($"Property with name {propertyName} is not a nullable property but the value is null");
        return result;
    }
}