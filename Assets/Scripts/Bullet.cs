using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	public Player source;
	public float speed = 1;
	public float lifespan = 1;
	float fired;
	new Collider2D collider;
	new Rigidbody2D rigidbody;

	void Awake ()
	{
		this.fired = Time.time;
	}

	void Start ()
	{
		this.collider = this.GetComponent<Collider2D> ();
		this.rigidbody = this.GetComponent<Rigidbody2D> ();
		this.rigidbody.velocity = this.transform.rotation * Vector2.up * this.speed;
	}
	
	void Update ()
	{
		if (Time.time >= this.fired + this.lifespan) {
			this.Explode ();
		}
	}

	void Explode ()
	{
		this.gameObject.SetActive (false);
		Destroy (this.gameObject);
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.collider.tag == "Player") {
			var target = collision.collider.GetComponent<Player> ();
			if (target == this.source) {
				this.source.score -= 1;
			} else {
				this.source.score += 1;
			}
			this.Explode ();
		} else if (collision.collider.tag == "Barrier") {
			if (this.collider.sharedMaterial == null) {
				this.Explode ();
			}
		} else {
			this.Explode ();
		}
	}
}
