﻿using System.Collections;
using UnityEngine;

public class Projectile_Spawner : MonoBehaviour
{
    private Player daddy;

    public float shootTimer = 1.5f;
    public Projectile fireball;
    private float gameTime;
    public Vector2 projectileVelocity;
    private bool canFire;

    // private float cooldown; // Cooldown that must run out before the player can fire another projectile
    public float fbSpeed = 1500.0f;

    // Use this for initialization
    private void Start()
    {
        daddy = transform.parent.transform.parent.GetComponent<Player>();
        canFire = true;
        // cooldown = 1.5f;
        gameTime = 0.0f;
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().currentState != GameManager.GameState.Showdown)
        {
            if (!canFire)
            {
                gameTime += Time.deltaTime;

                if (gameTime >= shootTimer)
                {
                    canFire = !canFire;
                    gameTime = 0.0f;
                }
            }

            float xAxis = Input.GetAxisRaw("RightStickXC" + daddy.id);
            float yAxis = Input.GetAxisRaw("RightStickYC" + daddy.id);

            float angle2 = Mathf.Atan2(yAxis, xAxis);
            float angle3 = Mathf.Rad2Deg * angle2;

            float angle = (xAxis + yAxis * 90);

            transform.parent.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle3));

            //Have the spawner create a projectile when the player presses the button
            if (Input.GetAxisRaw("Fire" + daddy.id) < 0.0f)
            {
                //Checks to see if the player can fire a projectile
                if (canFire)
                {
                    ShootFireball();
                }
            }
        }
    }

    public void ShootFireball()
    {
        float xAxis = Input.GetAxisRaw("RightStickXC" + daddy.id);
        float yAxis = Input.GetAxisRaw("RightStickYC" + daddy.id);

        canFire = !canFire;
        Projectile FB = (Projectile)Instantiate(fireball, transform.position, transform.rotation);

        FB.owner = GameObject.Find("Player " + daddy.id).GetComponent<Player>();

        Vector2 aimDirection = new Vector2((fbSpeed * xAxis * Time.deltaTime), -(fbSpeed * yAxis * Time.deltaTime));

        FB.GetComponent<Rigidbody2D>().velocity = aimDirection;
    }
}