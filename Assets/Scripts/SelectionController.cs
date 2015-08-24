using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectionController : MonoBehaviour {

	public GameObject selectionPrefab;
	public GameObject selectionMarker;
	public Camera cam;

	private GameObject currentSelectionObject;
	private bool currentlySelecting = false;
	private Vector3 selectionStartCorner = new Vector3 (0, 0, 0);
	private Vector3 selectionEndCorner = new Vector3 (0, 0, 0);

	private List<GameObject> selectionMarkers = new List<GameObject> ();
	private List<GameObject> selectedObjects = new List<GameObject> ();

	void Update () {
		if (Input.GetAxis ("Selection") != 0) {
			Ray mouseRay = cam.ScreenPointToRay (Input.mousePosition);
			RaycastHit mouseHitInfo = new RaycastHit ();
			if (Physics.Raycast (mouseRay, out mouseHitInfo, 500, 1 << 8)) {
				// Mouse is over terrain and the coordinate it's on has been found. Continue operation.
				Vector3 mousePoint = mouseHitInfo.point;
				if (!currentlySelecting) {
					selectionStartCorner = mousePoint;
					currentSelectionObject = (GameObject)Instantiate (selectionPrefab, mousePoint, selectionPrefab.transform.rotation);
					currentlySelecting = true;
				}
				selectionEndCorner = mousePoint;
				currentSelectionObject.transform.localPosition = (selectionStartCorner + selectionEndCorner) / 2f;
				currentSelectionObject.transform.localEulerAngles = new Vector3 (0, cam.transform.eulerAngles.y,
				                                                                 cam.transform.eulerAngles.z);
				Vector3 scale = currentSelectionObject.transform.TransformDirection (new Vector3 (Mathf.Abs (selectionStartCorner.x - selectionEndCorner.x), 
				                                                                                  3 + Mathf.Abs (selectionStartCorner.y - selectionEndCorner.y),
				                                                                                  Mathf.Abs (selectionStartCorner.z - selectionEndCorner.z))); 
				currentSelectionObject.transform.localScale = scale;
			}
		} else if (currentlySelecting) {
			GetObjects (); // Update "selectedObjects"
			Destroy (currentSelectionObject.gameObject);
			currentlySelecting = false;
		}
		if (currentlySelecting && selectionMarkers.Count != GetObjects ().Count) {
			ClearMarkers ();
			SetMarkers ();
		}
	}

	void ClearMarkers () {
		foreach (GameObject go in selectionMarkers) {
			Destroy (go);
		}
		selectionMarkers.Clear ();
	}

	void SetMarkers () {
		foreach (GameObject go in GetObjects ()) {
			GameObject marker = (GameObject)Instantiate (selectionMarker, go.transform.localPosition, selectionMarker.transform.localRotation);
			marker.transform.parent = go.transform;
			selectionMarkers.Add (marker);
		}
	}

	public List<GameObject> GetObjects () {
		if (currentSelectionObject != null) {
			return selectedObjects = currentSelectionObject.GetComponent<SelectionBoxController> ().selectedObjects;
		}
		return selectedObjects;
	}

}
