﻿using System.Collections;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public float playerSpeed = 15.0f;
    public float playerVelocityX;
    public float playerVelocityY;

    public Rigidbody2D PRB;

    //////



    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
      
        playerVelocityX = playerSpeed * Input.GetAxisRaw("C2LeftStickX") * Time.deltaTime;
        playerVelocityY = playerSpeed * Input.GetAxisRaw("C2LeftStickY") * Time.deltaTime;




        PRB.velocity = new Vector3(playerVelocityX, -playerVelocityY, 0);

    }
}