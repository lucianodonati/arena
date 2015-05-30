using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10;
    private Vector3 vel = new Vector3(1, 1, 1);
    private Vector3 pos = new Vector3(0, 0, 0);
    public AudioClip sound;
    public Player owner;
    private Projectile_Spawner launcher;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Player" || hit.gameObject.tag == "Platform" || hit.gameObject.tag == "Lava")
        {
            print("hit " + hit.gameObject.name);
            Destroy(this.gameObject);
        }
    }
}