using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {

	public Toggle BouncingBullets;
	public BulletController BouncyBulletTemplate;

	public void LoadTanks () {
		Application.LoadLevelAdditive("Tanks");
		if (this.BouncingBullets.isOn) {
			StartCoroutine (this.ConfigureBouncingBullets());
		} else {
			Destroy (this.gameObject);
		}
	}

	public IEnumerator ConfigureBouncingBullets () {
		GameObject [] players = GameObject.FindGameObjectsWithTag("Player");
		while (players.Length < 1) {
			yield return new WaitForEndOfFrame();
			players = GameObject.FindGameObjectsWithTag("Player");
		}
		foreach (var Player in players) {
			Player.GetComponent<TankController>().BulletTemplate = this.BouncyBulletTemplate;
		}
		Destroy(this.gameObject);
	}
}
