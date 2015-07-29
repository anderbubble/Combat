using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public string TurnAxis = "Horizontal";
	public string MoveAxis = "Vertical";
	public string FireButton = "Fire";
	public GameObject bullet;
	public Text ScoreUI;

	private Rigidbody2D Rigidbody;

	private int _Score = 0;
	
	public int Score {
		get {
			return this._Score;
		}
		
		set {
			this._Score = value;
			this.ScoreUI.text = this._Score.ToString();
		}
	}

	void Start () {
		this.Rigidbody = this.GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		var moving = MovePlayer();
		if (moving) {
			DampenVelocity();
		}
		if (Input.GetButtonDown (this.FireButton)) {
			FireBullet();
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

	void FireBullet () {
		Instantiate(this.bullet, this.transform.position + (this.transform.rotation * Vector3.up * .5f), this.transform.rotation);
	}

	public void Explode () {
		this.gameObject.SetActive (false);
	}
}
