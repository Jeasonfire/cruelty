using UnityEngine;
using System.Collections;

public class TutorialButton : MonoBehaviour {

	public Camera cam;
	public Collider buttonCollider;

	void Update () {
		Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		if (buttonCollider.Raycast (mouseRay, out hitInfo, 500) && (Input.GetAxis ("Selection") != 0 || Input.GetAxis ("Action") != 0)) {
			Tutorial.NextTutorial ();
		}
	}

}
