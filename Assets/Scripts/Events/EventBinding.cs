using System;

public class EventBinding<T> : IEventBindings<T>
{
    private event Action<T> onEvent;
    private event Action onEventNoArgs;
    
    event Action<T> IEventBindings<T>.OnEvent { add => onEvent += value; remove => onEvent -= value; }
    event Action IEventBindings<T>.OnEventNoArgs { add => onEventNoArgs += value; remove => onEventNoArgs -= value; }
    
    public EventBinding(Action<T> @event)
    {
        this.onEvent = @event;
    }

    public EventBinding(Action @event)
    {
        this.onEventNoArgs = @event;
    }

    public EventBinding(Action eventNoArgs, Action<T> @event)
    {
        this.onEvent = @event;
        this.onEventNoArgs = eventNoArgs;
    }

    public void RemoveDelegate(Action<T> @delegate)
    {
        onEvent -= @delegate;
    }

    public void AddDelegate(Action<T> @delegate, bool ensureSingletonDelegate = true)
    {
        if (ensureSingletonDelegate) RemoveDelegate(@delegate);
        onEvent += @delegate;
    }

    public void RemoveDelegate(Action @delegate)
    {
        onEventNoArgs -= @delegate;
    }

    public void AddDelegateNoArgs(Action @delegate, bool ensureSingletonDelegate = true)
    {
        if (ensureSingletonDelegate) RemoveDelegate(@delegate);
        onEventNoArgs += @delegate;
    }

    public void PublishOnEventNoArgs()
    {
        onEventNoArgs?.Invoke();
    }

    public void PublishOnEvent(T arg)
    {
        onEvent?.Invoke(arg);
    }

}