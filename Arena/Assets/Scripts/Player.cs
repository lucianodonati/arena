using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float percentage = 0.0f;
    public bool alive = true;

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
        }
        else if (collision.gameObject.tag == "Projectile")
        {
        }
    }
}