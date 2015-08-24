using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	void Play () {
		Application.LoadLevel (1);
	}
	
	void Options () {
		Application.LoadLevel (3);
	}
	
	void Quit () {
		Application.Quit ();
	}

}
