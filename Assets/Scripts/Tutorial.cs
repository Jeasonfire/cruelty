using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	private static string[] tutorialMessages = {
		"Congratulations, you're now in charge of this small village!",

		"You can pick workers by dragging your mouse on the terrain.",

		"You can command the picked workers to move by right-clicking.",

		"You need to gather materials so you can arm up your workers.",

		"To gather materials, move your workers next to rocks or trees.",

		"When you have the required resources, move your workers to\n" +
		"the barracks, it will consume some resources and arm your\n" +
		"worker with a spear. (NOTE: Each spear costs 2 rocks and 5 wood.)",

		"When you have some armed workers (soldiers), you can start\n" +
		"conquering the neighboring tribes.",

		"That's indeed your objective: exterminate the other tribes\n" +
		"so that you can have their lands and resources!",

		"That's it! You're on your own now, go ahead and conquer the lands!"
	};
	private static int tutorialIndex = 0;
	private static float lastNextTime = 0;

	public static void NextTutorial () {
		if (lastNextTime + 0.15 < Time.fixedTime) {
			HUDController.PostMessage (tutorialIndex < tutorialMessages.Length ? tutorialMessages[tutorialIndex++] : "", true);
			lastNextTime = Time.fixedTime;
		}
	}

}
