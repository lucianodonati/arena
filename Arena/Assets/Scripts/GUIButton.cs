using UnityEngine;
using System.Collections;

public class GUIButton : MonoBehaviour {

	Texture2D tex, tex2;
	Rect playRect, creditRect, exitRect;
	// Use this for initialization
	void Start () {

		playRect = new Rect (500.0f, 100.0f, 400, 150);
		
		creditRect = new Rect (500.0f, 700.0f, 400, 150);
		
		exitRect = new Rect (500.0f, 900.0f, 400, 50);

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
