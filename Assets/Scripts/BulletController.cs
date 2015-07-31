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
		if (collision.collider.tag == "Player")
		{
			collision.collider.GetComponent<TankController>().Explode(PointTo: this.source);
		}
		this.Explode ();
	}
}
