using UnityEngine;
using UnityEngine.UI;

public class Showdown : MonoBehaviour 
{
    #region Singleton

    /// <summary>
    /// The instance of Showdown
    /// </summary>
    private static Showdown instance;

    /// <summary>
    /// Returns the singleton instance of Showdown
    /// </summary>
    /// <returns> the instance of Showdown </returns>
    public static Showdown GetInstance()
    {
        if (instance == null)
        {
            throw new UnityException("trying to access instance of a null instance.");
        }

        return instance;
    }

    /// <summary>
    /// Happens before the first frame... use this to create our singleton game object.
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        stateMachine = new ShowdownStateMachine(new ShowdownInitState());
    }

    #endregion

    #region Serialize Fields

    [SerializeField]
    Image FightUIElement = null;

    #endregion

    #region Private Member Variables

    /// <summary>
    /// 
    /// </summary>
    private ShowdownStateMachine stateMachine = null;
    
    /// <summary>
    /// 
    /// </summary>
    private float timer = 0.0f;
    
    /// <summary>
    /// 
    /// </summary>
    private float suspenseTime = 0.0f;

    /// <summary>
    /// 
    /// </summary>
    private float suspenseTimer = 0.0f;

    #endregion

    #region Accessors/Modifiers

    /// <summary>
    /// 
    /// </summary>
    public float SuspenseTime
    {
        get 
        {
            return suspenseTime;
        }
        set
        {
            this.suspenseTime = value;
        }
    }

    #endregion

    public string currentState = "";
    public string suspenseTimeString = "";
    public string suspenseTimerString = "";

    /// <summary>
    /// Use this for initialization
    /// </summary>
	void Start () 
    {
        
	}

	/// <summary>
    /// Update is called once per frame
    /// </summary>
	void Update () 
    {
        stateMachine.CurrentState.Update();

        currentState = stateMachine.CurrentState.StateType.ToString();
        suspenseTimeString = suspenseTime.ToString();
        suspenseTimerString = suspenseTimer.ToString();

        if ( stateMachine.CurrentState.StateType.Equals( ShowdownState.ShowdownStateType.ShowdownSuspenseState ) )
        {
            RunSuspenseTimer();
        }
	}

    /// <summary>
    /// 
    /// </summary>
    public void InitShowdown()
    {     
        timer = 0.0f;
        suspenseTimer = 0.0f;
        suspenseTime = Random.Range(0.0f, 10.0f);
        FightUIElement.enabled = false;

        if (stateMachine != null)
        {
            stateMachine.TransitionToState(ShowdownState.ShowdownStateType.ShowdownSuspenseState);
        }
    }
    
    public void Fight()
    {
        if( FightUIElement != null )
        {
            FightUIElement.enabled = true;
        }
        else
        {
            Debug.LogError("FightUIElement is null");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void RunSuspenseTimer()
    {
        suspenseTimer += Time.deltaTime;

        if (suspenseTimer >= suspenseTime)
        {
            stateMachine.TransitionToState(ShowdownState.ShowdownStateType.ShowdownFightState);
        }
    }

}
