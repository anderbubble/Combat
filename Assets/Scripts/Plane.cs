using UnityEngine;
using System.Collections;

public class Plane : MonoBehaviour
{
	public float speed;
	public float AngularSpeed;
	public string TurnAxis = "Horizontal";
	Player player;
	new Collider2D collider;
	new Rigidbody2D rigidbody;

	public void Start ()
	{
		this.player = this.GetComponent<Player> ();
		this.collider = this.GetComponent<Collider2D> ();
		this.rigidbody = this.GetComponent<Rigidbody2D> ();

		this.IgnorePlayerCollision ();
	}
	
	public void FixedUpdate ()
	{
		this.rigidbody.AddForce (this.rigidbody.gravityScale * -1 * Physics2D.gravity);
	}

	public void Update ()
	{
		if (this.player.alive) {
			MovePlane ();
			TurnPlane ();
		}
	}
	
	void IgnorePlayerCollision ()
	{
		foreach (var plane in FindObjectsOfType<Plane>()) {
			var otherCollider = plane.collider;
			Physics2D.IgnoreCollision (this.collider, otherCollider);
		}
	}

	void MovePlane ()
	{
		this.transform.position += Time.deltaTime * (this.speed * (this.transform.rotation * Vector3.right));
	}
	
	void TurnPlane ()
	{
		var turn = -Input.GetAxis (this.TurnAxis);
		var rotation = Quaternion.AngleAxis (Time.deltaTime * this.AngularSpeed * turn, Vector3.forward);
		this.transform.rotation *= rotation;
	}
}
