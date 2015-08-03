using UnityEngine;
using System.Collections;

public class Tank : MonoBehaviour
{
	public string TurnAxis = "Horizontal";
	public string MoveAxis = "Vertical";
	public float speed;
	public float AngularSpeed;
	Player player;

	void Start ()
	{
		this.player = this.GetComponent<Player> ();
	}
	
	void Update ()
	{
		if (this.player.alive) {
			MoveTank ();
			TurnTank ();
		}
	}

	void MoveTank ()
	{
		var move = Input.GetAxis (this.MoveAxis);
		this.transform.position += Time.deltaTime * move * (this.speed * (this.transform.rotation * Vector3.up));

		if (move != 0) {
			this.player.DampenVelocity ();
		}
	}

	void TurnTank ()
	{
		var turn = -Input.GetAxis (this.TurnAxis);
		var rotation = Quaternion.AngleAxis (Time.deltaTime * turn * this.AngularSpeed, Vector3.forward);
		this.transform.rotation *= rotation;
	}
}
