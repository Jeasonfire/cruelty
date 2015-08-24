using UnityEngine;
using System.Collections;

public class DayNightCycler : MonoBehaviour {

	public Transform sun;
	public float cycleSpeed;

	void Update () {
		float speed = cycleSpeed;
		if (sun.eulerAngles.x < 0 || sun.eulerAngles.x > 180) {
			speed *= 3;
		}
		sun.Rotate (new Vector3 (speed * Time.deltaTime, 0, 0));
	}

}
