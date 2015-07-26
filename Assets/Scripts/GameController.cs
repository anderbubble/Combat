using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public GameObject Player1;
	public GameObject Player2;
	
	private HashSet<GameObject> RespawnPlayers;

	void Start () {
		this.RespawnPlayers = new HashSet<GameObject>();
	}

	void Update () {
		if (!this.RespawnPlayers.Contains(this.Player1) && !this.Player1.activeInHierarchy) {
			this.RespawnPlayers.Add (this.Player1);
			StartCoroutine (this.Respawn(this.Player1));
		}
		if (!this.RespawnPlayers.Contains (this.Player2) && !this.Player2.activeInHierarchy) {
			this.RespawnPlayers.Add (this.Player2);
			StartCoroutine (this.Respawn(this.Player2));
		}
	}

	IEnumerator Respawn (GameObject Player) {
		yield return new WaitForSeconds(3);
		Player.SetActive(true);
		this.RespawnPlayers.Remove (Player);
	}
}
