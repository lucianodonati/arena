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
				Projectile fireball = (GameObject)Instantiate(Projectile, transform.position + transform.forward * 2, transform.rotation);
				fireball.RB.velocity = Vector2(1.0f, 0.0f);
				fireball.owner = owner;
			}

	
		}

	
	}
}
