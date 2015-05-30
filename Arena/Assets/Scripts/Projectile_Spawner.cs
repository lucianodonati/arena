using UnityEngine;
using System.Collections;

public class Projectile_Spawner : MonoBehaviour {

	public Projectile fireball;
	public AudioClip launchSound;
	float gameTime;
	public Vector2 projectileVelocity;
	bool canFire;
	Player owner;
	Rigidbody RB;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(canFire)
			{
				Projectile FB = (Projectile)Instantiate(fireball, transform.position + transform.forward * 2, transform.rotation);
				FB.RB.velocity = new Vector2(1.0f, 0.0f);
				FB.owner = owner;
			}

	
		}

	
	}
}
