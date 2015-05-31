public class ShowdownResolveState : ShowdownState
{
    private float resolveTimer = 0.0f;

    #region Constructor

    /// <summary>
    /// Default Constructor - sets this states BattleStateType.
    /// </summary>
    public ShowdownResolveState(bool transitionOnly)
    {
        this.StateType = ShowdownState.ShowdownStateType.ShowdownResolveState;
        if (transitionOnly == false)
        {
            OnEnter();
        }
    }

    #endregion Constructor

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
        RunResolveTimer();
    }

    #endregion Public Methods

    private void RunResolveTimer()
    {
        resolveTimer += UnityEngine.Time.deltaTime;

        if (resolveTimer > 0.25)
        {
            Showdown.GetInstance().IsAcceptingAttacks = false;
            Showdown.GetInstance().DisplayWinner(Showdown.GetInstance().ProcessAttacks());

            Showdown.GetInstance().StateMachine.TransitionToState(ShowdownStateType.ShowdownInitState);
            //Destroy( Showdown.GetInstance() );
        }
    }
}