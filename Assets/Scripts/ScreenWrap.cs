using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ScreenWrap : MonoBehaviour
{

	Camera MainCamera;
	Dictionary<string, Camera> ghosts;
	
	Vector3 BottomLeft {
		get {
			return this.MainCamera.ViewportToWorldPoint (new Vector3 (0, 0, this.transform.position.z));
		}
	}

	Vector3 TopRight {
		get {
			return this.MainCamera.ViewportToWorldPoint (new Vector3 (1, 1, this.transform.position.z));
		}
	}
	
	float width {
		get {
			return this.TopRight.x - this.BottomLeft.x;
		}
	}

	float height {
		get {
			return this.TopRight.y - this.BottomLeft.y;
		}
	}

	void Awake ()
	{
		this.ghosts = new Dictionary<string, Camera> ();
	}

	void Start ()
	{
		this.MainCamera = this.GetComponent<Camera> ();
		this.InstantiateGhosts ();
		this.DestroyGhostComponent<ScreenWrap> ();
		this.DestroyGhostComponent<AudioListener> ();
		this.SetGhostClearFlags (CameraClearFlags.Nothing);
		this.PositionGhosts ();
		this.RotateGhosts ();
	}

	void Update ()
	{
		var renderers = FindObjectsOfType<Renderer> ();
		foreach (var renderer in renderers) {
			if (!VisibleFrom (renderer, this.MainCamera)) {
				this.MoveObject (renderer);
			}
		}
	}

	public void InstantiateGhosts ()
	{
		this.InstantiateGhost ("n").name = "North Camera";
		this.InstantiateGhost ("s").name = "South Camera";
		this.InstantiateGhost ("e").name = "East Camera";
		this.InstantiateGhost ("w").name = "West Camera";
		this.InstantiateGhost ("ne").name = "Northeast Camera";
		this.InstantiateGhost ("se").name = "Southeast Camera";
		this.InstantiateGhost ("nw").name = "Northwest Camera";
		this.InstantiateGhost ("sw").name = "Southwest Camera";
	}

	Camera InstantiateGhost (string position)
	{
		this.ghosts [position] = Instantiate (
			this.MainCamera, this.transform.position, this.transform.rotation) as Camera;
		return this.ghosts [position];
	}

	void PositionGhosts ()
	{
		this.ghosts ["n"].transform.position = this.transform.position
			+ new Vector3 (0, this.height, 0);
		this.ghosts ["s"].transform.position = this.transform.position
			+ new Vector3 (0, -this.height, 0);
		this.ghosts ["e"].transform.position = this.transform.position
			+ new Vector3 (this.width, 0, 0);
		this.ghosts ["w"].transform.position = this.transform.position
			+ new Vector3 (-this.width, 0, 0);
		this.ghosts ["ne"].transform.position = this.transform.position
			+ new Vector3 (this.width, this.height, 0);
		this.ghosts ["se"].transform.position = this.transform.position
			+ new Vector3 (-this.width, this.height, 0);
		this.ghosts ["nw"].transform.position = this.transform.position
			+ new Vector3 (this.width, -this.height, 0);
		this.ghosts ["sw"].transform.position = this.transform.position
			+ new Vector3 (this.width, -this.height, 0);
	}

	void RotateGhosts ()
	{
		this.ghosts ["n"].transform.rotation = this.transform.rotation;
		this.ghosts ["s"].transform.rotation = this.transform.rotation;
		this.ghosts ["e"].transform.rotation = this.transform.rotation;
		this.ghosts ["w"].transform.rotation = this.transform.rotation;
		this.ghosts ["ne"].transform.rotation = this.transform.rotation;
		this.ghosts ["se"].transform.rotation = this.transform.rotation;
		this.ghosts ["nw"].transform.rotation = this.transform.rotation;
		this.ghosts ["sw"].transform.rotation = this.transform.rotation;
	}

	void DestroyGhostComponent<T> () where T : Component
	{
		foreach (var entry in this.ghosts) {
			Destroy (this.ghosts [entry.Key].GetComponent<T> ());
		}
	}

	void SetGhostClearFlags (CameraClearFlags clearFlags)
	{
		foreach (var entry in this.ghosts) {
			entry.Value.clearFlags = clearFlags;
		}
	}

	void MoveObject (Renderer renderer)
	{
		foreach (var entry in this.ghosts) {
			if (VisibleFrom (renderer, entry.Value)) {
				renderer.transform.position =
					this.MainCamera.transform.position
					+ (renderer.transform.position - entry.Value.transform.position);
				break;
			}
		}
	}

	static bool VisibleFrom (Renderer renderer, Camera camera)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
		return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
	}
}
