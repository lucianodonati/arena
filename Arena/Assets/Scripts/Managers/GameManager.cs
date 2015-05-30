using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum GameState
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
    }
}