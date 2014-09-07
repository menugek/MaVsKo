using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float Speed;
	public LayerMask CollisionMask;

	public GameObject Owner { get; private set; }
	public Vector2 Direction { get; private set; }
	public Vector2 initialVelocity { get; private set; }

	public void Initialize(GameObject owner, Vector2 direction, Vector2 initialvelocity)
	{
		transform.right = direction;
		Owner = owner;
		Direction = direction;
		initialVelocity = initialvelocity;
		OnInitialized ();
	}

	protected virtual void OnInitialized()
	{
	}

	public virtual void OnTriggerEnter2D(Collider2D other)
	{
		if ((CollisionMask.value & (1 << other.gameObject.layer)) == 0) 
		{
			OnNotCollideWith (other);
			return;
		}
		var isOwner = other.gameObject == Owner;
		if (isOwner) 
		{
			Debug.Log ("Collider detected");
			OnCollideOwner ();
			return;
		}
		var takeDamage = (ItakeDamage)other.GetComponent (typeof(ItakeDamage));
		if (takeDamage != null) 
		{
			Debug.Log ("Collider detected");
			OnCollideTakeDamage (other, takeDamage);
			return;
		}
		OnCollideOther(other);
	}

	protected virtual void OnNotCollideWith(Collider2D other)
	{
	}

	protected virtual void OnCollideOwner()
	{
	}

	protected virtual void OnCollideTakeDamage(Collider2D other, ItakeDamage takeDamage)
	{
	}

	protected virtual void OnCollideOther(Collider2D other)
	{
	}

}
