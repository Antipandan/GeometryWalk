using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class EventBus<T>
{
    private static readonly HashSet<IEventBindings<T>> bindings = new HashSet<IEventBindings<T>>();
    public static void Register(IEventBindings<T> binding)
    {
        HandleClearing();
        bindings.Add(binding);
    }

    public static void DeRegister(IEventBindings<T> binding)
    {
        bindings.Remove(binding);
    }

    public static void Raise(T parameter, bool raiseAll = true)
    {
        // simpler
        foreach (IEventBindings<T> binding in bindings)
        {
            if (binding.BindingType != typeof(T)) continue;
            binding.PublishOnEvent(parameter);
            if (raiseAll) binding.PublishOnEventNoArgs();
        }
    }
    

    public static void Raise()
    {
        foreach (IEventBindings<T> binding in bindings)
        {
            if (binding.BindingType != typeof(T)) continue;
            binding.PublishOnEventNoArgs();
        }
    }

    // need scene to satisfy delegate requirements. Scene doesn't do anything
    private static void ClearAllBindings(Scene scene)
    {
        bindings.Clear();
    }

    private static void HandleClearing()
    {
        SceneManager.sceneUnloaded -= ClearAllBindings;
        SceneManager.sceneUnloaded += ClearAllBindings;
    }
}