using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

	public float shrinkTimer = 15.0f;
	float gameTimer;
	Rigidbody2D PRB;
	//bool shrinking = false;

	// Use this for initialization
	void Start () {
		gameTimer = 0.0f;
	
	}
	
	// Update is called once per frame
	void Update () {

		gameTimer += Time.deltaTime;

		if (gameTimer >= shrinkTimer) {
			gameTimer = 0.0f;
			Vector3 reductionAmt = new Vector3(10.0f, 10.0f, 0.0f);
			transform.localScale = transform.localScale - reductionAmt;
		}
	
	}
}
