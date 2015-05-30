using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float playerSpeed = 15.0f;
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

        //Vector3 move = new Vector3(playerVelocity, 0, 0);
        //PRB.MovePosition(move);

        print(Input.GetAxisRaw("Horizontal"));
        //float h = Input.GetAxisRaw("Horizontal");
       // float xPos = h * playerSpeed;
        
        //transform.position = new Vector3(xPos, 0, 2.0f);

      //  textOutput.text = "Value Returned: "+h.ToString("F2");  
      //  GetComponent<Rigidbody>().velocity = ;
    }
}
