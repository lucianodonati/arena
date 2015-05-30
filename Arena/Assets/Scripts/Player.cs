using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float percentage = 0.0f;
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