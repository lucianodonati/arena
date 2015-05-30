using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // If you need to see a variable, comment out the [HideInInspector] and when you're done debugging put it back =)

    [HideInInspector]
    public float percentage = 0.0f;

    [HideInInspector]
    public bool alive = true;

    // Sounds
    public AudioClip hurtSound;

    // Projectile
    public GameObject projectile;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void takeDamage(float _dam)
    {
        percentage += _dam;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Showdown
        }
        else if (collision.gameObject.tag == "Projectile")
        {
            // Playsound
        }
    }
}