using System.Collections;
using UnityEngine;

public class Projectile_Spawner : MonoBehaviour
{
    public Projectile fireball;
    public AudioClip launchSound;
    private float gameTime;
    public Vector2 projectileVelocity;
    private bool canFire;
    private Player owner;
    private Rigidbody RB;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canFire)
            {
                fireball.RB.velocity = new Vector3(1.0f, 0.0f);
                fireball.owner = owner;
            }
        }
    }
}