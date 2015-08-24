using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovementController : MonoBehaviour {

	public SelectionController selectionController;
	public Camera cam;

	void Update () {
		if (Input.GetAxis ("Action") != 0) {
			Ray mouseRay = cam.ScreenPointToRay (Input.mousePosition);
			RaycastHit mouseHitInfo = new RaycastHit ();
			if (Physics.Raycast (mouseRay, out mouseHitInfo, 500, 1 << 8)) {
				Vector3 mousePoint = mouseHitInfo.point;
				List<GameObject> objects = selectionController.GetObjects();
				if (objects != null) {
					int objCount = 0;
					Vector3 objPosTotal = new Vector3 (0, 0, 0);
					foreach (GameObject obj in objects) {
						if (obj != null) {
							objPosTotal += obj.transform.position;
							objCount++;
						}
					}
					Vector3 moveVector = mousePoint - (objPosTotal / objCount);
					foreach (GameObject obj in objects) {
						if (obj != null) {
							CharacterBehaviour cb = obj.GetComponent<CharacterBehaviour> ();
							if (cb != null) {
								cb.SetTargetPosition (obj.transform.position + moveVector);
							} else {
								Debug.Log (obj.name + " doesn't have a CharacterBehaviour!");
							}
						}
					}
				}
			}
		}
	}

}
