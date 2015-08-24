using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {

	public string buildingType;
	public Tribe tribe;

	void OnTriggerEnter (Collider other) {
		CharacterBehaviour cb = other.GetComponent<CharacterBehaviour> ();
		if (cb != null) {
			switch (buildingType) {
			case "hut":
				if (cb.CarriedResourceType ().Equals ("wood")) {
					tribe.StoreWood (cb.CarriedResourceAmount ());
				} else if (cb.CarriedResourceType ().Equals ("rock")) {
					tribe.StoreRock (cb.CarriedResourceAmount ());
				} else if (!cb.CarriedResourceType ().Equals ("")) {
					Debug.Log (other.name + " was carrying " + cb.CarriedResourceType() + ", which is unknown. (Is it not lowercase?)");
				}
				cb.ResetCarrying ();
				break;
			case "barracks":
				if (!cb.GetJob ().Equals ("kill")) {
					string message = "";
					if (tribe.GetResourceAmount ("wood") < 5) {
						message += "Not enough wood! (" + tribe.GetResourceAmount ("wood") + " / 5)";
					}
					if (tribe.GetResourceAmount ("rock") < 2) {
						message += "\nNot enough rocks! (" + tribe.GetResourceAmount ("rock") + " / 2)";
					}
					if (message.Equals ("")) {
						cb.SetJob ("kill");
						tribe.StoreWood (-5);
						tribe.StoreRock (-2);
					} else {
						HUDController.PostMessage (message, false);
					}
				} else {
					cb.SetJob ("");
					tribe.StoreWood (5);
					tribe.StoreRock (2);
				}
				break;
			}

		}
	}

	void OnTriggerExit (Collider other) {
	}

}
