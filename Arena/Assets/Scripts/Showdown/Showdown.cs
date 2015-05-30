using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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
    
    [SerializeField] private GameObject showdownCanvas = null;
    [SerializeField] private float suspenseLowerLimit = 1.0f;
    [SerializeField] private float suspenseUpperLimit = 10.0f;
    [SerializeField] private Image fightUIElement = null;
    #endregion
    
    #region Accessors/Modifiers

    /// <summary>
    /// 
    /// </summary>
    public ShowdownStateMachine StateMachine
    {
        get
        {
            return stateMachine;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public bool IsAcceptingAttacks
    {
        get
        {
            return isAcceptingAttacks;
        }
        set 
        {
            isAcceptingAttacks = value;
        }
    }

    #endregion

    #region Private Member Variables
    
    /// <summary>
    /// 
    /// </summary>
    private ShowdownStateMachine stateMachine = null;
    
    /// <summary>
    /// 
    /// </summary>
    private bool isAcceptingAttacks = false;

    /// <summary>
    /// 
    /// </summary>
    private float timer = 0.0f;

    //private Dictionary<string, Player> playersInShowdown = new Dictionary<string, Player>();

    #endregion

    public string currentState = "";

    public Text winnerName;

    /// <summary>
    /// This is so we can queue up who hit the button when, we want to make sure that if they both hit the button 
    /// in the same frame that we correctly make it a draw. (Unlikely, but possible.)
    /// </summary>
    private Dictionary<string, ShowdownAttack> attackMap = new Dictionary<string, ShowdownAttack>();

    private Player player1 = new Player();
    
    private Player player2 = new Player();

    
    // Use this for initialization

    void Start()
    {

	}

	/// <summary>
    /// Update is called once per frame
    /// </summary>
	void Update () 
    {
        if(Input.GetKeyUp(KeyCode.A))
        {
            Player player = new Player();
            Player player2 = new Player();

            player.playerName = "testplayer1";
            player2.playerName = "testplayer2";

            InitShowdown(player, player2);
        }

        stateMachine.CurrentState.Update();

        #region debug information
        currentState = stateMachine.CurrentState.StateType.ToString();
        #endregion
	}


    /// <summary>
    /// 
    /// </summary>
    public void InitShowdown( Player player1, Player player2 )
    {
        this.player1 = player1;
        this.player2 = player2;

        attackMap.Clear();
        fightUIElement.enabled = false;
        winnerName.text = "";
        showdownCanvas.GetComponent<ShowdownAnimation>().StartShowdown();
    }
    
    public void BeginShowdown()
    {
        stateMachine.TransitionToState(ShowdownState.ShowdownStateType.ShowdownSuspenseState);
    }

    public void DisplayWinner( Player player )
    {
        this.winnerName.text = player.playerName;
    }

    public void Fight()
    {
        if( fightUIElement != null )
        {
            fightUIElement.enabled = true;
        }
        else
        {
            Debug.LogError("FightUIElement is null");
        }
    }

    /// <summary>
    /// Runs the timer for the entire showdown.
    /// </summary>
    public void RunTimer()
    {
        timer += Time.deltaTime;
    }

    public void ExecuteAttack( Player player )
    {
        if ( IsAcceptingAttacks )
        {
            ShowdownAttack showdownAttack = new ShowdownAttack();

            showdownAttack.Player = player;
            showdownAttack.StateWhenExecuted = stateMachine.CurrentState.StateType;
            showdownAttack.TimeOfExecution = timer;

            if (!attackMap.ContainsKey(player.playerName))
            {
                attackMap.Add(player.playerName, showdownAttack);
            }

            stateMachine.TransitionToState( ShowdownState.ShowdownStateType.ShowdownResolveState );
        }
    }

    public Player ProcessAttacks()
    {
        ShowdownAttack player1Attack=null;
        ShowdownAttack player2Attack=null;
        
        if( attackMap.TryGetValue( player1.playerName, out player1Attack ) )
        {
             if( attackMap.TryGetValue( player2.playerName, out player2Attack ) )
             {
                 // Both players registered an attack, let's compare.
                 if( player1Attack.TimeOfExecution < player2Attack.TimeOfExecution )
                 {
                     if( player1Attack.StateWhenExecuted.Equals( ShowdownState.ShowdownStateType.ShowdownSuspenseState ) )
                     {
                         return player2;
                     }
                     else
                     {
                         return player1;
                     }
                 }
                 else 
                 {
                     if (player2Attack.StateWhenExecuted.Equals(ShowdownState.ShowdownStateType.ShowdownSuspenseState))
                     {
                         return player1;
                     }
                     else
                     {
                         return player2;
                     }
                 }
             }
             else
             {
                 // Only player1 registered an attack, see if it is in suspense state or not.
                 if( player1Attack.StateWhenExecuted.Equals( ShowdownState.ShowdownStateType.ShowdownSuspenseState ) )
                 {
                     // Player 1 Attacked too early, Player 2 is the winner.
                     return player2;
                 }
                 else
                 {
                     // Player 1 Attacked during the right time, Player 1 is the winner.
                     return player1;
                 }                 
             }
        }
        else
        {
            if (attackMap.TryGetValue(player2.playerName, out player2Attack))
            {
                // Only player1 registered an attack, see if it is in suspense state or not.
                if ( player2Attack.StateWhenExecuted.Equals( ShowdownState.ShowdownStateType.ShowdownSuspenseState ) )
                {
                    // Player 2 Attacked too early, Player 1 is the winner.
                    return player1;
                }
                else
                {
                    // Player 2 Attacked during the right time, Player 2 is the winner.
                    return player2;
                }      
            }
            else
            {
                Debug.LogError("this should never happen...");
                return null;
            }
        }
    }
}

public class ShowdownAttack
{
    #region Private Member Variables

    /// <summary>
    /// 
    /// </summary>
    private Player player;
    
    /// <summary>
    /// 
    /// </summary>
    private ShowdownState.ShowdownStateType stateWhenExecuted;

    /// <summary>
    /// 
    /// </summary>
    private float timeOfExecution;

    #endregion

    #region Accessors/Modifiers

    /// <summary>
    /// 
    /// </summary>
    public Player Player
    {
        get 
        {
            return player;
        }
        set 
        {
            player = value;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public ShowdownState.ShowdownStateType StateWhenExecuted
    {
        get
        {
            return stateWhenExecuted;
        }
        set
        {
            stateWhenExecuted = value;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public float TimeOfExecution
    {
        get
        {
            return timeOfExecution;
        }
        set
        {
            timeOfExecution = value;
        }
    }

    #endregion

    public ShowdownAttack()
    {

    }
}