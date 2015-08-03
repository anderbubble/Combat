using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public BulletController BouncyBulletTemplate;

	public void Start () {
		DontDestroyOnLoad(this);
	}

	public void ConfigureTanks (bool BouncingBullets=false) {
		if (BouncingBullets) {
			StartCoroutine(this.ConfigureBouncingBullets());
		} else {
			Destroy(this.gameObject);
		}
	}

	private IEnumerator ConfigureBouncingBullets () {
		var players = FindObjectsOfType<PlayerController>();
		while (players.Length < 1) {
			yield return new WaitForEndOfFrame();
			players = FindObjectsOfType<PlayerController>();
		}
		foreach (var player in players) {
			player.BulletTemplate = this.BouncyBulletTemplate;
		}
		Destroy(this.gameObject);
	}
}
