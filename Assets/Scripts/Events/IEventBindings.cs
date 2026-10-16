using System;

public interface IEventBindings<T>
{
    public Action<T> OnEvent { get; set; }
    public Action OnEventNoArgs { get; set; }

    public Type BindingType
    {
        get => typeof(T);
    }
}