using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public TankController source;
	public float speed = 1;
	public float lifespan = 1;

	private float fired;

	void Start () {
		this.fired = Time.time;
		this.GetComponent<Rigidbody2D>().velocity = (this.transform.rotation * Vector2.up * this.speed);
	}
	
	void Update () {
		if (Time.time >= this.fired + this.lifespan) {
			this.Explode ();
		}
	}

	void Explode () {
		this.gameObject.SetActive (false);
		Destroy (this.gameObject);
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.tag == "Player") {
			var target = collision.collider.GetComponent<TankController>();
			if (target == this.source) {
				this.source.Score -= 1;
			} else {
				this.source.Score += 1;
			}
			target.Explode ();
			this.Explode ();
		} else if (collision.collider.tag == "Barrier") {
			if (this.GetComponent<Collider2D>().sharedMaterial == null) {
				this.Explode ();
			}
		} else {
			this.Explode ();
		}
	}
}
