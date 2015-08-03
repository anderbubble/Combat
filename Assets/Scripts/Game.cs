using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
	public Bullet BouncyBulletTemplate;
	Player [] players;

	public void Start ()
	{
		DontDestroyOnLoad (this);
	}

	IEnumerator DestroyAfter (params Coroutine[] coroutines)
	{
		foreach (var coroutine in coroutines) {
			yield return coroutine;
		}
		Destroy (this);
	}

	public void LoadTanks (bool BouncingBullets=false)
	{
		StartCoroutine (this.DestroyAfter (
			StartCoroutine (this.ConfigureTanks (BouncingBullets: BouncingBullets))));
		Application.LoadLevel ("Tanks");
	}

	IEnumerator ConfigureTanks (bool BouncingBullets=false)
	{
		if (BouncingBullets)
		{
			yield return StartCoroutine(this.WaitForPlayers());
			foreach (var player in this.players) {
				player.BulletTemplate = this.BouncyBulletTemplate;
			}
		}
	}

	public void LoadPlanes ()
	{
		Application.LoadLevel ("Planes");
		Destroy (this);
	}

	IEnumerator WaitForPlayers ()
	{
		this.players = FindObjectsOfType<Player> ();
		while (this.players.Length < 1) {
			yield return new WaitForEndOfFrame ();
			this.players = FindObjectsOfType<Player> ();
		}
	}
}
