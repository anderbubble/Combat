using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour
{
	public Toggle BouncingBullets;
	public Game game;

	public void LoadTanks ()
	{
		this.game.ConfigureTanks (BouncingBullets: this.BouncingBullets.isOn);
		Application.LoadLevel ("Tanks");
	}
	
	public void LoadPlanes ()
	{
		Destroy (this.game);
		Application.LoadLevel ("Planes");
	}
}
