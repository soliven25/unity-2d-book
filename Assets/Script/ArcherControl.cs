using UnityEngine;
using System.Collections;

public class ArcherControl : MonoBehaviour {
	
	// variable to register Archer animator
	private Animator mAnimator;

	//variable for Background controlling.
	private BackgroundControl	mBackgrounds;
	private BackgroundControl	mForegrounds;
	
	// the spot Arraow fire
	[HideInInspector]
	private Transform mAttackSpot;
	
	// Archer HP, Power, attack speed
	public int mOrinHp;
	[HideInInspector]
	public int mHp;
	
	public int mOrinAttack;
	[HideInInspector]
	public int mAttack;
	
	public float mAttackSpeed;
	
	//refer Arrow prefab
	public Object mArrowPrefab;
	
	//Archer status emum
	public enum Status
	{
		Idle,
		Run,
		Attack,
		Dead
	}
	
	[HideInInspector]
	public Status mStatus = Status.Idle;
	
	// Use this for initialization
	void Start () {
		mHp = mOrinHp;
		mAttack = mOrinAttack;
		
		//Archer's Animator componet
		mAnimator = gameObject.GetComponent<Animator>();
		
		// get all BackgroundControl type component in hierache view
		BackgroundControl[] component = GameObject.FindObjectsOfType<BackgroundControl>();
		
		mBackgrounds = component[0];
		mForegrounds = component[1];
		
		//get transform from child object 'spot'
		mAttackSpot = transform.FindChild("spot");
	}
	
	// Update is called once per frame
	void Update () {
		
		//get left & right arrow key or A & D key
		float speed = Mathf.Abs(Input.GetAxis("Horizontal"));
		SetStatus(Status.Run, speed);
		
		mBackgrounds.FlowControl(speed);
		mForegrounds.FlowControl(speed);
		
		if(Input.GetKeyDown(KeyCode.Space))
		{
			SetStatus(Status.Attack,0);
		}
		else if(Input.GetKeyDown(KeyCode.F))
		{
			SetStatus(Status.Dead, 0);
		}
		
	}
	
	public void SetStatus(Status status, float param)
	{
		switch(status)
		{
			case Status.Idle:
				mAnimator.SetFloat("Speed",0);
				mBackgrounds.FlowControl(0);
				mForegrounds.FlowControl(0);
			break;
			
			case Status.Run:
				mBackgrounds.FlowControl(1);
				mForegrounds.FlowControl(1);
				mAnimator.SetFloat("Speed", param);
			break;
			
			case Status.Attack:
				mAnimator.SetTrigger("Shoot");
			break;
			
			case Status.Dead:
				mAnimator.SetTrigger("Die");
			break;
		}
	}
	
	private void ShootArrow()
	{
		//instance arrow prefab
		GameObject arrow = Instantiate(mArrowPrefab, mAttackSpot.position, Quaternion.identity) as GameObject;
		
		//Call shoot function
		arrow.SendMessage("Shoot");
		
	}
}
