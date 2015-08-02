using UnityEngine;
using System.Collections;

public class PlaneController : MonoBehaviour {
	public float speed = 1f;

	PlayerController Player;

	public void Start () {
		this.Player = this.GetComponent<PlayerController>();
		var ScreenWrap = this.GetComponent<ScreenWrap>();
		ScreenWrap.InstantiateGhosts();
		ScreenWrap.DestroyGhostComponent<PlaneController>();
		ScreenWrap.DestroyGhostComponent<Rigidbody2D>();
		Debug.Log(this.Player.BulletOffset);
	}
	
	public void FixedUpdate () {
		Rigidbody2D Rigidbody = this.GetComponent<Rigidbody2D>();
		Rigidbody.AddForce (Rigidbody.gravityScale * -1 * Physics2D.gravity);
	}

	public void Update () {
		this.transform.position += (Time.deltaTime * Vector3.right * this.speed);
	}
}
