using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform camBase;
	public Transform eye;

	public float moveSpeed = 20;

	public Vector2 mouseSensitivity = new Vector2 (1, 1);
	public float maxVerticalRotation = 80;
	public float minVerticalRotation = 10;

	public float zoomSensitivity = 5;
	public float maxZoomLevel = 50;
	public float minZoomLevel = 4;
	private float currentZoomLevel;
	private float defaultZoomLevel;

	void Start() {
		currentZoomLevel = -eye.localPosition.z;
		defaultZoomLevel = -eye.localPosition.z;
		UpdateEyeDepth ();
	}

	void Update () {
		Camera.main.fieldOfView = Options.fov;
		if (Input.GetAxis ("RotateCameraSnap") != 0) {
			camBase.localEulerAngles = new Vector3(camBase.localEulerAngles.x,
			                                       camBase.localEulerAngles.y + Input.GetAxis ("RotateCameraSnap") * 180 * Time.deltaTime, 
			                                       camBase.localEulerAngles.z);
		}
		if (Input.GetAxis ("ResetCameraRotation") != 0) {
			camBase.localEulerAngles = new Vector3(camBase.localEulerAngles.x,
			                                       0, camBase.localEulerAngles.z);
			currentZoomLevel = defaultZoomLevel;
			UpdateEyeDepth ();
		}
		float scroll = Input.GetAxis ("Mouse ScrollWheel");
		if (scroll != 0) {
			currentZoomLevel = Mathf.Clamp (currentZoomLevel - scroll * zoomSensitivity * (currentZoomLevel / 10), minZoomLevel, maxZoomLevel);
			UpdateEyeDepth ();
		}
		Vector3 move = new Vector3 (Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		if (move.sqrMagnitude != 0) {
			move = camBase.TransformDirection (move);
			move.Set (move.x, 0, move.z);
			move = move.normalized * moveSpeed;
			camBase.localPosition += move * Time.deltaTime * Mathf.Pow(currentZoomLevel, 1.25f) / 25;
			camBase.localPosition = new Vector3 (Mathf.Clamp (camBase.localPosition.x, -80, 80), camBase.localPosition.y, 
			                                     Mathf.Clamp (camBase.localPosition.z, -80, 80));
		}
	}

	void UpdateEyeDepth() {
		eye.localPosition = new Vector3 (eye.localPosition.x, eye.localPosition.y, -currentZoomLevel);
		camBase.localEulerAngles = new Vector3(minVerticalRotation + (maxVerticalRotation - minVerticalRotation) * (currentZoomLevel / maxZoomLevel), 
		                                       camBase.localEulerAngles.y, camBase.localEulerAngles.z);
	}
	
}
