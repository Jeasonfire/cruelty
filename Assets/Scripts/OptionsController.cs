using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

	public Slider fov;
	public Slider sfx;
	public Slider music;
	public Text fovText;

	void Update () {
		Options.fov = fov.value;
		Options.sfxVolume = sfx.value;
		Options.musicVolume = music.value;
		fovText.text = "Fov: " + fov.value;
	}

	void MainMenu () {
		Application.LoadLevel (0);
	}

}
