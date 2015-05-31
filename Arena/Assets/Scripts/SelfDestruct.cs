using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {
	//ParticleSystem PS;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Destroy (this.gameObject, 1.51f);
	}
}
