﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Dictionary<int, Player> players;
    public Player playerPrefab;
    public float gameTime = 0.0f;
    public Showdown showdown;

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
    }

    // Update is called once per frame
    private void Update()
    {
        if (players.Count == 1)
            currentState = GameState.Win;

        switch (currentState)
        {
            case GameState.MainMenu:
                break;

            case GameState.Fighting:
                gameTime += Time.deltaTime;
                break;

            case GameState.Showdown:
                //Showdown.GetInstance().InitShowdown(player, player2);
                break;

            case GameState.Win:
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

        Vector3 startingPos = new Vector3(0.9f, 0.6f, -7.0f);
        switch (newPlayer.id % 4)
        {
            case 0:
                startingPos.x = -0.9f;
                newPlayer.myColor = Color.red;
                break;

            case 1:
                startingPos.x = 0.9f;
                newPlayer.myColor = Color.blue;
                break;

            case 2:
                startingPos.x = -0.9f;
                startingPos.y = -0.6f;
                newPlayer.myColor = Color.green;
                break;

            case 3:
                startingPos.x = 0.9f;
                startingPos.y = -0.6f;
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
}