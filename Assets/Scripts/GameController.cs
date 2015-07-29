using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public PlayerController Player1;
	public PlayerController Player2;

	private HashSet<PlayerController> RespawnPlayers;

	void Start () {
		this.RespawnPlayers = new HashSet<PlayerController>();
	}

	void Update () {
		if (!this.RespawnPlayers.Contains(this.Player1) && !this.Player1.gameObject.activeInHierarchy) {
			this.Player2.Score += 1;
			this.RespawnPlayers.Add (this.Player1);
			StartCoroutine (this.Respawn(this.Player1));
		}
		if (!this.RespawnPlayers.Contains (this.Player2) && !this.Player2.gameObject.activeInHierarchy) {
			this.Player1.Score += 1;
			this.RespawnPlayers.Add (this.Player2);
			StartCoroutine (this.Respawn(this.Player2));
		}
	}

	IEnumerator Respawn (PlayerController Player) {
		yield return new WaitForSeconds(3);
		Player.gameObject.SetActive(true);
		this.RespawnPlayers.Remove (Player);
	}
}
