using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CreditsScript : MonoBehaviour {
	
	public float creditSpeed = 10; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		pos.y += creditSpeed * Time.deltaTime;
		transform.position = pos;
	
	}
	
}
