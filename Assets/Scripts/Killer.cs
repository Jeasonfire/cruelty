using UnityEngine;
using System.Collections;

public class Killer : MonoBehaviour {

	void OnTriggerEnter (Collider other) {
		CharacterBehaviour cb = other.GetComponent<CharacterBehaviour> ();
		if (cb != null) {
			cb.Kill ();
		}
	}

}
