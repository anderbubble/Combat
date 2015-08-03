using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour
{
	public Toggle BouncingBullets;
	public Game game;

	public void LoadTanks ()
	{
		this.game.LoadTanks (BouncingBullets: this.BouncingBullets.isOn);
	}
	
	public void LoadPlanes ()
	{
		this.game.LoadPlanes ();
	}
}
