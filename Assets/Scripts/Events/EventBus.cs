using System.Collections.Generic;

public static class EventBus<T>
{
    private static readonly HashSet<IEventBindings<T>> bindings = new HashSet<IEventBindings<T>>();

    public static void Register(IEventBindings<T> binding)
    {
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
            binding.OnEvent.Invoke(parameter);
            if (raiseAll) binding.OnEventNoArgs.Invoke();
        }
    }

    public static void Raise()
    {
        foreach (IEventBindings<T> binding in bindings)
        {
            binding.OnEventNoArgs.Invoke();
        }
    }
}