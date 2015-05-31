using System.Collections;
using UnityEngine;

public class Projectile_Spawner : MonoBehaviour
{
    private Player daddy;

    public float shootTimer = 0.5f;
    public Projectile fireball;
    public AudioClip launchSound;
    private float gameTime;
    public Vector2 projectileVelocity;
    private bool canFire;

    // private float cooldown; // Cooldown that must run out before the player can fire another projectile
    public float fbSpeed = 1500.0f;

    // Use this for initialization
    private void Start()
    {
        daddy = transform.parent.transform.parent.GetComponent<Player>();
        canFire = true;
        // cooldown = 1.5f;
        gameTime = 0.0f;
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().currentState != GameManager.GameState.Showdown) ;
        {


            if (!canFire)
            {
                gameTime += Time.deltaTime;

                if (gameTime >= shootTimer)
                {
                    canFire = !canFire;
                    gameTime = 0.0f;
                }
            }

            float xAxis = Input.GetAxisRaw("RightStickXC" + daddy.id);
            float yAxis = Input.GetAxisRaw("RightStickYC" + daddy.id);

            // xAxis = Mathf.Abs(xAxis);
            //  yAxis = Mathf.Abs(yAxis);

            float angle2 = Mathf.Atan2(yAxis, xAxis);
            float angle3 = Mathf.Rad2Deg * angle2;
            //Vector3 aimDirection = new Vector3(xAxis, yAxis, 0);
            //float angle = Vector3.Angle(aimDirection, new Vector3(0, 0, 1));
            //Vector3 cross = Vector3.Cross(aimDirection, new Vector3(0, 0, 1));
            //if (cross.z > 0)
            //    angle = 360 - angle;
            //if (xAxis != 0 && yAxis != 0)
            //{
            //    //     transform.RotateAround(daddy.transform.position, Vector3.back, angle);
            //}
            float angle = (xAxis + yAxis * 90);

            transform.parent.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle3));

            //Have the spawner create a projectile when the player presses the button
            if (Input.GetAxisRaw("Fire" + daddy.id) < 0.0f)
            {
                //Checks to see if the player can fire a projectile
                if (canFire)
                {
                    canFire = !canFire;
                    Projectile FB = (Projectile)Instantiate(fireball, transform.position, transform.rotation);
                    //FB.RB.position = this.transform.position;
                    FB.owner = GameObject.Find("Player " + daddy.id).GetComponent<Player>();

                    {
                        print("X " + xAxis);
                        print("Y " + yAxis);
                        print(angle);

                        Vector2 aimDirection = new Vector2((fbSpeed * xAxis * Time.deltaTime), -(fbSpeed * yAxis * Time.deltaTime));

                        FB.GetComponent<Rigidbody2D>().velocity = aimDirection;
                    }
                }
            }
        }
    }
}