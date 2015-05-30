using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 15.0f;
    public float playerVelocityX;
    public float playerVelocityZ;

    public Rigidbody PRB;

    //////



    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        playerVelocityX = playerSpeed * Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        playerVelocityZ = playerSpeed * Input.GetAxisRaw("Vertical") * Time.deltaTime;

        //movementVector.x = Input.GetAxis("LeftJoystickX") * movementSpeed;
        //movementVector.z = Input.GetAxis("LeftJoystickY") * movementSpeed;


        PRB.velocity = new Vector3(playerVelocityX, 0, playerVelocityZ);
    }
}