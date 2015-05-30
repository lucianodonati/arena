using UnityEngine;
using System.Collections;

public class Projectile_Spawner : MonoBehaviour {

	public Projectile fireball;
	public AudioClip launchSound;
	float gameTime;
	public Vector2 projectileVelocity;
	bool canFire;
	Player owner;
	public Rigidbody RB;

	// Use this for initialization
	void Start () {
		canFire = true;
		RB.position = transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(canFire)
			{
				Projectile FB = (Projectile)Instantiate(fireball, transform.position + transform.forward * 2, transform.rotation);
				FB.RB.position = this.transform.position;
				FB.RB.velocity = new Vector3(1.0f, 1.0f, 1.0f);
				FB.owner = owner;
			}

	
		}

	
	}
}
