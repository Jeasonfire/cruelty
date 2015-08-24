using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundHandler : MonoBehaviour {

	private static Hashtable sounds = new Hashtable ();

	public AudioSource chop;
	public AudioSource mine;
	public AudioSource kill;
	public AudioSource equip;
	public AudioSource dequip;
	public AudioSource gameBGM;
	public AudioSource menuBGM;

	void Start () {
		sounds.Clear ();
		sounds.Add ("wood", chop);
		sounds.Add ("rock", mine);
		sounds.Add ("kill", kill);
		sounds.Add ("equip", equip);
		sounds.Add ("dequip", dequip);
		sounds.Add ("gameBGM", gameBGM);
		sounds.Add ("menuBGM", menuBGM);

		if (Application.loadedLevelName.Equals ("MainMenu")) {
			SoundHandler.PlaySound ("menuBGM", true, true);
		}
		if (Application.loadedLevelName.Equals ("Options")) {
			SoundHandler.PlaySound ("menuBGM", true, true);
		}
		if (Application.loadedLevelName.Equals ("Game")) {
			SoundHandler.PlaySound ("gameBGM", true, true);
		}
		if (Application.loadedLevelName.Equals ("GameOver")) {
			SoundHandler.PlaySound ("menuBGM", true, true);
		}
	}

	public static void PlaySound (string sound, bool loop = false, bool music = false) {
		if (sounds.ContainsKey (sound)) {
			AudioSource source = ((AudioSource)sounds[sound]);
			source.volume = music ? Options.musicVolume : Options.sfxVolume;
			source.loop = loop;
			source.Play ();
		} else {
			Debug.Log ("Tried to play sound that hasn't been loaded: " + sound);
		}
	}

}
