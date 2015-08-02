using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ScreenWrap : MonoBehaviour {

	Camera MainCamera;
	
	Vector3 screenBottomLeft {
		get {
			return this.MainCamera.ViewportToWorldPoint(new Vector3(0, 0, this.transform.position.z));
		}
	}

	Vector3 screenTopRight {
		get {
			return this.MainCamera.ViewportToWorldPoint(new Vector3(1, 1, this.transform.position.z));
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
	
	Dictionary<string, Camera> Ghosts = new Dictionary<string, Camera>();

	void Start () {
		this.MainCamera = this.GetComponent<Camera>();
		this.InstantiateGhosts ();
		this.DestroyGhostComponent<ScreenWrap>();
		this.DestroyGhostComponent<AudioListener>();
		this.SetGhostClearFlags(CameraClearFlags.Nothing);
		this.PositionGhosts ();
		this.RotateGhosts();
	}

	void Update () {
		var renderers = FindObjectsOfType<Renderer>();
		foreach (var renderer in renderers) {
			if (!VisibleFrom(renderer, this.MainCamera)) {
				this.MoveObject(renderer);
			}
		}
	}

	public void InstantiateGhosts() {
		this.InstantiateGhost("n").name = "North Camera";
		this.InstantiateGhost("s").name = "South Camera";
		this.InstantiateGhost("e").name = "East Camera";
		this.InstantiateGhost("w").name = "West Camera";
		this.InstantiateGhost("ne").name = "Northeast Camera";
		this.InstantiateGhost("se").name = "Southeast Camera";
		this.InstantiateGhost("nw").name = "Northwest Camera";
		this.InstantiateGhost("sw").name = "Southwest Camera";
	}

	Camera InstantiateGhost (string position) {
		this.Ghosts[position] = Instantiate(
			this.MainCamera, this.transform.position, this.transform.rotation) as Camera;
		return this.Ghosts[position];
	}

	void PositionGhosts () {
		this.Ghosts["n"].transform.position = this.transform.position
			+ new Vector3(0, this.screenHeight, 0);
		this.Ghosts["s"].transform.position = this.transform.position
			+ new Vector3(0, -this.screenHeight, 0);
		this.Ghosts["e"].transform.position = this.transform.position
			+ new Vector3(this.screenWidth, 0, 0);
		this.Ghosts["w"].transform.position = this.transform.position
			+ new Vector3(-this.screenWidth, 0, 0);
		this.Ghosts["ne"].transform.position = this.transform.position
			+ new Vector3(this.screenWidth, this.screenHeight, 0);
		this.Ghosts["se"].transform.position = this.transform.position
			+ new Vector3(-this.screenWidth, this.screenHeight, 0);
		this.Ghosts["nw"].transform.position = this.transform.position
			+ new Vector3(this.screenWidth, -this.screenHeight, 0);
		this.Ghosts["sw"].transform.position = this.transform.position
			+ new Vector3(this.screenWidth, -this.screenHeight, 0);
	}

	void RotateGhosts () {
		this.Ghosts["n"].transform.rotation = this.transform.rotation;
		this.Ghosts["s"].transform.rotation = this.transform.rotation;
		this.Ghosts["e"].transform.rotation = this.transform.rotation;
		this.Ghosts["w"].transform.rotation = this.transform.rotation;
		this.Ghosts["ne"].transform.rotation = this.transform.rotation;
		this.Ghosts["se"].transform.rotation = this.transform.rotation;
		this.Ghosts["nw"].transform.rotation = this.transform.rotation;
		this.Ghosts["sw"].transform.rotation = this.transform.rotation;
	}

	void DestroyGhostComponent<T> () where T : Component {
		foreach(var entry in this.Ghosts) {
			Destroy(this.Ghosts[entry.Key].GetComponent<T>());
		}
	}

	void SetGhostClearFlags (CameraClearFlags clearFlags) {
		foreach(var entry in this.Ghosts) {
			entry.Value.GetComponent<Camera>().clearFlags = clearFlags;
		}
	}

	void MoveObject (Renderer renderer) {
		foreach(var entry in this.Ghosts) {
			if (VisibleFrom(renderer, entry.Value)) {
				renderer.transform.position =
					this.MainCamera.transform.position
						+ (renderer.transform.position - entry.Value.transform.position);
				break;
			}
		}
	}

	static bool VisibleFrom (Renderer renderer, Camera camera) {
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
		return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
	}
}
