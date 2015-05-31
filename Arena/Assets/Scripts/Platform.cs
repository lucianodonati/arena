using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float shrinkTimer = 15.0f;
    private float gameTimer = 0.0f;
    //bool shrinking = false;

    // Use this for initialization
    private void Start()
    {
        gameTimer = 0.0f;
    }

    // Update is called once per frame
    private void Update()
    {
        if (gameTimer < 0.0f)
        {
            if (transform.localScale.x > 10.0f)
            {
                gameTimer = shrinkTimer;
                Vector3 reductionAmt = new Vector3(10.0f, 10.0f, 0.0f);
                transform.localScale = transform.localScale - reductionAmt;
            }
        }
        else
            gameTimer -= Time.deltaTime;
    }
}