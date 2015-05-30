using System.Collections;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
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
      
        playerVelocityX = playerSpeed * Input.GetAxisRaw("C2LeftStickX") * Time.deltaTime;
        playerVelocityZ = playerSpeed * Input.GetAxisRaw("C2LeftStickY") * Time.deltaTime;




        PRB.velocity = new Vector3(playerVelocityX, 0, -playerVelocityZ);
    }
}