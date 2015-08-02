using UnityEngine;
using System.Collections;

public class PlaneController : MonoBehaviour {
	public float speed = 1f;

	public void Start () {
		var ScreenWrap = this.GetComponent<ScreenWrap>();
		ScreenWrap.InstantiateGhosts();
		ScreenWrap.DestroyComponent<PlaneController>();
		ScreenWrap.DestroyComponent<Rigidbody2D>();
		ScreenWrap.DestroyComponent<BoxCollider2D>();
	}

	public void FixedUpdate () {
		Rigidbody2D Rigidbody = this.GetComponent<Rigidbody2D>();
		Rigidbody.AddForce (Rigidbody.gravityScale * -1 * Physics2D.gravity);
	}

	public void Update () {
		this.transform.position += (Time.deltaTime * Vector3.right * this.speed);
	}
}
