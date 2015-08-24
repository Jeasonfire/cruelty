using UnityEngine;
using System.Collections;

public class GameOverChecker : MonoBehaviour {

	public Tribe aiTribe;

	void Update () {
		if (aiTribe.GetCharacterCount () <= 0) {
			Application.LoadLevel (2);
		}
	}

}
