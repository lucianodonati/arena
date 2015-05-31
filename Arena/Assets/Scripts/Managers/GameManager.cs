﻿using System;
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
    public Player showdown1, showdown2;
    public int rounds = 1;

    public int m_nPlayerCount;
    public float m_fWinTimer;
    public string m_sWinMessege;

    public Camera mainCamera, showdownCamera;
    public GameObject HUD;

    private Texture2D tex, tex2;
    private Rect playRect, creditRect, exitRect;
    private Ray ray;
    private RaycastHit hit;

    public enum GameState
    {
        MainMenu, Fighting, Showdown, Win
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
            setState("Showdown");
            Showdown temp = Instantiate(showdownPrefab);

            temp.showdownCamera = showdownCamera;
            temp.HUD = HUD;
            temp.mainCamera = Camera.main;

            Showdown.GetInstance().InitShowdown(showdown1, showdown2);
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
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        Player tempPlayer = p.GetComponent<Player>();
        string name = tempPlayer.name;
        m_fWinTimer -= Time.deltaTime;
        m_sWinMessege = name + " WINS";
    }

    private void OnGUI()
    {
        GUIStyle gStyle = new GUIStyle();

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