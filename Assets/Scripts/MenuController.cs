using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {

	public Toggle BouncingBullets;
	public GameController GameController;

	public void LoadTanks () {
		this.GameController.ConfigureTanks(BouncingBullets: this.BouncingBullets.isOn);
		Application.LoadLevel("Tanks");
	}
}
