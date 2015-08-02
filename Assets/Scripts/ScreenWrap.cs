using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// http://gamedevelopment.tutsplus.com/articles/create-an-asteroids-like-screen-wrapping-effect-with-unity--gamedev-15055

public class ScreenWrap : MonoBehaviour {

	public Camera Camera;
	
	Vector3 screenBottomLeft {
		get {
			return this.Camera.ViewportToWorldPoint(new Vector3(0, 0, this.transform.position.z));
		}
	}

	Vector3 screenTopRight {
		get {
			return this.Camera.ViewportToWorldPoint(new Vector3(1, 1, this.transform.position.z));
		}
	}
	
	float screenWidth {
		get {
			return this.screenTopRight.x - this.screenBottomLeft.x;
		}
	}

	float screenHeight {
		get {
			return this.screenTopRight.y - this.screenBottomLeft.y;
		}
	}
	
	Dictionary<string, Transform> Ghosts = new Dictionary<string, Transform>();

	void Update () {
		this.SwapGhosts ();
		this.PositionGhosts ();
		this.RotateGhosts ();
	}

	public void InstantiateGhosts() {
		this.InstantiateGhost("n");
		this.InstantiateGhost("s");
		this.InstantiateGhost("e");
		this.InstantiateGhost("w");
		this.InstantiateGhost("ne");
		this.InstantiateGhost("se");
		this.InstantiateGhost("nw");
		this.InstantiateGhost("sw");
		this.DestroyComponent<ScreenWrap>();
		this.PositionGhosts ();
		this.RotateGhosts ();
	}
	
	public void DestroyComponent<T> () where T : Component {
		foreach(var entry in this.Ghosts) {
			Destroy(this.Ghosts[entry.Key].GetComponent<T>());
		}
	}

	Transform InstantiateGhost (string position) {
		this.Ghosts[position] = Instantiate(
			this.transform, this.transform.position, this.transform.rotation) as Transform;
		return this.Ghosts[position];
	}

	void PositionGhosts () {
		this.Ghosts["n"].position = this.transform.position
			+ new Vector3(0, this.screenHeight, 0);
		this.Ghosts["s"].position = this.transform.position
			+ new Vector3(0, -this.screenHeight, 0);
		this.Ghosts["e"].position = this.transform.position
			+ new Vector3(this.screenWidth, 0, 0);
		this.Ghosts["w"].position = this.transform.position
			+ new Vector3(-this.screenWidth, 0, 0);
		this.Ghosts["ne"].position = this.transform.position
			+ new Vector3(this.screenWidth, this.screenHeight, 0);
		this.Ghosts["se"].position = this.transform.position
			+ new Vector3(-this.screenWidth, this.screenHeight, 0);
		this.Ghosts["nw"].position = this.transform.position
			+ new Vector3(this.screenWidth, -this.screenHeight, 0);
		this.Ghosts["sw"].position = this.transform.position
			+ new Vector3(this.screenWidth, -this.screenHeight, 0);
	}

	void RotateGhosts () {
		this.Ghosts["n"].rotation = this.transform.rotation;
		this.Ghosts["s"].rotation = this.transform.rotation;
		this.Ghosts["e"].rotation = this.transform.rotation;
		this.Ghosts["w"].rotation = this.transform.rotation;
		this.Ghosts["ne"].rotation = this.transform.rotation;
		this.Ghosts["se"].rotation = this.transform.rotation;
		this.Ghosts["nw"].rotation = this.transform.rotation;
		this.Ghosts["sw"].rotation = this.transform.rotation;
	}

	void SwapGhosts () {
		foreach(var entry in this.Ghosts) {
			if (this.OnScreen (entry.Value)) {
				transform.position = entry.Value.position;
				break;
			}
		}
	}

	bool OnScreen (Transform Ghost) {
		return (this.screenBottomLeft.x < Ghost.position.x)
			&& (Ghost.position.x < this.screenTopRight.x)
			&& (this.screenBottomLeft.y < Ghost.position.y)
			&& (Ghost.position.y < this.screenTopRight.y);
	}
}
