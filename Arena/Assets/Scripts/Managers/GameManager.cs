using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Dictionary<int, Player> players;
    public Player playerPrefab;
    public float gameTime = 0.0f;
    public Showdown showdown;
    public Player showdown1, showdown2;
    public int rounds = 1;
    public int m_nPlayerCount;
    public float m_fWinTimer;
    public string m_sWinMessege;

    public enum GameState
    {
        MainMenu, Fighting, Showdown, Win
    }

    public GameState currentState = GameState.MainMenu;

    // Use this for initialization
    private void Start()
    {
        players = new Dictionary<int, Player>(Input.GetJoystickNames().Length);
        Debug.Log(Input.GetJoystickNames().Length + " players detected");
        DontDestroyOnLoad(this);

        CreatePlayer("Luciano");
        CreatePlayer("Brian");

        m_nPlayerCount = players.Count;
        m_fWinTimer = 3.0f;
    }

    // Update is called once per frame
    private void Update()
    {
        if (m_nPlayerCount <= 1)
            currentState = GameState.Win;

        switch (currentState)
        {
            case GameState.MainMenu:
                break;

            case GameState.Fighting:
                gameTime += Time.deltaTime;
                break;

            case GameState.Showdown:
                //showdown1.movementEnabled = false;
                //showdown2.movementEnabled = false;
                break;

            case GameState.Win:

                WinMessege();
                if (m_fWinTimer <= 0.0f)
                {
                    m_sWinMessege = "";
                    m_fWinTimer = 3.0f;
                    currentState = GameState.Fighting;
                    m_nPlayerCount = players.Count;
                    Application.LoadLevel(Application.loadedLevel);
                    Destroy(this.gameObject); 
                }
                break;

            default:
                break;
        }
    }

    #region Player

    public void CreatePlayer(string _name)
    {
        Player newPlayer = Instantiate(playerPrefab);
        newPlayer.id = players.Count + 1;

        Vector3 startingPos = new Vector3(15.6f, 16.8f, -7.0f);
        switch (newPlayer.id % 4)
        {
            case 0:
                startingPos.x = -15.6f;
                newPlayer.myColor = Color.red;
                break;

            case 1:
                startingPos.x = 15.6f;
                newPlayer.myColor = Color.blue;
                break;

            case 2:
                startingPos.x = -15.6f;
                startingPos.y = -16.8f;
                newPlayer.myColor = Color.green;
                break;

            case 3:
                startingPos.x = 15.6f;
                startingPos.y = -16.8f;
                newPlayer.myColor = Color.magenta;
                break;
        }
        newPlayer.transform.position = startingPos;

        newPlayer.playerName = _name;
        newPlayer.name = "Player " + newPlayer.id;
        players.Add(newPlayer.id, newPlayer);
    }

    public void PlayerDied(int _id)
    {
        players.Remove(_id);
    }

    #endregion Player

    public void GoShowdown(Player p1, Player p2)
    {
        if (currentState != GameState.Showdown)
        {
            showdown1 = p1;
            showdown2 = p2;
            setState(GameState.Showdown);
            Showdown.GetInstance().InitShowdown(showdown1, showdown2);
        }
    }

    public void setState(GameState _state)
    {
        if ((int)_state < 2)
            Application.LoadLevel((int)_state);

        currentState = _state;
    }

    public void setState(string stateString)
    {
        try
        {
            currentState = (GameState)(Enum.Parse(typeof(GameState), stateString, true));

            if ((int)currentState < 2)
                Application.LoadLevel((int)currentState);
        }
        catch (ArgumentNullException e)
        {
            Debug.LogError(e.Message);
        }
    }

    public void WinMessege()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        Player tempPlayer = p.GetComponent<Player>();
        string name = tempPlayer.name;
        m_fWinTimer -= Time.deltaTime;
        m_sWinMessege = name + " WINS";
    }

    void OnGUI()
    {

        GUIStyle style = new GUIStyle();
        style.fontSize = 52;
        style.normal.textColor = Color.green;
        style.font = (Font)Resources.Load("full Pack 2025", typeof(Font));
        GUI.Label(new Rect(Screen.width/2.0f - Screen.width/6 ,Screen.height/2.0f, 200.0f, 100.0f),m_sWinMessege,style);

    }
}