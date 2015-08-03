using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TankMenu : MonoBehaviour {
	public Toggle BouncyBullets;
	public Game game;

	public void Load ()
	{
		game.LoadTanks(BouncingBullets: this.BouncyBullets.isOn);
	}
}
