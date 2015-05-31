using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 10000.0f;
    private Rigidbody2D PRB;
    private Player player;
    private bool onePress = true;
    // Use this for initialization
    private void Start()
    {
        player = GetComponent<Player>();
        PRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().currentState == GameManager.GameState.Showdown)
            if (Input.GetButtonDown("ShowdownButton" + player.id) && onePress)
            {

                Showdown.GetInstance().ExecuteAttack(player);
                onePress = false;
            }


        float playerVelocityX = 0, playerVelocityY = 0;
        {
            float axis = Input.GetAxisRaw("LeftStickXC" + player.id);
            if (axis != 0)
                playerVelocityX = playerSpeed * axis * Time.deltaTime;
        }

        {
            float axis = Input.GetAxisRaw("LeftStickYC" + player.id);
            if (axis != 0)
                playerVelocityY = playerSpeed * axis * Time.deltaTime;
        }

        //PRB.velocity += new Vector2(playerVelocityX, -playerVelocityY);
        PRB.AddForce(new Vector2(playerVelocityX, -playerVelocityY));
    }
}