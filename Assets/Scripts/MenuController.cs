using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {

	public Toggle BouncingBullets;
	public GameController game;

	public void LoadTanks () {
		this.game.ConfigureTanks(BouncingBullets: this.BouncingBullets.isOn);
		Application.LoadLevel("Tanks");
	}
	
	public void LoadPlanes () {
		Destroy (this.game);
		Application.LoadLevel("Planes");
	}
}
