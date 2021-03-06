﻿using UnityEngine;
using System.Collections;

public class Plane : MonoBehaviour
{
	public float speed;
	Player player;
	Collider2D collider;
	Rigidbody2D rigidbody;

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
}
