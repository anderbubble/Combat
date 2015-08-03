using UnityEngine;
using System.Collections;

public class PlaneMenu : MonoBehaviour {
	public Game game;
	
	public void Load ()
	{
		game.LoadPlanes();
	}
}
