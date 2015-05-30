using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
        currentState = _state;
    }
}