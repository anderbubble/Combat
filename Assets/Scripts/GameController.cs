using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public BulletController BouncyBulletTemplate;

	public void Start () {
		DontDestroyOnLoad(this);
	}

	public void ConfigureTanks (bool BouncingBullets=false) {
		if (BouncingBullets) {
			StartCoroutine(this.ConfigureBouncingBullets ());
		} else {
			Destroy (this.gameObject);
		}
	}

	private IEnumerator ConfigureBouncingBullets () {
		GameObject [] players = GameObject.FindGameObjectsWithTag("Player");
		while (players.Length < 1) {
			yield return new WaitForEndOfFrame();
			players = GameObject.FindGameObjectsWithTag("Player");
		}
		foreach (var Player in players) {
			Player.GetComponent<PlayerController>().BulletTemplate = this.BouncyBulletTemplate;
		}
		Destroy(this.gameObject);
	}
}
