using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum BuildableUnitTypes {
	SmallStarbase,
	MediumStarbase,
	LargeStarbase,
	SmallStardock,
	MediumStardock,
	LargeStardock,
	SmallJumpgate,
	MediumJumpgate,
	LargeJumpgate
}

public class ConstructionStarship : MonoBehaviour {

	private Transform myTransform;
	public GameObject NanobotPrefab;

	public int NumberOfBays = 0;
	private NanobotBayController[] NanoBays;

	public float NanobotMoveSpeed = 1.0f;

	// Test Construction
	public bool TestConstructBuilding = false;
	public BuildableUnitTypes UnitToBuild = BuildableUnitTypes.SmallStarbase;
	public ColorSchemeTypes UnitColoring = ColorSchemeTypes.Red;
	private StarshipTypes testBuildSize = StarshipTypes.Small;

	// Construction Variables
	public bool ConstructionStarted = false;
	public int ConstructionStep = 0;
	public bool AllNanoBotsInPosition = false;
	public int AvailNanobotCount = 0;
	private List<NanobotController> availableNanobots;
	private Transform constructionGridTransform;

	private GameObject testObjectToConstruct;
	private ConstructStarship constructScript;
	private float buildRadius = 50;
	private float buildDistance = 50;
	private Vector3 buildPosition;
	private Quaternion buildRotation;

	// Use this for initialization
	void Start () {
		myTransform = gameObject.transform;

		// Setup Nanobays
		NanoBays = gameObject.GetComponentsInChildren<NanobotBayController> ();
		NumberOfBays = NanoBays.Length;

		// Spawn Nanobots
		if (NanobotPrefab != null) {
			SpawnNanobots ();

			// Store Nanobots
			availableNanobots = new List<NanobotController> ();
			if (NanoBays.Length > 0) {
				for (int i = 0; i < NanoBays.Length; i++) {
					availableNanobots.Add (NanoBays [i].NanoBotScript);
				}
			}
			AvailNanobotCount = availableNanobots.Count;

			CreateConstructionGridLocation ();
		}
	}

	private void CreateConstructionGridLocation() {
		// Create New Construction Grid Location
		GameObject newConstructGrid = new GameObject("ConstructionGrid");
		newConstructGrid.transform.position = myTransform.position;

		// Create Nanobot Position Locations
		if (NanoBays.Length > 0) {
			for (int i = 0; i < NanoBays.Length; i++) {
				GameObject newConstructLocation = new GameObject ("ConstructionLocation" + i.ToString ());
				newConstructLocation.transform.parent = newConstructGrid.transform;
				NanoBays [i].ConstructPositionTransform = newConstructLocation.transform;
			}
		}

		// Assign and Make Child
		newConstructGrid.transform.parent = myTransform;
		constructionGridTransform = newConstructGrid.transform;
	}

