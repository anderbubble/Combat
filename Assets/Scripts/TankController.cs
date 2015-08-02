using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TankController : MonoBehaviour {

	public string TurnAxis = "Horizontal";
	public string MoveAxis = "Vertical";
	public GameObject Explosion;

	private Rigidbody2D Rigidbody;
	private PlayerController Player;

	void Start () {
		this.Rigidbody = this.GetComponent<Rigidbody2D>();
		this.Player = this.GetComponent<PlayerController>();
		this.Player.BulletOffset = ((this.GetComponent<BoxCollider2D>().size.y / 2) + this.Player.BulletTemplate.GetComponent<CircleCollider2D>().radius + .05f) * Vector2.up;
	}
	
	void Update () {
		if (this.GetComponent<PlayerController>().alive) {
			var moving = MovePlayer();
			if (moving) {
				DampenVelocity();
			}
		}
	}

	bool MovePlayer () {
		var turn = Input.GetAxis (this.TurnAxis);
		var move = Input.GetAxis (this.MoveAxis);
		var rotation = Quaternion.AngleAxis(-45 * turn * Time.deltaTime, Vector3.forward);
		this.transform.rotation *= rotation;
		this.transform.position += this.transform.rotation * Vector3.up * move * Time.deltaTime;
		return (move != 0);
	}

	void DampenVelocity () {
		this.Rigidbody.velocity = Vector2.zero;
		this.Rigidbody.angularVelocity = 0;
	}
	
	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.tag == "Bullet") {
			this.Explode ();
		}
	}

	public void Explode () {
		this.DampenVelocity();
		this.GetComponent<PlayerController>().alive = false;
		Instantiate (this.Explosion, this.transform.position, Quaternion.identity);
		StartCoroutine(this.WaitRespawn ());
	}

	IEnumerator WaitRespawn (float seconds=3) {
		yield return new WaitForSeconds(seconds);
		this.GetComponent<PlayerController>().alive = true;
	}
}
