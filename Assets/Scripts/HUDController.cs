using UnityEngine;
using System.Collections;

public class HUDController : MonoBehaviour {

	public TextMesh woodValueText;
	public TextMesh rockValueText;
	public TextMesh workerValueText;
	public TextMesh messagesText;
	public TextMesh latestMessageText;
	public Tribe playerTribe;
	public float messageCooldown = 5;

	private static string[] messages = new string[13];
	private static int lastMsgRows = 1;
	private static float lastPostTime = 0;
	private static bool started = false;
	private static bool freeze = false;

	void Update () {
		woodValueText.text = "Wood: " + playerTribe.GetResourceAmount ("wood");
		rockValueText.text = "Rocks: " + playerTribe.GetResourceAmount ("rock");
		workerValueText.text = "Workers: " + playerTribe.GetCharacterCount ();
		foreach (CharacterBehaviour cb in playerTribe.GetCharacters ()) {
			string text = cb.GetName ();
			if (cb.CarriedResourceType () != "") {
				text += "\nGathered " + cb.CarriedResourceType () + ": " + cb.CarriedResourceAmount ();
			}
			cb.nameTextMesh.text = text;
		}
		messagesText.text = "";
		for (int i = messages.Length - 1; i > lastMsgRows - 1; i--) {
			messagesText.text += messages[i] + (i > lastMsgRows ? "\n" : "");
		}
		latestMessageText.text = "";
		for (int i = lastMsgRows - 1; i > 0; i--) {
			latestMessageText.text += messages[i] + (i > 0 ? "\n" : "");
		}
		latestMessageText.text += messages [0];

		if (lastPostTime + messageCooldown < Time.fixedTime && messageCooldown > 0 && !freeze) {
			PostMessage ("", false);
		}

		if (!started) {
			started = true;
			PostMessage ("Press the button on the right (and slightly above) to continue.", true);
		}
	}

	public static void PostMessage (string msg, bool freeze) {
		string[] msgSplit = msg.Split ('\n');
		string[] newMessages = new string[messages.Length];
		for (int i = 0; i < messages.Length - msgSplit.Length; i++) {
			newMessages[i + msgSplit.Length] = messages[i];
		}
		lastMsgRows = msgSplit.Length;
		for (int i = 0; i < lastMsgRows; i++) {
			newMessages [lastMsgRows - 1 - i] = msgSplit[i];
		}
		messages = newMessages;
		lastPostTime = Time.fixedTime;
		HUDController.freeze = freeze;
	}

}
