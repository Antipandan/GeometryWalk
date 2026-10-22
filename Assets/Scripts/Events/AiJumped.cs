public struct AiJumped
{
    private ushort nrJumpActions;

    public AiJumped(ushort nrJumpActions)
    {
        this.nrJumpActions = nrJumpActions;
    }

    public ushort NrJumpActions
    {
        get => nrJumpActions;
    }
    
}