using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectionBoxController : MonoBehaviour {

	public List<GameObject> selectedObjects = new List<GameObject>();

	void Update () {
		foreach (GameObject obj in selectedObjects) {
			CharacterBehaviour cb = obj.GetComponent<CharacterBehaviour> ();
			if (cb != null && cb.ShouldBeKilled ()) {
				selectedObjects.Remove (obj);
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		CharacterBehaviour cb = other.GetComponent<CharacterBehaviour> ();
		if (cb != null && cb.tribe.tribeName.Equals ("player")) {
			selectedObjects.Add (other.gameObject);
		}
	}

	void OnTriggerExit(Collider other) {
		selectedObjects.Remove (other.gameObject);
	}

}
