using System.Collections;
using UnityEngine;

public class Projectile_Spawner : MonoBehaviour
{
    private Player daddy;
    public float shootTimer = 0.5f;
    public Projectile fireball;
    public AudioClip launchSound;
    private float gameTime;
    public Vector2 projectileVelocity;
    private bool canFire;
    private float cooldown; // Cooldown that must run out before the player can fire another projectile

    // Use this for initialization
    private void Start()
    {
        canFire = true;
        cooldown = 1.5f;
        gameTime = 0.0f;
        daddy = transform.parent.GetComponent<Player>();
    }

    // Update is called once per frame
    private void Update()
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

        //Have the spawner create a projectile when the player presses the button
        if (Input.GetAxisRaw("Fire" + daddy.id) > 0.0f)
        {
            //Checks to see if the player can fire a projectile
            if (canFire)
            {
                canFire = !canFire;
                Projectile FB = (Projectile)Instantiate(fireball, transform.position, transform.rotation);
                //FB.RB.position = this.transform.position;
                FB.GetComponent<Rigidbody2D>().velocity = new Vector2(5.0f, 0.0f);
                FB.owner = GameObject.Find("Player " + daddy.id).GetComponent<Player>();
            }
        }
    }
}