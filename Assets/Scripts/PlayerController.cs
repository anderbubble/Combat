using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public string TurnAxis = "Horizontal";
	public string MoveAxis = "Vertical";
	public string FireButton = "Fire";
	public BulletController bullet;
	public Text ScoreUI;
	private bool loaded = true;
	public float ReloadTime = 0.5f;

	private Rigidbody2D Rigidbody;

	private int _Score = 0;

	public bool alive {
		get {
			return (
				this.GetComponent<SpriteRenderer>().enabled
				&& this.GetComponent<BoxCollider2D>().enabled);
		}

		set {
			this.GetComponent<SpriteRenderer>().enabled = value;
			this.GetComponent<BoxCollider2D>().enabled = value;
		}
	}

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
		if (this.alive) {
			var moving = MovePlayer();
			if (moving) {
				DampenVelocity();
			}
			if (this.loaded && Input.GetButtonDown (this.FireButton)) {
				FireBullet();
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

	void FireBullet () {
		var Bullet = 
			Instantiate(this.bullet, this.transform.position + (this.transform.rotation * Vector3.up * .5f), this.transform.rotation)
				as BulletController;
		Bullet.source = this;
		this.loaded = false;
		StartCoroutine (this.Reload(this.ReloadTime));
	}

	public void Explode (PlayerController PointTo=null) {
		this.DampenVelocity();
		if (PointTo != null)
		{
			PointTo.Score += 1;
		}
		this.alive = false;
		StartCoroutine(this.WaitRespawn ());
	}

	IEnumerator WaitRespawn (float seconds=3) {
		yield return new WaitForSeconds(seconds);
		this.alive = true;
	}

	IEnumerator Reload (float seconds) {
		yield return new WaitForSeconds(seconds);
		this.loaded = true;
	}
}
