#region Using Directives

using System.Collections.Generic;

#endregion

public class ShowdownStateMachine
{
    #region Private Member Variables

    /// <summary>
    /// The current state of the state machine.
    /// </summary>
    private IShowdownState currentState;

    /// <summary>
    /// The Dictionary of acceptable state transitions.
    /// </summary>
    private Dictionary<ShowdownStateTransition, IShowdownState> transitions = new Dictionary<ShowdownStateTransition, IShowdownState>();

    #endregion
    
    #region Accessors/Modifiers

    /// <summary>
    /// Accessor for the current state of the state machine.
    /// </summary>
    public IShowdownState CurrentState
    {
        get
        {
            return currentState;
        }
    }

    #endregion
    
    #region Constructor

    /// <summary>
    /// Default Constructor
    /// </summary>
    /// <param name="startingState"> The starting state of the state machine. </param>
    public ShowdownStateMachine(IShowdownState startingState)
    {
        currentState = startingState;
        InitializeTransitions();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// A call to transition to the nextState passed in.
    /// </summary>
    /// <param name="nextState"> the desired state to transition to. </param>
    public void TransitionToState( ShowdownState.ShowdownStateType nextState)
    {       
        IShowdownState battleState = GetNextState(nextState);

        currentState.OnExit();

        currentState = battleState;

        currentState.OnEnter();
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Initializes the state transitions Dictionary.
    /// </summary>
    private void InitializeTransitions()
    {
        transitions.Add(new ShowdownStateTransition(ShowdownState.ShowdownStateType.ShowdownInitState, ShowdownState.ShowdownStateType.ShowdownSuspenseState), new ShowdownSuspenseState());
        transitions.Add(new ShowdownStateTransition(ShowdownState.ShowdownStateType.ShowdownSuspenseState, ShowdownState.ShowdownStateType.ShowdownFightState), new ShowdownFightState());
        transitions.Add(new ShowdownStateTransition(ShowdownState.ShowdownStateType.ShowdownFightState, ShowdownState.ShowdownStateType.ShowdownResolveState), new ShowdownResolveState());
    }
    
    /// <summary>
    /// Validates the transition from the current state to the nextBattleState passed in.
    /// </summary>
    /// <param name="nextBattleState"> The desired next battle state. </param>
    /// <returns> Returns the next state or null depending on the validity of the state transition requested. </returns>
    private IShowdownState ValidateStateTransition(ShowdownState.ShowdownStateType nextBattleState)
    {
        IShowdownState nextState = null;

        ShowdownStateTransition transition = new ShowdownStateTransition(currentState.StateType, nextBattleState);
        if(!transitions.TryGetValue( transition, out nextState ))
        {
            UnityEngine.Debug.LogError("Invalid Transition");
        }

        return nextState;
    }
    
    /// <summary>
    /// Requests a next state.
    /// </summary>
    /// <param name="nextState"> the next state we want. </param>
    /// <returns> returns a valid next state or null. </returns>
    private IShowdownState GetNextState(ShowdownState.ShowdownStateType nextState)
    {
        return ValidateStateTransition(nextState);
    }

    #endregion
}