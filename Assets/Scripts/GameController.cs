using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public GameObject Player1;
	public GameObject Player2;

	public Text Player1ScoreUI;
	public Text Player2ScoreUI;

	private int _Player1Score = 0;
	private int _Player2Score = 0;

	private int Player1Score {
		get {
			return this._Player1Score;
		}

		set {
			this._Player1Score = value;
			this.Player1ScoreUI.text = this._Player1Score.ToString();
		}
	}
	
	private int Player2Score {
		get {
			return this._Player2Score;
		}
		
		set {
			this._Player2Score = value;
			this.Player2ScoreUI.text = this._Player2Score.ToString();
		}
	}

	private HashSet<GameObject> RespawnPlayers;

	void Start () {
		this.RespawnPlayers = new HashSet<GameObject>();
	}

	void Update () {
		if (!this.RespawnPlayers.Contains(this.Player1) && !this.Player1.activeInHierarchy) {
			this.Player2Score += 1;
			this.RespawnPlayers.Add (this.Player1);
			StartCoroutine (this.Respawn(this.Player1));
		}
		if (!this.RespawnPlayers.Contains (this.Player2) && !this.Player2.activeInHierarchy) {
			this.Player1Score += 1;
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
