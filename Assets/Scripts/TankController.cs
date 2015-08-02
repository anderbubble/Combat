using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TankController : MonoBehaviour {

	public string TurnAxis = "Horizontal";
	public string MoveAxis = "Vertical";
	private PlayerController Player;

	void Start () {
		this.Player = this.GetComponent<PlayerController>();
		this.Player.BulletOffset = ((this.GetComponent<BoxCollider2D>().size.y / 2) + this.Player.BulletTemplate.GetComponent<CircleCollider2D>().radius + .05f) * Vector2.up;
	}
	
	void Update () {
		if (this.GetComponent<PlayerController>().alive) {
			var moving = MoveTank();
			if (moving) {
				this.GetComponent<PlayerController>().DampenVelocity();
			}
		}
	}

	bool MoveTank () {
		var turn = Input.GetAxis (this.TurnAxis);
		var move = Input.GetAxis (this.MoveAxis);
		var rotation = Quaternion.AngleAxis(-45 * turn * Time.deltaTime, Vector3.forward);
		this.transform.rotation *= rotation;
		this.transform.position += this.transform.rotation * Vector3.up * move * Time.deltaTime;
		return (move != 0);
	}
}
