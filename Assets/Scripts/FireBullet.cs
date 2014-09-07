using UnityEngine;
using System.Collections;

public class FireBullet : MonoBehaviour {
	float nextUsage;
	public float delay = 2f;
	bool Attacking = false;
	public KeyCode key;

	public float BulletSpeed = 500f;
	public GameObject bullet;

	Animator anim;
	
	void Start () 
	{
		nextUsage = Time.time + delay;
		anim = GetComponent<Animator> ();	
	}
	
	void Update () 
	{
		anim.SetBool("Attack", Attacking);
		if (Input.GetKeyDown (key) && Time.time > nextUsage)
		{
			nextUsage = Time.time + delay;
			anim.SetBool ("Attack", true);
		}
	}

	void Attack()
	{

	}
}
