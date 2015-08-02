using UnityEngine;
using System.Collections;

public class PlaneController : MonoBehaviour {
	public float speed = 1f;
	public string TurnAxis = "Horizontal";

	PlayerController player;
	Collider2D collider;

	public void Start () {
		this.player = this.GetComponent<PlayerController>();
		this.collider = this.GetComponent<Collider2D>();
		this.IgnorePlayerCollision();
	}

	void IgnorePlayerCollision () {
		foreach (var plane in FindObjectsOfType<PlaneController>()) {
			var collider = plane.GetComponent<Collider2D>();
			Physics2D.IgnoreCollision(this.collider, collider);
		}
	}
	
	public void FixedUpdate () {
		Rigidbody2D Rigidbody = this.GetComponent<Rigidbody2D>();
		Rigidbody.AddForce (Rigidbody.gravityScale * -1 * Physics2D.gravity);
	}

	public void Update () {
		if (this.player.alive) {
			this.transform.position += this.transform.rotation * (Time.deltaTime * Vector3.right * this.speed);
			TurnPlane ();
		}
	}
	
	void TurnPlane () {
		var turn = Input.GetAxis (this.TurnAxis);
		var rotation = Quaternion.AngleAxis(-45 * turn * Time.deltaTime, Vector3.forward);
		this.transform.rotation *= rotation;
	}
}
