using UnityEngine;
using System.Collections;

public class Melee : Bullet, ItakeDamage
{
	public int Damage;
	public GameObject DestroyedEffect;
	public int PointsToGiveToplayer;
	public float Timetolive;
	
	
	public void Update()
	{
		if ((Timetolive -= Time.deltaTime) <= 0)
		{
			
			DestroyProjectile ();
			return;
		}
		transform.Translate (Direction * ((Mathf.Abs (initialVelocity.x) + Speed) * Time.deltaTime), Space.World);
	}                        
	
	public virtual void OnTriggerEnter2D(Collider2D other)
	{
		if ((CollisionMask.value & (1 << other.gameObject.layer)) == 0) {
			Debug.Log ("Collider Non detected");
			OnNotCollideWith (other);
			return;
		}
		var player = other.GetComponent<PlayerControl> ();
		if (player != null) {
			player.TakeDamage (Damage);
			DestroyProjectile ();
		}
		var isOwner = other.gameObject == Owner;
		if (isOwner) {
			Debug.Log ("Collider detected");
			OnCollideOwner ();
			return;
		}
		var takeDamage = (ItakeDamage)other.GetComponent (typeof(ItakeDamage));
		if (takeDamage != null) {
			Debug.Log ("Collider detected");
			OnCollideTakeDamage (other, takeDamage);
			return;
		}
		OnCollideOther (other);
	}
	
	public void TakeDamage(int damage, GameObject instigator)
	{
		DestroyProjectile ();
		return;
	}
	
	protected override void OnCollideOther(Collider2D other)
	{
		DestroyProjectile();
		return;
	}
	
	protected override void OnCollideTakeDamage(Collider2D other, ItakeDamage takeDamage)
	{
		takeDamage.TakeDamage(Damage, gameObject);
		DestroyProjectile();
		return;
	}
	
	private void DestroyProjectile()
	{
		Destroy(gameObject);
	}
}
