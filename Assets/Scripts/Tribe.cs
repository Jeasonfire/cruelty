using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tribe : MonoBehaviour {

	public string tribeName;
	public GameObject character;
	public GameObject house;
	public GameObject barracks;
	public GameObject hut;
	
	private List<CharacterBehaviour> characters = new List<CharacterBehaviour> ();
	private List<Building> buildings = new List<Building> ();
	private int storedWood = 0;
	private int storedRock = 0;

	void Start () {
		switch (tribeName) {
		case "player":
			CreateCharacter (new Vector3 (2, 0, 0));
			CreateCharacter (new Vector3 (-2, 0, 0));
			CreateCharacter (new Vector3 (0, 0, -2));
			CreateBuilding (house, new Vector3 (0, 0, 10));
			CreateBuilding (barracks, new Vector3 (-12, 0, 10));
			CreateBuilding (hut, new Vector3 (7.5f, 0, -6));
			CreateBuilding (hut, new Vector3 (-7.5f, 0, -6));
			CreateBuilding (hut, new Vector3 (0, 0, -6));
			break;
		case "ai01":
			CreateCharacter (new Vector3 (2, 0, 0));
			CreateCharacter (new Vector3 (-2, 0, 0));
			CreateCharacter (new Vector3 (0, 0, -2));
			CreateBuilding (house, new Vector3 (0, 0, 10));
			CreateBuilding (hut, new Vector3 (7.5f, 0, -6));
			CreateBuilding (hut, new Vector3 (-7.5f, 0, -6));
			CreateBuilding (hut, new Vector3 (0, 0, -6));
			break;
		}
	}

	void LateUpdate () {
		for (int i = 0; i < characters.Count; i++) {
			if (characters[i].ShouldBeKilled ()) {
				KillCharacter (characters[i]);
				i--;
			}
		}
	}

	public void StoreWood (int amt) {
		storedWood += amt;
	}
	
	public void StoreRock (int amt) {
		storedRock += amt;
	}

	public void KillCharacter (CharacterBehaviour cb) {
		characters.Remove (cb);
		Destroy (cb.gameObject);
	}
	
	public void KillBuilding (Building b) {
		buildings.Remove (b);
		Destroy (b.gameObject);
	}
	
	public int GetResourceAmount (string resource) {
		switch (resource) {
		case "wood":
			return storedWood;
		case "rock":
			return storedRock;
		default:
			return -1;
		}
	}

	public void CreateCharacter (Vector3 position) {
		CharacterBehaviour cb = AddCharacter ((GameObject) Instantiate (character, transform.TransformPoint (position), character.transform.rotation));
		cb.tribe = this;	
		cb.transform.parent = transform;
	}

	public CharacterBehaviour AddCharacter (GameObject obj) {
		CharacterBehaviour cb = obj.GetComponent<CharacterBehaviour> ();
		if (cb != null) {
			characters.Add (cb);
		} else {
			Debug.Log ("Couldn't add " + obj.name + " as a character, because it doesn't have the CharacterBehaviour component!");
		}
		return cb;
	}
	
	public int GetCharacterCount() {
		return characters.Count;
	}
	
	public List<CharacterBehaviour> GetCharacters() {
		return characters;
	}
	
	public void CreateBuilding (GameObject building, Vector3 position) {
		Building b = AddBuilding ((GameObject) Instantiate (building, transform.TransformPoint (position), building.transform.rotation));
		b.tribe = this;
		b.transform.parent = transform;
	}
	
	public Building AddBuilding (GameObject obj) {
		Building b = obj.GetComponent<Building> ();
		if (b != null) {
			buildings.Add (b);
		} else {
			Debug.Log ("Couldn't add " + obj.name + " as a building, because it doesn't have the Building component!");
		}
		return b;
	}
	
	public int GetBuildingCount() {
		return buildings.Count;
	}
	
	public List<Building> GetBuildings() {
		return buildings;
	}

}
