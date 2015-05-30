public abstract class ShowdownState : IShowdownState
{
    #region Public Enumerations

    public enum ShowdownStateType
    {
        ShowdownInitState,
        ShowdownSuspenseState,
        ShowdownFightState,
        ShowdownResolveState,
    };

    #endregion

    #region Private Variables

    /// <summary>
    /// The showdownstatetype that of each class that inherits from showdown state.
    /// </summary>
    private ShowdownStateType stateType;

    #endregion

    #region Constructor

    /// <summary>
    /// Default Constructor
    /// </summary>
    public ShowdownState()
    {        
    }

    #endregion

    #region IShowdownState Methods

    /// <summary>
    /// Occurs when the state is entered.
    /// </summary>
    public virtual void OnEnter()
    {

    }

    /// <summary>
    /// Occurs when the state is left.
    /// </summary>
    public virtual void OnExit()
    {

    }

    /// <summary>
    /// Occurs everytime an update is called. (Once per frame in this instance)
    /// </summary>
    public virtual void Update()
    {

    }

    /// <summary>
    /// The showdown state type of the state. Used in showdown state transitions.
    /// </summary>
    public ShowdownState.ShowdownStateType StateType
    {
        get
        {
            return this.stateType;
        }
        set 
        {
            this.stateType = value;
        }
    }

    #endregion
}

