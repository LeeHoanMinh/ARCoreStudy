using UnityEngine;
using System.Collections;

public class NanobotBayController : MonoBehaviour {

	private ConstructStarship parentScript;
	public Transform NanobaySlotTransform;

	// Active Nanobot
	public bool BayActive = false;
	public bool BayHasNanobot = false;
	private int returnStage = 0;
	public int ReturnStage {
		get {
			return returnStage;
		}
		set {
			returnStage = value;
		}
	}
	private GameObject nanoBotGameobject;
	private Transform nanoBotTransform;
	private NanobotController nanoBotScript;
	public NanobotController NanoBotScript {
		get { return nanoBotScript; }
	}
	private Vector3 nanoBotLocalRotEulers;

	private float BotRotationSpeed = 1.0f;
	private float nanobotMoveSpeed = 5.0f;

	// Goto Points
	private Transform GotoPointOne;
	private int constructPositionNumber = 0;
	public Transform ConstructPositionTransform;
	private float distanceFromConstructPoint = 0;

	// Active Construction Project
	public bool HasActiveProject = false;
	private int NumberOfBotsActive = 0;
	public bool BotDocked = true;
	private bool playedLaunchSFX = false;
	private bool playedDockSFX = false;
	public bool BotInConstructPosition = false;
	private float constructionRadius = 50;
	private ConstructStarship currentProjectScript;
	private Transform currentProjectTransform;
//	private float activeProjectLifetime = 0;
	private float returnDelayTimer = 0;
	private float returnDelayTimerFreq = 1.5f;

	public void SetupConstructionProject(ConstructStarship newProject, float constructionRadiusIn, int constructNumIn, StarshipTypes buildSizeIn, int numOfBots) {
		BotDocked = false;
		NumberOfBotsActive = numOfBots;
		currentProjectScript = newProject;
		currentProjectTransform = currentProjectScript.transform;

		constructionRadius = constructionRadiusIn;
		constructPositionNumber = constructNumIn;
		float yOffsetMultiplier = 0.75f;
		if (constructPositionNumber == 0) {			
			Vector3 constructOffset = new Vector3(constructionRadius, constructionRadius * yOffsetMultiplier, 0);
			constructOffset = currentProjectTransform.position + constructOffset;
			ConstructPositionTransform.position = constructOffset;
		}
		else if (constructPositionNumber == 1) {
			Vector3 constructOffset = new Vector3(-constructionRadius, constructionRadius * yOffsetMultiplier, 0);
			constructOffset = currentProjectTransform.position + constructOffset;
			ConstructPositionTransform.position = constructOffset;
		}
		else if (constructPositionNumber == 2) {
			Vector3 constructOffset = new Vector3(0, constructionRadius * yOffsetMultiplier, constructionRadius);
			constructOffset = currentProjectTransform.position + constructOffset;
			ConstructPositionTransform.position = constructOffset;
		}
		else if (constructPositionNumber == 3) {
			Vector3 constructOffset = new Vector3(0, constructionRadius * yOffsetMultiplier, -constructionRadius);
			constructOffset = currentProjectTransform.position + constructOffset;
			ConstructPositionTransform.position = constructOffset;
		}
		else if (constructPositionNumber == 4) {
			Vector3 constructOffset = new Vector3(constructionRadius / 2, constructionRadius * yOffsetMultiplier, -(constructionRadius / 2));
			constructOffset = currentProjectTransform.position + constructOffset;
			ConstructPositionTransform.position = constructOffset;
		}
		else if (constructPositionNumber == 5) {
			Vector3 constructOffset = new Vector3(-(constructionRadius / 2), constructionRadius * yOffsetMultiplier, (constructionRadius / 2));
			constructOffset = currentProjectTransform.position + constructOffset;
			ConstructPositionTransform.position = constructOffset;
		}

		if (NumberOfBotsActive < 4) {
			nanoBotScript.SetNanoBeamFreq (1.0f);
		} else {
			nanoBotScript.SetNanoBeamFreq (1.5f);
		}
		nanoBotScript.SizeClassBeingBuilt = buildSizeIn;

		nanoBotLocalRotEulers = Vector3.zero;
		HasActiveProject = true;
	}

	public void SetupNanobay(ConstructionStarship parentScript, GameObject nanoBot, float nanoMoveSpeedIn) {
		nanobotMoveSpeed = nanoMoveSpeedIn;

		nanoBotGameobject = nanoBot;
		nanoBotTransform = nanoBot.transform;
		nanoBotScript = nanoBot.GetComponent<NanobotController> ();
		nanoBotGameobject.transform.position = NanobaySlotTransform.position;
		nanoBotGameobject.transform.rotation = NanobaySlotTransform.rotation;

		if (nanoBotGameobject != null && nanoBotScript != null) {
			BayHasNanobot = true;
			BotDocked = true;
			BayActive = true;
		}
	}

