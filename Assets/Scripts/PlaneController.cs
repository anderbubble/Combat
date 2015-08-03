using UnityEngine;
using System.Collections;

public class PlaneController : MonoBehaviour {
	public float speed = 1f;
	public string TurnAxis = "Horizontal";

	PlayerController player;
	new Collider2D collider;
	new Rigidbody2D rigidbody;

	public void Start () {
		this.player = this.GetComponent<PlayerController>();
		this.collider = this.GetComponent<Collider2D>();
		this.rigidbody = this.GetComponent<Rigidbody2D>();

		this.IgnorePlayerCollision();
	}
	
	public void FixedUpdate () {
		this.rigidbody.AddForce (this.rigidbody.gravityScale * -1 * Physics2D.gravity);
	}

	public void Update () {
		if (this.player.alive) {
			this.transform.position += this.transform.rotation * (Time.deltaTime * Vector3.right * this.speed);
			TurnPlane ();
		}
	}
	
	void IgnorePlayerCollision () {
		foreach (var plane in FindObjectsOfType<PlaneController>()) {
			var otherCollider = plane.collider;
			Physics2D.IgnoreCollision(this.collider, otherCollider);
		}
	}
	
	void TurnPlane () {
		var turn = Input.GetAxis (this.TurnAxis);
		var rotation = Quaternion.AngleAxis(-45 * turn * Time.deltaTime, Vector3.forward);
		this.transform.rotation *= rotation;
	}
}
