using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // If you need to see a variable, comment out the [HideInInspector] and when you're done debugging put it back =)

    #region PlayerStuff
    public int enemyID;
    public int id;
    public Color myColor;
    public string playerName;
    private Renderer myRenderer;
    private GameManager gm;
    public Player enemy;

    //[HideInInspector]
    public float percentage = 0.0f;

    private Rigidbody2D PRB;

    [HideInInspector]
    public bool alive = true;


    // Hud
    public GUIStyle style;


    #endregion PlayerStuff

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

        if (id == 1)
        {
            enemy = GameObject.Find("Player 2").GetComponent<Player>();
            enemyID = 2;
        }
        else if (id == 2)
        {
            enemy = GameObject.Find("Player 1").GetComponent<Player>();
            enemyID = 1;
        }

        myRenderer = GetComponent<Renderer>();
        PRB = GetComponent<Rigidbody2D>();
        sounds = GetComponent<SoundPlayer>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        GetComponent<SpriteRenderer>().color = myColor;

        // initialize gui style 
        style = new GUIStyle();
        style.fontSize = 54;
        style.fontStyle = FontStyle.Italic;
    }

    // Update is called once per frame
    private void Update()
    {
        if (gm.m_fWinTimer < 3.0f)
	{
        return;
	} 

        // Timers
        if (onLava)
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject collGO = collision.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collGO = collision.gameObject;
        if (collGO.tag == "Player")
        {
            // Showdown
            gm.GoShowdown(this, collGO.GetComponent<Player>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collGO = collision.gameObject;

        if (collGO.tag == "Projectile" && collGO.GetComponent<Projectile>().owner != this)
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
        else if (collGO.tag == "Lava")
        {
            //DIE
            gm.m_nPlayerCount--;
            Destroy(this.gameObject);
      
        }
    }

    void OnGUI()
    {

        float colorChange = Mathf.Clamp(1.0f - percentage/100.0f,0.0f,1.0f);

        style.normal.textColor = new Color(1.0f, colorChange, colorChange, 1.0f);
        style.font = (Font)Resources.Load("full Pack 2025", typeof(Font));
        GUI.Label(new Rect(100.0f + ((id-1) * Screen.width/4), Screen.height - 50.0f, 200.0f, 100.0f), ((int)percentage).ToString() + "  %", style);
    }
}