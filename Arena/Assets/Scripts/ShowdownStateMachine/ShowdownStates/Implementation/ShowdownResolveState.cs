public class ShowdownResolveState : ShowdownState
{    
    #region Constructor

    /// <summary>
    /// Default Constructor - sets this states BattleStateType.
    /// </summary>
    public ShowdownResolveState()
    {
        this.StateType = ShowdownState.ShowdownStateType.ShowdownResolveState;
        OnEnter();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Occurs when this state is entered from another state.
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
    }
    
    /// <summary>
    /// Occurs when this state is exited to another state.
    /// </summary>
    public override void OnExit()
    {
        base.OnExit();
    }

    /// <summary>
    /// This is called every frame while this state is the current state.
    /// </summary>
    public override void Update()
    {
        base.Update();
    }

    #endregion
}
