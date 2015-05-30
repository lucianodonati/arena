using UnityEngine;
using System.Collections;

public class GameControls : MonoBehaviour 
{
    Player player = new Player();
    Player player2 = new Player();
	// Use this for initialization
	void Start () {


        
        player.playerName = "testplayer1";
        player2.playerName = "testplayer2";

	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Showdown.GetInstance().ExecuteAttack(player);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Showdown.GetInstance().ExecuteAttack(player2);
        }
	}
}
