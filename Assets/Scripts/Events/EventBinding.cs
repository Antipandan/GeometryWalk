using System;

public class EventBinding<T> : IEventBindings<T> where T : IEvent
{
    private Action<T> onEvent;
    private Action onEventNoArgs;
    
    public Action<T> OnEvent { get; set; }
    public Action OnEventNoArgs { get; set; }
    
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

}