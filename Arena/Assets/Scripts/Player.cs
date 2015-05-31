using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // If you need to see a variable, comment out the [HideInInspector] and when you're done debugging put it back =)

    #region PlayerStuff

    public int id;
    public Color myColor;
    public string playerName;
    private Renderer myRenderer;
    private GameManager gm;
    public Player enemy;
    public Vector3 m_vStartingPosition;
    private float showDownTimer = 0.0f;
    public float showDownCooldown = 5.0f;
   

    //[HideInInspector]
    public float percentage = 0.0f;

    private Rigidbody2D PRB;

    [HideInInspector]
    public bool alive = true;

    #endregion PlayerStuff

    #region GUI

    private Text percText;
    //private Text blinkCD;
    //private Text shieldCD;

    #endregion GUI

    #region Sounds

    private SoundPlayer sounds;

    #endregion Sounds

    #region Lava

    public bool onLava = false;

    private float lavaTimer = 0.0f, ticEvery = 0.0f, lavaDamage;

    #endregion Lava

    // Use this for initialization
    private void Start()
    {
        percText = GameObject.Find("HealthPercentage" + id).GetComponent<Text>();
        //blinkCD = GameObject.Find("BlinkUI" + id).GetComponent<Text>();
        //shieldCD = GameObject.Find("ShieldUI" + id).GetComponent<Text>();

        myRenderer = GetComponent<Renderer>();
        PRB = GetComponent<Rigidbody2D>();
        sounds = GetComponent<SoundPlayer>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        GetComponent<SpriteRenderer>().color = myColor;
        if (id == 1)
            enemy = GameObject.Find("Player 2").GetComponent<Player>();
        else
            enemy = GameObject.Find("Player 1").GetComponent<Player>();

        GameObject.Find("P" + id.ToString() + "_img").GetComponent<Image>().color = myColor;
    }

    public void SetColor()
    {
        string obj = "PlayerShowdown" + id.ToString();
        GameObject.Find(obj).SendMessage("SetColor", myColor);
    }

    // Update is called once per frame
    private void Update()
    {
        if (gm.m_fWinTimer < 3.0f || alive == false)
        {
            return;
        }

        if (showDownTimer > 0.0f)
            showDownTimer -= Time.deltaTime;

        float colorChange = Mathf.Clamp(1.0f - percentage / 100.0f, 0.0f, 1.0f);
        percText.text = percentage.ToString() + "%";
        percText.color = new Color(1.0f, colorChange, colorChange, 1.0f);

        //blinkCD.text = gameObject.GetComponent<PlayerController>().blinkCD.ToString();
        // Timers
        if (onLava && GameObject.Find("GameManager").GetComponent<GameManager>().currentState != GameManager.GameState.Showdown)
        {
            if (lavaTimer <= 0.0f)
            {
                takeDamage(lavaDamage);
                lavaTimer = ticEvery;
                if (Random.Range(0, 2) == 0)
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

    public void pushBack(Vector2 pushTo)
    {
        Vector2 force = new Vector2(-pushTo.x, -pushTo.y);

        force *= 5000.0f;

        PRB.AddForce(force);

        takeDamage(50.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collGO = collision.gameObject;

        if (collGO.tag == "Player")
        {
            if (showDownTimer <= 0.0f)
                // Showdown
                gm.GoShowdown();
            showDownTimer = showDownCooldown;
            PRB.velocity = new Vector3(0, 0, 0);
        }
        else if (collGO.tag == "Projectile" && collGO.GetComponent<Projectile>().owner != this && gameObject.GetComponent<PlayerController>().shielded == false)
        {
            // Playsound

            Vector2 force = collGO.GetComponent<Rigidbody2D>().velocity.normalized;

            force *= 5000.0f + percentage * 75.0f;

            PRB.AddForce(force);

            takeDamage(10.0f);

            Destroy(collGO);
        }
        else if (collGO.tag == "Projectile" && collGO.GetComponent<Projectile>().owner != this && gameObject.GetComponent<PlayerController>().shielded == true)
        {
            Destroy(collGO);
            Projectile_Spawner child = transform.GetComponentInChildren<Projectile_Spawner>();
            child.ShootFireball();


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
        else if (collGO.tag == "Lava")
        {
            //DIE
            gm.m_nPlayerCount--;
            alive = false;
        }
    }
}