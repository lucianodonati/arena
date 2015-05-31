using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 12000.0f;
    private Rigidbody2D PRB;
    private Player player;
    public float blinkDist = 11.0f;
    public float blinkCD = 5.0f;
    public bool shielded = false;
    public float shieldCD = 7.0f;
    public float shieldTimer = 0.8f;

        private Image Blink;
    private Image Shield;


    // Use this for initialization
    private void Start()
    {
        player = GetComponent<Player>();
        PRB = GetComponent<Rigidbody2D>();

        Blink = GameObject.Find("Blinkp" + player.id).GetComponent<Image>();
        Shield = GameObject.Find("Shieldp" + player.id).GetComponent<Image>();
    }

    // Update is called once per frame
    private void Update()
    {
     //   string[] meh = Input.GetJoystickNames();

        shieldCD -= Time.deltaTime;
        if (shielded == true)
        {
            shieldTimer -= Time.deltaTime;
            if (shieldTimer <= 0)
            {
                shielded = false;
                shieldTimer = 0.8f;
            }
        }

        if (Input.GetButtonDown("ShieldC" + player.id.ToString()) && shieldCD <= 0)
        {
            Shield.CrossFadeAlpha(0.0f, 0.0f, true);
            shielded = true;
            shieldCD = 7.0f;
            Shield.CrossFadeAlpha(1.0f, shieldCD, false);
        }


        if (GameObject.Find("GameManager").GetComponent<GameManager>().currentState == GameManager.GameState.Showdown)
        {
            if (Input.GetButtonDown("ShowdownButton" + player.id))
                Showdown.GetInstance().ExecuteAttack(player);
            //if (Input.GetAxisRaw("ShowdownButton") == 1 && player.id == 1)
            //    Showdown.GetInstance().ExecuteAttack(player);
        }
        else
        {
            blinkCD -= Time.deltaTime;

            float playerVelocityX = 0, playerVelocityY = 0;
            {
                float axis = Input.GetAxisRaw("LeftStickXC" + player.id);
                if (axis != 0)
                    playerVelocityX = playerSpeed * axis * Time.deltaTime;
            }

            {
                float axis = Input.GetAxisRaw("LeftStickYC" + player.id);
                if (axis != 0)
                    playerVelocityY = playerSpeed * axis * Time.deltaTime;
            }

            //PRB.velocity += new Vector2(playerVelocityX, -playerVelocityY);
            PRB.AddForce(new Vector2(playerVelocityX, -playerVelocityY));

            float xAxis = Input.GetAxisRaw("RightStickXC" + player.id);
            float yAxis = Input.GetAxisRaw("RightStickYC" + player.id);

            if (Input.GetAxisRaw("Blink" + player.id) > 0.0f && blinkCD <= 0.0f)
            {
                Blink.CrossFadeAlpha(0.0f, 0.0f, true);
                GetComponent<SoundPlayer>().PlaySound("Blink");
                blinkCD = 5.0f;

                Vector3 newPos = new Vector3((blinkDist * xAxis), -(blinkDist * yAxis));
                Vector3 newNewPos = player.transform.position + newPos;

                player.transform.position = newNewPos;

                playerVelocityX = playerSpeed * xAxis * Time.deltaTime * 10;
                playerVelocityY = playerSpeed * yAxis * Time.deltaTime * 10;

                PRB.velocity = new Vector2(0, 0);
                PRB.AddForce(new Vector2(playerVelocityX, -playerVelocityY));
                Blink.CrossFadeAlpha(1.0f, blinkCD, false);
            }
        }
    }
}