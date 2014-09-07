using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float maxSpeed = 10f;
	bool faceRight = true;
	public KeyCode key;

	Animator anim;

	bool Grounded = false;
	public Transform GroundCheck;
	float Groundradius = 0.05f;
	public LayerMask WhatIsGround;
	public float JumpForce = 500f;

	public Bullet Bullet;
	public float Firerate;
	bool Attacking = false;

	public int MaxHealth = 20;
	public GameObject OuchEffect;

	public int Health { get ; private set; }

	private float _canFireIn;
	public Transform ProjectileFireLocation;

	void Start()
	{
		anim = GetComponent<Animator> ();
		Health = MaxHealth;
	}

	void FixedUpdate()
	{
		Grounded = Physics2D.OverlapCircle (GroundCheck.position, Groundradius, WhatIsGround);
		anim.SetBool ("Ground", Grounded);
		float move = Input.GetAxis ("Horizontal2");

		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);

		anim.SetFloat ("SecondSpeed", Mathf.Abs (move));
		rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);
		if (move > 0 && !faceRight)
			Flip ();
		else if (move < 0 && faceRight)
			Flip ();
	}

	void Update()
	{
		anim.SetBool ("Attack", Attacking);
		_canFireIn -= Time.deltaTime;
		if (Grounded && Input.GetKeyDown (KeyCode.Z)) 
		{
			anim.SetBool ("Ground", false);
			rigidbody2D.AddForce (new Vector2 (0, JumpForce));
		}
		else if (Input.GetKeyDown (key)) 
		{
			FireProjectile ();
		}
	}

	private void FireProjectile()
	{
		if (_canFireIn > 0)
			return;

		anim.SetBool ("Attack", true);
		var direction = faceRight ? Vector2.right : -Vector2.right;
		
		var projectile = (Bullet) Instantiate(Bullet, ProjectileFireLocation.position, ProjectileFireLocation.rotation);
		projectile.Initialize (gameObject, direction, rigidbody2D.velocity);

		_canFireIn = Firerate;
	}


	void Flip ()
	{
		faceRight = !faceRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void TakeDamage(int damage)
	{
		var clone = Instantiate (OuchEffect, transform.position, transform.rotation);
		Health -= damage;
		Destroy (clone);
		if (Health <= 0)
			Application.LoadLevel ("Player1Death");
	}
}
