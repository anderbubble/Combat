using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
	public Bullet BouncyBulletTemplate;

	public void Start ()
	{
		DontDestroyOnLoad (this);
	}

	public void ConfigureTanks (bool BouncingBullets=false)
	{
		if (BouncingBullets) {
			StartCoroutine (this.ConfigureBouncingBullets ());
		} else {
			Destroy (this.gameObject);
		}
	}

	private IEnumerator ConfigureBouncingBullets ()
	{
		var players = FindObjectsOfType<Player> ();
		while (players.Length < 1) {
			yield return new WaitForEndOfFrame ();
			players = FindObjectsOfType<Player> ();
		}
		foreach (var player in players) {
			player.BulletTemplate = this.BouncyBulletTemplate;
		}
		Destroy (this.gameObject);
	}
}
