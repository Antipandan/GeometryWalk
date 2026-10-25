using System;
using CustomUtility;

public interface IEventBindings<T>
{
    public event Action<T> OnEvent {
        add
        {
            ErrorHandling.PreventEnteringPlayMode($"{nameof(OnEventNoArgs)} property" +
                                                  $" has not been implemented. It needs to be implemented!");
            throw new NotImplementedException();
        }
        remove
        {
            ErrorHandling.PreventEnteringPlayMode($"{nameof(OnEventNoArgs)} property" +
                                                  $" has not been implemented. It needs to be implemented!");
            throw new NotImplementedException();
        } 
    }
    public event Action OnEventNoArgs
    {
        add
        {
            ErrorHandling.PreventEnteringPlayMode($"{nameof(OnEventNoArgs)} property" +
                                                  $" has not been implemented. It needs to be implemented!");
            throw new NotImplementedException();
        }
        remove
        {
            ErrorHandling.PreventEnteringPlayMode($"{nameof(OnEventNoArgs)} property" +
                                                  $" has not been implemented. It needs to be implemented!");
            throw new NotImplementedException();
        }
    }

    public Type BindingType
    {
        get => typeof(T);
    }

    public void PublishOnEvent(T arg);
    
    public void PublishOnEventNoArgs();
}