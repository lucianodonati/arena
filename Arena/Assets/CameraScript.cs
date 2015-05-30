using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    private GameObject player1;
    private GameObject player2;

    // Use this for initialization
    void Start()
    {
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");


    }

    // Update is called once per frame
    void Update()
    {

        Vector3 vBetween = player2.transform.position - player1.transform.position;
        vBetween.z = 0;
        Vector3 pos = Camera.main.transform.position;
        
        Vector3 offset = player1.transform.position;
        offset.z = 0.0f;

        offset += vBetween * 0.5f;
        pos.x = offset.x;
        pos.y = offset.y;
        Camera.main.transform.position = pos;

        float length = vBetween.magnitude;
        print(length);
        float t = length/7.0f;
        t = Mathf.Clamp(t,0.0f,1.0f);
        Camera.main.orthographicSize = Mathf.Lerp(1.33f,3.91f,t);
    }
}
