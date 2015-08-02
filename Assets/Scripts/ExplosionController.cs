using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour {

	public ParticleSystem Explosion;
	
	public void Explode () {
		this.GetComponent<PlayerController>().alive = false;
		Instantiate (this.Explosion, this.transform.position, Quaternion.identity);
		StartCoroutine(this.WaitRespawn ());
	}

	IEnumerator WaitRespawn (float seconds=3) {
		yield return new WaitForSeconds(seconds);
		this.GetComponent<PlayerController>().alive = true;
	}
	
	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.tag == "Bullet") {
			this.Explode ();
		}
	}
}
