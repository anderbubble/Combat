using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TankController : MonoBehaviour {
	public string TurnAxis = "Horizontal";
	public string MoveAxis = "Vertical";

	PlayerController player;

	void Start () {
		this.player = this.GetComponent<PlayerController>();
	}
	
	void Update () {
		if (this.player.alive) {
			var moving = MoveTank();
			if (moving) {
				this.player.DampenVelocity();
			}
		}
	}

	bool MoveTank () {
		var turn = Input.GetAxis(this.TurnAxis);
		var move = Input.GetAxis(this.MoveAxis);
		var rotation = Quaternion.AngleAxis(-45 * turn * Time.deltaTime, Vector3.forward);
		this.transform.rotation *= rotation;
		this.transform.position += this.transform.rotation * Vector3.up * move * Time.deltaTime;
		return move != 0;
	}
}
