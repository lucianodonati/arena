﻿using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 10000.0f;
    private Rigidbody2D PRB;
    private Player player;
    private bool onePress = true;
    public float blinkDist = 11.0f;
    public float blinkCD = 5.0f;

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
        {
            if (Input.GetButtonDown("ShowdownButton" + player.id) && onePress)
            {
                Showdown.GetInstance().ExecuteAttack(player);
                onePress = false;
            }
        }
        else
        {
            blinkCD -= Time.deltaTime;

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

            float xAxis = Input.GetAxisRaw("RightStickXC" + player.id);
            float yAxis = Input.GetAxisRaw("RightStickYC" + player.id);

            if (Input.GetAxisRaw("Blink" + player.id) > 0.0f && blinkCD <= 0.0f)
            {
                blinkCD = 5.0f;

                Vector3 newPos = new Vector3((blinkDist * xAxis), -(blinkDist * yAxis));
                Vector3 newNewPos = player.transform.position + newPos;

                player.transform.position = newNewPos;

                playerVelocityX = playerSpeed * xAxis * Time.deltaTime * 30;
                playerVelocityY = playerSpeed * yAxis * Time.deltaTime * 30;

                PRB.velocity = new Vector2(0, 0);
                PRB.AddForce(new Vector2(playerVelocityX, -playerVelocityY));
            }
        }
    }
}