	// Update is called once per frame
	void Update () {
		// Test Construction
		if (Application.isEditor) {
			if (Input.GetKeyUp (KeyCode.Space)) {
				TestConstructBuilding = true;
			}
		}
		if (TestConstructBuilding) {
			ConstructionStarted = true;

			buildRadius = GetBuildRadius(UnitToBuild);
			buildDistance = GetBuildDistance(UnitToBuild);
			buildPosition = myTransform.position + (myTransform.forward * buildDistance);
			buildRotation = myTransform.rotation;

			// Construct Medium Jumpgate as Test Construction Building
			testObjectToConstruct = StarshipPrefabManager.GlobalAccess.GetStarbasePrefab(StarshipTypes.Small, ColorSchemeTypes.Red);
			testBuildSize = StarshipTypes.Small;

			// Select Builable Unit
			if (UnitToBuild == BuildableUnitTypes.SmallStarbase) {
				testObjectToConstruct = StarshipPrefabManager.GlobalAccess.GetStarbasePrefab(StarshipTypes.Small, UnitColoring);	
				testBuildSize = StarshipTypes.Small;
			}
			else if (UnitToBuild == BuildableUnitTypes.MediumStarbase) {
				testObjectToConstruct = StarshipPrefabManager.GlobalAccess.GetStarbasePrefab(StarshipTypes.Medium, UnitColoring);	
				testBuildSize = StarshipTypes.Medium;
			}
			else if (UnitToBuild == BuildableUnitTypes.LargeStarbase) {
				testObjectToConstruct = StarshipPrefabManager.GlobalAccess.GetStarbasePrefab(StarshipTypes.Large, UnitColoring);	
				testBuildSize = StarshipTypes.Large;
			}
			else if (UnitToBuild == BuildableUnitTypes.SmallStardock) {
				testObjectToConstruct = StarshipPrefabManager.GlobalAccess.GetStardockPrefab(StarshipTypes.Small, UnitColoring);	
				testBuildSize = StarshipTypes.Small;
			}
			else if (UnitToBuild == BuildableUnitTypes.MediumStardock) {
				testObjectToConstruct = StarshipPrefabManager.GlobalAccess.GetStardockPrefab(StarshipTypes.Medium, UnitColoring);	
				testBuildSize = StarshipTypes.Medium;
			}
			else if (UnitToBuild == BuildableUnitTypes.LargeStardock) {
				testObjectToConstruct = StarshipPrefabManager.GlobalAccess.GetStardockPrefab(StarshipTypes.Large, UnitColoring);	
				testBuildSize = StarshipTypes.Large;
			}
			else if (UnitToBuild == BuildableUnitTypes.SmallJumpgate) {
				testObjectToConstruct = StarshipPrefabManager.GlobalAccess.GetJumpgatePrefab(StarshipTypes.Small, UnitColoring);	
				testBuildSize = StarshipTypes.Small;
			}
			else if (UnitToBuild == BuildableUnitTypes.MediumJumpgate) {
				testObjectToConstruct = StarshipPrefabManager.GlobalAccess.GetJumpgatePrefab(StarshipTypes.Medium, UnitColoring);	
				testBuildSize = StarshipTypes.Medium;
			}
			else if (UnitToBuild == BuildableUnitTypes.LargeJumpgate) {
				testObjectToConstruct = StarshipPrefabManager.GlobalAccess.GetJumpgatePrefab(StarshipTypes.Large, UnitColoring);	
				testBuildSize = StarshipTypes.Large;
			}

			// Instantiate Test Object
			GameObject newTestingBuilding = GameObject.Instantiate(testObjectToConstruct, buildPosition, buildRotation) as GameObject;
			newTestingBuilding.name = "ConstructionTestObject";

			constructScript = newTestingBuilding.AddComponent<ConstructStarship> ();
			if (constructScript != null) {
				
				constructScript.RegisterNanobots (availableNanobots);
				constructScript.AutoStartConstruction = false;
				constructScript.ConstructFromDebrisScript ();

				constructionGridTransform.position = constructScript.transform.position;

				if (NanoBays.Length > 0) {
					for (int i = 0; i < NanoBays.Length; i++) {
						NanoBays [i].SetupConstructionProject (constructScript, buildRadius, i, testBuildSize, NanoBays.Length);
					}
				}
			}

			constructionTimeElapsed = 0;
			AllNanoBotsInPosition = false;

			ConstructionStep = constructScript.CurrentStep;

			TestConstructBuilding = false;
		}

		if (ConstructionStarted) {
			ConstructionStep = constructScript.CurrentStep;
			if (ConstructionStep < 5)
				ConstructBuilding ();
			else {
				if (NanoBays.Length > 0) {
					for (int i = 0; i < NanoBays.Length; i++) {
						NanoBays [i].ReturnStage = 1;
					}
				}
				ConstructionStarted = false;
			}
		} else {
			// Return Nanobots to Bays
			if (NanoBays.Length > 0) {
				for (int i = 0; i < NanoBays.Length; i++) {
					NanoBays [i].UpdateNanoBot (ConstructionStarted);
				}
			}
		}
	}

	private float constructionTimeElapsed = 0;
	private void ConstructBuilding() {
		constructionTimeElapsed += Time.deltaTime;

		bool allBotsInPosition = true;
		if (NanoBays.Length > 0) {
			for (int i = 0; i < NanoBays.Length; i++) {
				NanoBays [i].UpdateNanoBot (constructScript.enabled);
				if (!NanoBays [i].BotInConstructPosition)
					allBotsInPosition = false;
				if (constructionTimeElapsed < 2.0f)
					NanoBays [i].MoveTowardGotoPointOne ();
				else
					NanoBays [i].MoveTowardConstructPosition ();
			}
		}

		if (allBotsInPosition) {
			// All Bots In Position
			AllNanoBotsInPosition = true;
			if (!constructScript.ConstructionStarted)
				constructScript.ConstructionStarted = true;
			else {
				// Building Being Constructed
				if (constructScript.CurrentStep != 5) {
					AddNanoBuildEffects();
					constructionGridTransform.RotateAround (constructionGridTransform.position, constructionGridTransform.up, 40 * Time.deltaTime);
				} else {
					// Construction Complete
					Debug.Log("Construction Complete.");
//					ConstructionStarted = false;
				}
			}		
		}
	}

