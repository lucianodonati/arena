using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float speed = 10;
	Vector3 vel = new Vector3(1,1,1);
	Vector3 pos = new Vector3(0,0,0);
	public AudioClip sound;
	public Player owner;
	Projectile_Spawner launcher;
	public Rigidbody RB;


	// Use this for initialization
	void Start () {

	
	
	}
	
	// Update is called once per frame
	void Update () {

		if( RB.velocity.magnitude <= 15 )
		{
			RB.AddForce( RB.velocity, ForceMode.VelocityChange );	
		}

	
	}
}
