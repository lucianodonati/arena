using UnityEngine;

public class ShowdownSuspenseState : ShowdownState
{    
    #region Constructor

    /// <summary>
    /// Default Constructor - sets this states BattleStateType.
    /// </summary>
    public ShowdownSuspenseState(bool transitionOnly=false)
    {
        this.StateType = ShowdownState.ShowdownStateType.ShowdownSuspenseState;
        if (transitionOnly == false)
        {            
            OnEnter();
        }
    }

    #endregion

    #region Private Member Variables

    /// <summary>
    /// 
    /// </summary>
    private float suspenseTime = 0.0f;

    /// <summary>
    /// 
    /// </summary>
    private float suspenseTimer = 0.0f;

    #endregion

    #region Public Methods

    /// <summary>
    /// Occurs when this state is entered from another state.
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();        
        suspenseTime = Random.Range(4.0f, 5.0f);
        Showdown.GetInstance().IsAcceptingAttacks = true;
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

        RunSuspenseTimer();
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// 
    /// </summary>
    private void RunSuspenseTimer()
    {
        suspenseTimer += Time.deltaTime;

        if (suspenseTimer >= suspenseTime)
        {
            Showdown.GetInstance().StateMachine.TransitionToState(ShowdownState.ShowdownStateType.ShowdownFightState);
        }
    }

    #endregion
}
