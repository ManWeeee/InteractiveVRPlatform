using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks;
using UnityEngine;

public static class Container
{
    private static readonly Dictionary<Type, object> DependenciesDictionary = new();

    public static void Register<T>(T instance) where T : class
    {
        if (DependenciesDictionary.ContainsKey(typeof(T)))
        {
            Debug.LogWarning($"Type {typeof(T)} is already registered. Overwriting the instance.");
        }

        DependenciesDictionary[typeof(T)] = instance;
        Debug.Log($"Registered instance of type {typeof(T)}");
    }

    public static T GetInstance<T>() where T : class
    {
        if (DependenciesDictionary.TryGetValue(typeof(T), out var instance))
        {
            Debug.Log($"Retrieved instance of type {typeof(T)}");
            return instance as T;
        }

        Debug.LogError($"Instance of type {typeof(T)} not found in container.");
        return null;
    }
}
