using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour
{
	public GameObject tanks;
	public GameObject planes;

	public void TanksMenu ()
	{
		this.tanks.SetActive (true);
		this.gameObject.SetActive (false);
	}
	
	public void PlanesMenu ()
	{
		this.planes.SetActive (true);
		this.gameObject.SetActive (false);
	}
}
