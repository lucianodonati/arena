using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float playerSpeed = 150.0f;
    public float playerVelocityX;
    public float playerVelocityZ;

    public Rigidbody PRB;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerVelocityX = playerSpeed * Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        playerVelocityZ = playerSpeed * Input.GetAxisRaw("Vertical") * Time.deltaTime;



        PRB.velocity = new Vector3(playerVelocityX, 0, playerVelocityZ);

       
    }
}