	public void SetupNanobayGoToPointOne(Transform gotoPointTransformIn) {
		gotoPointTransformIn.position = NanobaySlotTransform.position;
		gotoPointTransformIn.rotation = NanobaySlotTransform.rotation;
		gotoPointTransformIn.position += gotoPointTransformIn.up * 20.0f;
		gotoPointTransformIn.parent = NanobaySlotTransform;
		GotoPointOne = gotoPointTransformIn;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateNanoBot(bool constructionActive) {
		if (constructionActive) {
			if (!playedLaunchSFX) {
				nanoBotScript.PlayLaunchSFX ();
				playedLaunchSFX = true;
			}
			distanceFromConstructPoint = Vector3.Distance (nanoBotTransform.position, ConstructPositionTransform.position);
			if (distanceFromConstructPoint < 0.5f)
				BotInConstructPosition = true;
			else
				BotInConstructPosition = false;
			nanoBotTransform.LookAt (currentProjectTransform.position);
			nanoBotLocalRotEulers = Vector3.Lerp (nanoBotLocalRotEulers, new Vector3 (-90, 0, 0), BotRotationSpeed * Time.deltaTime);
			nanoBotScript.UpdateLocalRotation (nanoBotLocalRotEulers);
			returnStage = 0;
		} else {
			// Returning To Ship
			if (returnStage == 1) {
				if (returnDelayTimer < returnDelayTimerFreq) {
					returnDelayTimer += Time.deltaTime;
				} else {
					returnDelayTimer = 0;
					returnStage = 2;
				}
			}
			else if (returnStage == 2) {
				distanceFromConstructPoint = Vector3.Distance (nanoBotTransform.position, GotoPointOne.position);
				if (distanceFromConstructPoint < 0.5f) {
					BotInConstructPosition = true;
					returnStage = 3;
				}
				else
					BotInConstructPosition = false;
				nanoBotTransform.LookAt (GotoPointOne.position);
				nanoBotLocalRotEulers = Vector3.Lerp (nanoBotLocalRotEulers, new Vector3 (0, 0, 0), BotRotationSpeed * Time.deltaTime);
				nanoBotScript.UpdateLocalRotation (nanoBotLocalRotEulers);

				nanoBotTransform.position = Vector3.Lerp (nanoBotTransform.position, GotoPointOne.position, nanobotMoveSpeed * Time.deltaTime);
			} else if (returnStage == 3) {
				distanceFromConstructPoint = Vector3.Distance (nanoBotTransform.position, NanobaySlotTransform.position);
				if (distanceFromConstructPoint < 0.25f) {
					BotInConstructPosition = true;
					if (!playedDockSFX) {
						nanoBotScript.PlayDockSFX ();
						playedDockSFX = true;
					}
					returnStage = 4;
				}
				else
					BotInConstructPosition = false;
				nanoBotLocalRotEulers = Vector3.Lerp (nanoBotLocalRotEulers, Vector3.zero, BotRotationSpeed * 4 * Time.deltaTime);
				nanoBotScript.UpdateLocalRotation (nanoBotLocalRotEulers);

				nanoBotTransform.position = Vector3.Lerp (nanoBotTransform.position, NanobaySlotTransform.position, nanobotMoveSpeed * Time.deltaTime);
				nanoBotTransform.rotation = Quaternion.Lerp (nanoBotTransform.rotation, NanobaySlotTransform.rotation, BotRotationSpeed * 4 * Time.deltaTime);
			} else if (returnStage == 4) {				
				nanoBotTransform.position = NanobaySlotTransform.position;
				nanoBotTransform.rotation = NanobaySlotTransform.rotation;
				returnDelayTimer = 0;
				BotInConstructPosition = false;
				playedLaunchSFX = false;
				playedDockSFX = false;
				BotDocked = true;
			}
		}
	}
	public void MoveTowardGotoPointOne() {
		nanoBotTransform.position = Vector3.Lerp (nanoBotTransform.position, GotoPointOne.position, nanobotMoveSpeed * Time.deltaTime);
	}
	public void MoveTowardConstructPosition() {
		nanoBotTransform.position = Vector3.Lerp (nanoBotTransform.position, ConstructPositionTransform.position, nanobotMoveSpeed * Time.deltaTime);
	}
}
