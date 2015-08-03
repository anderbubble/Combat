using UnityEngine;
using System.Collections;

public class Tank : MonoBehaviour
{
	public string MoveAxis = "Vertical";
	public float speed;
	Player player;

	void Start ()
	{
		this.player = this.GetComponent<Player> ();
	}
	
	void Update ()
	{
		if (this.player.alive) {
			MoveTank ();
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
}
