using UnityEngine;
using System.Collections;

public class Resource : MonoBehaviour {

	public string resourceType;
	public float resourcesCollectingDelay;
	public int resourcesAmount;

	void Update () {
		if (resourcesAmount <= 0) {
			Destroy (transform.parent.gameObject);
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Character") {
			CharacterBehaviour cb = other.GetComponent<CharacterBehaviour> ();
			if (cb != null) {
				cb.StartCollectingResource (this);
			}
		}
	}
	
	void OnTriggerExit (Collider other) {
		if (other.tag == "Character") {
			CharacterBehaviour cb = other.GetComponent<CharacterBehaviour> ();
			if (cb != null) {
				cb.StopCollectingResources ();
			}
		}
	}

}
