using UnityEngine;
using System.Collections;

public class AnimationEvents : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void TransitionToSuspense()
    {
        Debug.Log("TransitionToSuspense");
        Showdown.GetInstance().BeginShowdown();
    }
}
