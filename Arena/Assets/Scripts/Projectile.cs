using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float killTimer = 5;
    public Player owner;

    // Use this for initialization
    private void Start()
    {
        GetComponent<SoundPlayer>().PlaySound("Fire");
    }

    // Update is called once per frame
    private void Update()
    {
        killTimer -= Time.deltaTime;
        if (killTimer <= 0.0f)
            Destroy(gameObject);

        Vector2 toTarget = (Vector2)owner.enemy.transform.position - (Vector2)transform.position;
        Vector2 newDirection = Vector2.Lerp(GetComponent<Rigidbody2D>().velocity.normalized, toTarget.normalized, Time.deltaTime * 1.7f);
        GetComponent<Rigidbody2D>().velocity = newDirection * 100;
    }
}