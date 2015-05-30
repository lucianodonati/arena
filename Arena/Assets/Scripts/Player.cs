using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // If you need to see a variable, comment out the [HideInInspector] and when you're done debugging put it back =)

    //[HideInInspector]
    public float percentage = 0.0f;

    public Rigidbody PRB;

    [HideInInspector]
    public bool alive = true;

    // Sounds
    public AudioClip hurtSound;

    // Projectile
    public GameObject projectile;

    // Lava
    public bool onLava = false;

    private float lavaTimer = 0.0f, ticEvery = 0.0f, lavaDamage;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        // Timers
        if (onLava)
        {
            if (lavaTimer <= 0.0f)
            {
                takeDamage(lavaDamage);
                lavaTimer = ticEvery;
            }
            else
                lavaTimer -= Time.deltaTime;
        }
    }

    public void takeDamage(float _dam)
    {
        percentage += _dam;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collGO = collision.gameObject;

        if (collGO.tag == "Player")
        {
            // Showdown
        }
        else if (collGO.tag == "Projectile"/* && collGO.GetComponent<Projectile>().owner != this.gameObject*/)
        {
            // Playsound

            Vector3 force = collGO.GetComponent<Rigidbody>().velocity.normalized;

            force *= 1.0f + percentage;

            PRB.AddForce(force);
        }
        else if (collGO.tag == "Lava")
        {
            // Playsound

            Lava theLava = collGO.GetComponent<Lava>();

            ticEvery = theLava.ticTimer;
            lavaDamage = theLava.damage;
            onLava = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //GameObject collGO = collision.gameObject;

        if (collision.gameObject.tag == "Lava")
        {
            onLava = false;
        }
    }
}