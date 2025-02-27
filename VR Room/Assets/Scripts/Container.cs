using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public static class Container
{
    private static readonly Dictionary<Type, object> DependenciesDictionary = new();

    public static void Register<T>(T instance) where T : class
    {
        Type type = typeof(T);
        if (DependenciesDictionary.ContainsKey(type))
        {
            Debug.LogWarning($"Type {type} is already registered. Overwriting the instance.");
        }

        DependenciesDictionary[type] = instance;
        Debug.Log($"Registered instance of type {type}");
    }

    public static void Unregister<T>() where T : class
    {
        Type type = typeof(T);
        if (DependenciesDictionary.TryGetValue(type, out var instance))
        {
            Debug.Log($"Unregistered instance of type {type}");
            DependenciesDictionary.Remove(type);
        }
    }

    public static T GetInstance<T>() where T : class
    {
        Type type = typeof(T);
        if (DependenciesDictionary.TryGetValue(type, out var instance))
        {
            Debug.Log($"Retrieved instance of type {type}");
            return instance as T;
        }

        Debug.LogWarning($"Instance of type {type} not found in container.");
        return null;
    }

    public static bool TryGetInstance<T>(out T instance) where T : class
    {
        Type type = typeof(T);
        if (DependenciesDictionary.TryGetValue(type, out var instance1))
        {
            instance = instance1 as T;
            Debug.Log($"Retrieved instance of type {type}");
            return true;
        }
        instance = null;
        return false;
    }
}
