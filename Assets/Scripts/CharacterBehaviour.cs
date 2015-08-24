using UnityEngine;
using System.Collections;

public class CharacterBehaviour : MonoBehaviour {

	private string[] CHARACTER_NAMES = {"Evadeam", "Eleazar", "Jorin", "Thouche", "Neapolion", "Rosser", "Popiniau", "Ector", "Grifon", "Hoel",
											"Turquan", "Tintagel", "Juste", "Tristram", "Rasequin", "Kanelinqes", "Engenouf", "Kymme", "Ryons", "Esdelot",
											"Albin", "Merlin", "Celestria", "Jevan", "Mervyn", "Tristan", "Garreth", "Falconworth", "Pariset", "Jolis"};

	public CharacterController characterController;
	public float moveSpeed = 10;
	public TextMesh nameTextMesh;
	public GameObject chopIndicator;
	public GameObject mineIndicator;
	public GameObject killIndicator;
	public GameObject weapon;
	public Tribe tribe;

	private string characterName;
	private Vector3 targetPosition;
	private float lastTargetCheckTime = 0;
	private Vector3 lastPosition = new Vector3 (0, 0, 0);

	private Resource currentCollectedResource;
	private string currentCarriedType = "";
	private int currentCarriedAmount = 0;
	private float resourceCollectingTime;

	private string currentJob = "";
	private GameObject jobIndicator;
	private GameObject currentWeapon;

	private bool shouldBeKilled = false;

	void Start() {
		targetPosition = transform.position;
		int nameIndex = (int) (Random.value * (CHARACTER_NAMES.Length - 1));
		characterName = CHARACTER_NAMES[nameIndex];
	}

	void UpdateAI () {
		if (tribe == null) {
			return;
		}
		switch (tribe.tribeName) {
		case "ai01":
			if (targetPosition == transform.position && currentJob == "") {
				Vector3 move = new Vector3 (Random.value * 60 - 30, 0, Random.value * 60 - 30);
				targetPosition = tribe.transform.position + move;
			}
			break;
		}
	}

	void Update () {
		Vector3 move = (targetPosition - transform.position).normalized * moveSpeed;
		characterController.SimpleMove (move);

		if (lastTargetCheckTime + 0.05 <= Time.fixedTime) {
			if ((transform.position - lastPosition).magnitude < moveSpeed / 40 ) {
				targetPosition = transform.position;
			} else {
				transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x,
				                                          Mathf.Atan2 (move.x, move.z) * Mathf.Rad2Deg,
				                                          transform.localEulerAngles.z);
				nameTextMesh.transform.localEulerAngles = new Vector3 (nameTextMesh.transform.localEulerAngles.x,
				                                                       -Mathf.Atan2 (move.x, move.z) * Mathf.Rad2Deg,
				                                                       nameTextMesh.transform.localEulerAngles.z);
				if (jobIndicator != null) {
					jobIndicator.transform.localEulerAngles = new Vector3 (jobIndicator.transform.localEulerAngles.x,
					                                                       -Mathf.Atan2 (move.x, move.z) * Mathf.Rad2Deg,
					                                                       jobIndicator.transform.localEulerAngles.z);
				}
			}
			lastPosition = transform.position;
			lastTargetCheckTime = Time.fixedTime;
		}
	
		if (currentCollectedResource != null) {
			resourceCollectingTime += Time.deltaTime;
			if (resourceCollectingTime >= currentCollectedResource.resourcesCollectingDelay) {
				resourceCollectingTime = 0;
				if (currentCarriedType == "") {
					currentCarriedType = currentCollectedResource.resourceType;
					if (currentCarriedAmount != 0) {
						Debug.Log ("Something has gone wrong, " + gameObject.name + " was carrying " + currentCarriedAmount + " of nothing.");
					}
				}
				if (currentCarriedType != currentCollectedResource.resourceType) {
					Debug.Log ("Something has gone very wrong, " + gameObject.name + " tried to collect more than 1 type of resource!");
					return;
				}
				currentCarriedAmount++;
				currentCollectedResource.resourcesAmount--;
				if (tribe.tribeName.Equals ("player")) {
					SoundHandler.PlaySound (currentCollectedResource.resourceType);
				}
			}
		}

		switch (currentJob) {
		case "":
			if (jobIndicator != null) {
				Destroy (jobIndicator);
			}
			break;
		case "wood":
			if (jobIndicator == null) {
				jobIndicator = (GameObject) Instantiate (chopIndicator, transform.position, transform.rotation);
				jobIndicator.transform.parent = transform;
			}
			break;
		case "rock":
			if (jobIndicator == null) {
				jobIndicator = (GameObject) Instantiate (mineIndicator, transform.position, transform.rotation);
				jobIndicator.transform.parent = transform;
			}
			break;
		case "kill":
			if (jobIndicator == null) {
				jobIndicator = (GameObject) Instantiate (killIndicator, transform.position, transform.rotation);
				jobIndicator.transform.parent = transform;
				currentWeapon = (GameObject) Instantiate (weapon, transform.position, transform.rotation);
				currentWeapon.transform.parent = transform;
			}
			break;
		}
		if (!currentJob.Equals ("kill") && currentWeapon != null) {
			Destroy (currentWeapon);
		}

		UpdateAI ();
	}

	public void SetTargetPosition (Vector3 pos) {
		targetPosition = pos;
		lastTargetCheckTime = Time.fixedTime;
	}

	public void StartCollectingResource (Resource resource) {
		if ((!currentCarriedType.Equals ("") && !resource.resourceType.Equals (currentCarriedType))
		    || currentJob.Equals ("kill")) {
			return;
		}
		currentCollectedResource = resource;
		resourceCollectingTime = 0;
		SetJob (resource.resourceType);
	}

	public void StopCollectingResources () {
		if (currentCollectedResource != null) {
			currentCollectedResource = null;
		}
	}

	public string GetName () {
		return characterName;
	}

	public string CarriedResourceType () {
		return currentCarriedType;
	}

	public int CarriedResourceAmount () {
		return currentCarriedAmount;
	}

	public void ResetCarrying () {
		currentCarriedType = "";
		currentCarriedAmount = 0;
	}

	public string GetJob () {
		return currentJob;
	}

	public void SetJob (string job) {
		currentJob = job;
		Destroy (jobIndicator);
	}

	public void Kill () {
		shouldBeKilled = true;
	}

	public bool ShouldBeKilled() {
		return shouldBeKilled;
	}

}
