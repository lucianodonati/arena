using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Dictionary<int, Player> players;
    public Player playerPrefab;
    public Showdown showdownPrefab;
    public float gameTime = 0.0f;
    public Player player1, player2;

    public int m_nPlayerCount;
    public float m_fWinTimer;
    public string m_sWinMessege;
    public int m_nRounds;
    public int[] m_nScores;

    private float roundTimer = 10.0f;

    public Camera mainCamera, showdownCamera;
    public GameObject HUD;

    private Texture2D tex, tex2;
    private Rect playRect = new Rect(), creditRect = new Rect(), exitRect = new Rect();
    private Ray ray;
    private RaycastHit hit;

    public enum GameState
    {
        MainMenu, Fighting, Showdown, Win, RoundEnd
    }

    public GameState currentState = GameState.MainMenu;

    // Use this for initialization
    private void Start()
    {
        showdownCamera = GameObject.Find("ShowdownCamera").GetComponent<Camera>();
        showdownCamera.enabled = false;
        HUD = GameObject.Find("HUD");

        players = new Dictionary<int, Player>(2);
        DontDestroyOnLoad(this);

        if (GameObject.Find("MenuHandler") != null)
        {
            Menu mnu = GameObject.Find("MenuHandler").GetComponent<Menu>();

            player1 = CreatePlayer(mnu.p1Name);
            player2 = CreatePlayer(mnu.p2Name);
        }
        else
        {
            player1 = CreatePlayer("Player 1");
            player2 = CreatePlayer("Player 2");
        }

        m_nPlayerCount = players.Count;
        m_fWinTimer = 3.0f;
        m_nRounds = 3;
        m_nScores = new int[2];
        m_nScores[0] = 0;
        m_nScores[1] = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        if (m_nPlayerCount <= 1)
        {
            if (currentState != GameState.Win)
            {
                currentState = GameState.RoundEnd;
            }
        }

        // if (players.Count == 1)
        //   currentState = GameState.Win;

        switch (currentState)
        {
            case GameState.MainMenu:
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    print(hit.collider.name);
                }
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
                    if (EndRound())
                        Application.LoadLevel(0);
                    else
                        Application.LoadLevel(Application.loadedLevel);
                    Destroy(this.gameObject);
                }
                break;

            case GameState.RoundEnd:

                if (EndRound())
                {
                    break;
                }

                currentState = GameState.Fighting;
                m_nPlayerCount = players.Count;
                player1.percentage = 0;
                player2.percentage = 0;
                break;

            default:
                break;
        }
    }

    #region Player

    public void EndShowdown()
    {
        GetComponent<SoundPlayer>().PlaySound("End");
    }

    public Player CreatePlayer(string _name)
    {
        Player newPlayer = Instantiate(playerPrefab);
        //playerPrefab = new Player ();

        //Player newPlayer = playerPrefab;

        newPlayer.id = players.Count + 1;

        Vector3 startingPos = new Vector3(15.6f, 16.8f, -7.0f);
        switch (newPlayer.id % 4)
        {
            case 1:
                startingPos.x = -15.6f;
                newPlayer.myColor = Color.red;
                break;

            case 0:
                startingPos.x = 15.6f;
                newPlayer.myColor = Color.blue;
                break;

            case 2:
                startingPos.x = -15.6f;
                startingPos.y = -16.8f;
                newPlayer.myColor = Color.blue;
                break;

            case 3:
                startingPos.x = 15.6f;
                startingPos.y = -16.8f;
                newPlayer.myColor = Color.magenta;
                break;
        }

        newPlayer.m_vStartingPosition = startingPos;
        newPlayer.transform.position = startingPos;

        newPlayer.playerName = _name;
        newPlayer.name = "Player " + newPlayer.id;
        players.Add(newPlayer.id, newPlayer);

        return newPlayer;
    }

    public void PlayerDied(int _id)
    {
        players.Remove(_id);
    }

    #endregion Player

    public void GoShowdown()
    {
        if (currentState != GameState.Showdown)
        {
            player1.SetColor();
            player2.SetColor();

            setState("Showdown");
            Showdown temp = Instantiate(showdownPrefab);

            temp.showdownCamera = showdownCamera;
            temp.HUD = HUD;
            temp.mainCamera = Camera.main;

            Showdown.GetInstance().InitShowdown(player1, player2);
        }
    }

    public void setState(string stateString)
    {
        try
        {
            currentState = (GameState)(Enum.Parse(typeof(GameState), stateString, true));
        }
        catch (ArgumentNullException e)
        {
            Debug.LogError(e.Message);
        }
    }

    public void WinMessege()
    {
        GameObject[] p = GameObject.FindGameObjectsWithTag("Player");
        Player tempPlayer1 = p[0].GetComponent<Player>();
        Player tempPlayer2 = p[1].GetComponent<Player>();
        Player tempPlayer;
        if (tempPlayer1.alive == true)
        {
            tempPlayer = tempPlayer1;
        }
        else
        {
            tempPlayer = tempPlayer2;
        }
        string name = tempPlayer.name;
        m_fWinTimer -= Time.deltaTime;
        m_sWinMessege = name + " WINS";
    }

    public bool EndRound()
    {
        GameObject[] p = GameObject.FindGameObjectsWithTag("Player");
        Player tempPlayer1 = p[0].GetComponent<Player>();
        Player tempPlayer2 = p[1].GetComponent<Player>();
        Player tempPlayer;
        if (tempPlayer1.alive == true)
        {
            tempPlayer = tempPlayer1;
        }
        else
        {
            tempPlayer = tempPlayer2;
        }

        m_nScores[tempPlayer.id - 1]++;

        if (m_nScores[tempPlayer.id - 1] >= m_nRounds / 2 + 1)
        {
            currentState = GameState.Win;
            return true;
        }

        tempPlayer1.alive = true;
        tempPlayer2.alive = true;

        tempPlayer1.transform.position = tempPlayer1.m_vStartingPosition;
        tempPlayer2.transform.position = tempPlayer2.m_vStartingPosition;
        m_nPlayerCount++;
        return false;
    }

    private void OnGUI()
    {
        tex = (Texture2D)Resources.Load("button");
        tex2 = (Texture2D)Resources.Load("buttonActive");

        GUI.backgroundColor = Color.clear;
        GUI.Button(playRect, new GUIContent(tex));
        GUI.Button(creditRect, new GUIContent(tex));
        GUI.Button(exitRect, new GUIContent(tex));

        //if (myRect.Contains(Input.mousePosition)) {
        //	GUI.Button (myRect, new GUIContent(tex2));
        //}
        GUIStyle style = new GUIStyle();
        style.fontSize = 52;
        style.normal.textColor = Color.green;
        GUI.Label(new Rect(Screen.width / 2.0f - Screen.width / 6, Screen.height / 2.0f, 200.0f, 100.0f), m_sWinMessege, style);
    }
}