using System.Collections;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public float damage = 5.0f, ticTimer = 0.5f;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

	void OnCollisionEnter(Collision hit)
	{
		//if(hit.gameObject.tag == "Projectile")
		//{
		//	Destroy(hit.gameObject,0.0f);
		//}
	}
}