	private float nanoEffectsTimer = 0;
	private float nanoEffectsTimerFreq = 0.025f;
	private void AddNanoBuildEffects() {
		if (StarshipPrefabManager.GlobalAccess != null) {
			if (nanoEffectsTimer < nanoEffectsTimerFreq) {
				nanoEffectsTimer += Time.deltaTime;
			} else {
				if (testBuildSize == StarshipTypes.Small) {
					StarshipPrefabManager.GlobalAccess.AddSmallPrimeNanoEffects (buildPosition);
				}
				else if (testBuildSize == StarshipTypes.Medium) {
					StarshipPrefabManager.GlobalAccess.AddMediumPrimeNanoEffects (buildPosition);
				}
				else if (testBuildSize == StarshipTypes.Large) {
					StarshipPrefabManager.GlobalAccess.AddLargePrimeNanoEffects (buildPosition);
				}
				nanoEffectsTimer = 0;
			}
		}
	}

	private void SpawnNanobots() {
		// Spawns NanoBots at bays
		if (NanoBays.Length > 0) {
			for (int i = 0; i < NanoBays.Length; i++) {

				// Spawn Nanobot on Current Bay
				GameObject newNanoBotGO = GameObject.Instantiate(NanobotPrefab, myTransform.position, myTransform.rotation) as GameObject;
				newNanoBotGO.name = "ActiveNanobot_" + i.ToString();
				NanoBays [i].SetupNanobay (this, newNanoBotGO, NanobotMoveSpeed);
				newNanoBotGO.transform.parent = NanoBays [i].transform;

				GameObject newNanoGotoPoint01 = new GameObject ();
				newNanoGotoPoint01.name = "Nano_0" + i.ToString() + "_GoToPoint01";
				NanoBays [i].SetupNanobayGoToPointOne (newNanoGotoPoint01.transform);

			}
		}
	}

	private float GetBuildRadius(BuildableUnitTypes unitToBuild) {
		float smallRadius = 40;
		float mediumRadius = 45;
		float largeRadius = 60;
		switch (unitToBuild) {
		case BuildableUnitTypes.SmallStarbase:
			return smallRadius;
		case BuildableUnitTypes.MediumStarbase:
			return mediumRadius;
		case BuildableUnitTypes.LargeStarbase:
			return largeRadius;
		case BuildableUnitTypes.SmallStardock:
			return smallRadius;
		case BuildableUnitTypes.MediumStardock:
			return mediumRadius;
		case BuildableUnitTypes.LargeStardock:
			return largeRadius;
		case BuildableUnitTypes.SmallJumpgate:
			return smallRadius;
		case BuildableUnitTypes.MediumJumpgate:
			return mediumRadius;
		case BuildableUnitTypes.LargeJumpgate:
			return largeRadius;
		default:
			return mediumRadius;
		}
	}

	private float GetBuildDistance(BuildableUnitTypes unitToBuild) {
		float smallDistance = 35;
		float mediumDistance = 40;
		float largeDistance = 60;
		switch (unitToBuild) {
		case BuildableUnitTypes.SmallStarbase:
			return smallDistance;
		case BuildableUnitTypes.MediumStarbase:
			return mediumDistance;
		case BuildableUnitTypes.LargeStarbase:
			return largeDistance;
		case BuildableUnitTypes.SmallStardock:
			return smallDistance;
		case BuildableUnitTypes.MediumStardock:
			return mediumDistance;
		case BuildableUnitTypes.LargeStardock:
			return largeDistance;
		case BuildableUnitTypes.SmallJumpgate:
			return smallDistance;
		case BuildableUnitTypes.MediumJumpgate:
			return mediumDistance;
		case BuildableUnitTypes.LargeJumpgate:
			return largeDistance;
		default:
			return smallDistance;
		}
	}
}
