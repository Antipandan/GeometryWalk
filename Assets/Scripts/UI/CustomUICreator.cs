using UnityEngine;

public class CustomUICreator : ICustomUICreator
{
    // ReSharper disable once MemberCanBeProtected.Global
    public CustomUICreator()
    {
        
    }
    public virtual GameObject CreateInstance()
    {
        return null;
    }
}