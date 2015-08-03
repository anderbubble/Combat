using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	public Quaternion forward;
	public Bullet BulletTemplate;
	public int MaxBullets = 1;
	public float BulletSpeed = 9;
	public float AngularSpeed;
	public float ReloadTime = 0;
	public string TurnAxis = "Horizontal";
	public string FireButton = "Fire";
	public Text ScoreUI;
	public float BulletOffset = 0;
	bool loaded = true;
	List<Bullet> bullets;
	int _score = 0;
	SpriteRenderer renderer;
	BoxCollider2D collider;
	Rigidbody2D rigidbody;

	public bool alive {
		get {
			return
				this.renderer.enabled
				&& this.collider.enabled;
		}
		
		set {
			this.renderer.enabled = value;
			this.collider.enabled = value;
		}
	}
	
	public int score {
		get {
			return this._score;
		}
		
		set {
			this._score = value;
			if (this._score < 0) {
				this._score = 0;
			}
			this.ScoreUI.text = this._score.ToString ();
		}
	}

	Vector2 BulletOffsetVector {
		get {
			var edge = this.collider.size.x / 2;
			var radius = this.BulletTemplate.GetComponent<CircleCollider2D> ().radius;
			return this.forward * ((edge + radius + this.BulletOffset) * Vector2.up);
		}
	}

	void Awake ()
	{
		this.bullets = new List<Bullet> ();
	}

	void Start ()
	{
		this.renderer = this.GetComponent<SpriteRenderer> ();
		this.collider = this.GetComponent<BoxCollider2D> ();
		this.rigidbody = this.GetComponent<Rigidbody2D> ();
	}
	
	void Update ()
	{
		if (this.alive) {
			if (this.loaded && Input.GetButtonDown (this.FireButton) && this.CountActiveBullets () < this.MaxBullets) {
				FireBullet ();
			}
			this.Turn ();
		}
	}
	
	void Turn ()
	{
		var turn = -Input.GetAxis (this.TurnAxis);
		var rotation = Quaternion.AngleAxis (Time.deltaTime * turn * this.AngularSpeed, Vector3.forward);
		this.transform.rotation *= rotation;
	}

	void FireBullet ()
	{
		var Bullet =
			Instantiate (this.BulletTemplate, this.transform.position + (this.transform.rotation * this.BulletOffsetVector), this.transform.rotation * this.forward)
				as Bullet;
		Bullet.source = this;
		Bullet.speed += this.BulletSpeed;
		this.bullets.Add (Bullet);
		this.loaded = false;
		StartCoroutine (this.Reload (this.ReloadTime));
	}
	
	IEnumerator Reload (float seconds)
	{
		yield return new WaitForSeconds (seconds);
		this.loaded = true;
	}
	
	public void CleanupBullets ()
	{
		this.bullets.RemoveAll (x => x == null);
	}
	
	public int CountActiveBullets ()
	{
		this.CleanupBullets ();
		return this.bullets.Count;
	}

	public void DampenVelocity ()
	{
		this.rigidbody.velocity = Vector2.zero;
		this.rigidbody.angularVelocity = 0;
	}
}
