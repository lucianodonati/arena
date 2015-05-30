using System.Collections;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public float gameTime = 0.0f;

    public enum GameState
    {
        MainMenu, Fighting, Showdown, Win
    }

    public GameState currentState = GameState.MainMenu;

    // Use this for initialization
    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    private void Update()
    {
        switch (currentState)
        {
            case GameState.MainMenu:
                break;

            case GameState.Fighting:
                gameTime += Time.deltaTime;
                break;

            case GameState.Showdown:
                break;

            case GameState.Win:
                break;

            default:
                break;
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
}