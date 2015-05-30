using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float killTimer = 10;
    private Vector3 vel = new Vector3(1, 1, 1);
    private Vector3 pos = new Vector3(0, 0, 0);
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
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        //if (hit.gameObject.tag == "Player")
        //{
        //    Destroy(this.gameObject);
        //}
    }
}