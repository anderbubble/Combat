using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour {

	public ParticleSystem Explosion;

	PlayerController player;

	void Start () {
		this.player = this.GetComponent<PlayerController>();
	}
	
	public void Explode () {
		this.player.alive = false;
		var explosion = Instantiate (this.Explosion, this.transform.position, Quaternion.identity) as ParticleSystem;
		StartCoroutine (this.CleanupExplosion(explosion));
		StartCoroutine(this.WaitRespawn ());
	}

	IEnumerator CleanupExplosion (ParticleSystem explosion) {
		while (explosion.IsAlive()) {
			yield return new WaitForEndOfFrame();
		}
		Destroy (explosion.gameObject);
	}

	IEnumerator WaitRespawn (float seconds=3) {
		yield return new WaitForSeconds(seconds);
		this.player.alive = true;
	}
	
	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.tag == "Bullet") {
			this.player.DampenVelocity();
			this.Explode ();
		}
	}
}
