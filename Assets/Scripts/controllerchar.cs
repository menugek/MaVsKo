using UnityEngine;
using System.Collections;

public class controllerchar : MonoBehaviour {
	public float maxSpeed = 10f;
	bool facingRight = false;

	Animator anim;

	bool Grounded = false;
	public Transform GroundCheck;
	float GroundRadius = 0.05f;
	public LayerMask WhatIsGround;
	public float jumpForce = 700f;
	
	bool doubleJump = false;

	public Melee melee;
	public float Firerate;
	bool Attacking = false;
	public KeyCode key;

	private float _canFireIn;
	public Transform ProjectileFireLocation;

	public int MaxHealth = 20;
	public GameObject OuchEffect;

	public int Health { get ; private set; }
	
	void Awake()
	{
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void Start ()
	{
		Health = MaxHealth;
		anim = GetComponent<Animator> ();
	}

	void FixedUpdate () 
	{
		Grounded = Physics2D.OverlapCircle (GroundCheck.position, GroundRadius, WhatIsGround);
		anim.SetBool ("Ground", Grounded);
		float move = Input.GetAxis ("Horizontal");

		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);


		if (Grounded)
			doubleJump = false;

		anim.SetFloat ("Speed", Mathf.Abs (move));
		rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);
		if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();
	}

	void Update()
	{
		anim.SetBool ("Attack", Attacking);
		_canFireIn -= Time.deltaTime;
		if ((Grounded || !doubleJump) && Input.GetKeyDown (KeyCode.UpArrow)) {
						anim.SetBool ("Ground", false);
						rigidbody2D.AddForce (new Vector2 (0, jumpForce));
						if (!doubleJump && !Grounded)
								doubleJump = true;
				} else if (Input.GetKeyDown (key)) {
						Attack ();
				}
	}

	private void Attack()
	{
		if (_canFireIn > 0)
			return;

		var direction = facingRight ? Vector2.right : -Vector2.right;

		var projectile = (Melee)Instantiate (melee, ProjectileFireLocation.position, ProjectileFireLocation.rotation);
		projectile.Initialize (gameObject, direction, rigidbody2D.velocity);
		anim.SetBool ("Attack", true);
		_canFireIn = Firerate;
	}

	void Flip ()
	{
		facingRight = !facingRight;	
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
			Application.LoadLevel ("Player2Death");
	}
}
