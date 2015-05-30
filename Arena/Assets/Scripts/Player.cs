using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // If you need to see a variable, comment out the [HideInInspector] and when you're done debugging put it back =)

    #region PlayerStuff

    public int id;
    public Color myColor;
    public string playerName;
    private Renderer myRenderer;

    //[HideInInspector]
    public float percentage = 0.0f;

    private Rigidbody2D PRB;

    [HideInInspector]
    public bool alive = true;

    #endregion PlayerStuff

    #region Sounds

    public SoundPlayer sounds;

    #endregion Sounds

    #region Projectile

    public GameObject projectile;

    #endregion Projectile

    #region Lava

    public bool onLava = false;

    private float lavaTimer = 0.0f, ticEvery = 0.0f, lavaDamage;

    #endregion Lava

    // Use this for initialization
    private void Start()
    {
        myRenderer = GetComponent<Renderer>();
        PRB = GetComponent<Rigidbody2D>();
        sounds = GetComponent<SoundPlayer>();

        GetComponent<SpriteRenderer>().color = myColor;
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
                if (Random.Range(0, 5) == 0)
                    sounds.PlaySound("LavaHit");
            }
            else
                lavaTimer -= Time.deltaTime;

            // Die

            if (!myRenderer.isVisible)
                GameObject.Find("GameManager").GetComponent<GameManager>().PlayerDied(id);
        }
    }

    public void takeDamage(float _dam)
    {
        percentage += _dam;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject collGO = collision.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collGO = collision.gameObject;
        if (collGO.tag == "Player")
        {
            // Showdown
        }
        else if (collGO.tag == "Projectile" && collGO.GetComponent<Projectile>().owner != this)
        {
            // Playsound

            Vector2 force = collGO.GetComponent<Rigidbody2D>().velocity.normalized;

            force *= 5000.0f + percentage * 75.0f;

            PRB.AddForce(force);

            takeDamage(10.0f);

            Destroy(collGO);
        }
        else if (collGO.tag == "Platform")
            onLava = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject collGO = collision.gameObject;

        if (collGO.tag == "Platform")
        {
            // Playsound

            Lava theLava = GameObject.Find("Lava").GetComponent<Lava>();
            ticEvery = theLava.ticTimer;
            lavaDamage = theLava.damage;
            onLava = true;
        }
    }
}