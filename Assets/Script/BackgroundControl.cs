using UnityEngine;
using System.Collections;

public class BackgroundControl : MonoBehaviour {

	// variable for Background & Foreground Animator
	public Animator[] mBackgrounds;
	// Use this for initialization
	void Start () {
		FlowControl(0);
	}
	
	public void FlowControl(float speed)
	{
		foreach (Animator bg in mBackgrounds)
		{
			bg.speed = speed;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
