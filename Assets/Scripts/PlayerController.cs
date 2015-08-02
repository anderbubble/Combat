using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	public Quaternion Forward;
	public BulletController BulletTemplate;
	public int MaxBullets = 1;
	public float BulletSpeed = 9;
	public float ReloadTime = 0;
	public string FireButton = "Fire";
	public Text ScoreUI;
	public float BulletOffset = 0;
	
	Vector2 BulletOffsetVector;
	bool loaded = true;
	List<BulletController> Bullets;

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

	void Awake () {
		this.Bullets = new List<BulletController>();
	}

	void Start () {
		var edge = this.GetComponent<BoxCollider2D>().size.x / 2;
		var radius = this.BulletTemplate.GetComponent<CircleCollider2D>().radius;
		this.BulletOffsetVector = this.Forward * ((edge + radius + this.BulletOffset) * Vector2.up);
	}
	
	void Update () {
		if (this.alive) {
			if (this.loaded && Input.GetButtonDown (this.FireButton) && this.CountActiveBullets() < this.MaxBullets) {
				FireBullet();
			}
		}
	}

	void FireBullet () {
		var Bullet =
			Instantiate(this.BulletTemplate, this.transform.position + (this.transform.rotation * this.BulletOffsetVector), this.transform.rotation * this.Forward)
				as BulletController;
		Bullet.source = this;
		Bullet.speed += this.BulletSpeed;
		this.Bullets.Add (Bullet);
		this.loaded = false;
		StartCoroutine (this.Reload(this.ReloadTime));
	}
	
	IEnumerator Reload (float seconds) {
		yield return new WaitForSeconds(seconds);
		this.loaded = true;
	}
	
	public void CleanupBullets ()
	{
		this.Bullets.RemoveAll (x => (((BulletController)x).Equals (null)));
	}
	
	public int CountActiveBullets ()
	{
		this.CleanupBullets();
		return this.Bullets.Count;
	}
	
	private int _Score = 0;
	
	public int Score {
		get {
			return this._Score;
		}
		
		set {
			this._Score = value;
			if (this._Score < 0) {
				this._Score = 0;
			}
			this.ScoreUI.text = this._Score.ToString();
		}
	}

	public void DampenVelocity () {
		var rigidbody = this.GetComponent<Rigidbody2D>();
		if (rigidbody != null) {
			rigidbody.velocity = Vector2.zero;
			rigidbody.angularVelocity = 0;
		}
	}
}
