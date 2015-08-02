using UnityEngine;
using System.Collections;

public class PlaneController : MonoBehaviour {
	public float speed = 1f;
	public string TurnAxis = "Horizontal";

	PlayerController Player;

	public void Start () {
		this.Player = this.GetComponent<PlayerController>();
		var ScreenWrap = this.GetComponent<ScreenWrap>();
		ScreenWrap.InstantiateGhosts();
		ScreenWrap.DestroyGhostComponent<PlaneController>();
		ScreenWrap.DestroyGhostComponent<Rigidbody2D>();
	}
	
	public void FixedUpdate () {
		Rigidbody2D Rigidbody = this.GetComponent<Rigidbody2D>();
		Rigidbody.AddForce (Rigidbody.gravityScale * -1 * Physics2D.gravity);
	}

	public void Update () {
		if (this.Player.alive) {
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
