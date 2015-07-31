using UnityEngine;
using System.Collections;

public class PrimaryUIController : MonoBehaviour {
	void Start () {
		Application.LoadLevelAdditive ("Tanks");
		Application.LoadLevelAdditive ("TankWalls");
		Application.LoadLevelAdditive ("TankBarriers");
	}
}
