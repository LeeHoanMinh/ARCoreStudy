using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class JumpgateController : MonoBehaviour {

	private Transform myTransform;
	public StarshipTypes MaxShipSpawnSize = StarshipTypes.Large;

	[Space(10)]

	public Transform[] LocalRotationSections;
	public bool RandomizeRotationAngles = true;
	private float randomChangeTimer = 0;
	private float randomChangeTimerFreq = 1.0f;
	public float LocalRotationSpeedMIN = 2.5f;
	public float LocalRotationSpeedMAX = 5f;
	private float currentRotationSpeed = 2.5f;

	[Space(10)]

	public GameObject JumpgateSpawnEffectPrefab;
	public GameObject JumpOutEffectPrefab;

	[Space(10)]
	public bool TestSpawnStarship = false;
	public bool DestroySpawnedStarship = false;
	public StarshipTypes TestTypeToSpawn = StarshipTypes.Small;
	public ColorSchemeTypes TestColorToSpawn = ColorSchemeTypes.Red;
	public int TestShipIndex = 0;
	[Space(10)]
	public GameObject TestShipToSpawnPrefab;

	private List<Transform> spawnedShipsToUpdate;
	private List<Transform> spawnedShipsCompleteList;

	private bool canSpawn = true;
	private float spawnDelayTimer = 0;
	private float spawnDelayTimerFreq = 1.75f;

	// Jump Out Variables
	public Transform StarshipToJumpOut;
	private Vector3 jumpOutStartPosition;
	private Vector3 jumpOutPosition;
	private float jumpMoveToPosSpeed = 0.5f;
	private float jumpOutLifeTime = 0;
	private bool jumpingOutStarship = false;
	private bool jumpOutShipInPosition = false;
	private bool jumpOutShip = false;
	public float distanceToJumpOutPos = 0;
	private float jumpOutDelayTimer = 0;
	private float jumpOutDelayTimerFreq = 1.5f;
	private bool jumpOutEffectSpawned = false;
	private float jumpOutStartPosDistance = 120;

	// Use this for initialization
	void Start () {
		myTransform = gameObject.transform;
		spawnedShipsToUpdate = new List<Transform> ();
		spawnedShipsCompleteList = new List<Transform> ();
		canSpawn = true;

		// Jump Out Setup
		jumpOutStartPosition = myTransform.position + (myTransform.forward * 120);
	}
	
	// Update is called once per frame
	void Update () {
		if (StarshipToJumpOut == null) {
			if (TestSpawnStarship) {
				// Test Spawn
				SpawnStarship ();
				TestSpawnStarship = false;
			}
		}

		// Jump Out Starship
		if (StarshipToJumpOut != null) {
			if (!jumpingOutStarship) {
				// Setup Jump Out
				jumpOutLifeTime = 0;
				jumpOutShipInPosition = false;
				jumpOutShip = false;
				jumpOutDelayTimer = 0;
				distanceToJumpOutPos = 1000;
				jumpOutEffectSpawned = false;
				jumpingOutStarship = true;
				if (MaxShipSpawnSize == StarshipTypes.Medium)
					jumpOutStartPosDistance = 25;
				else if (MaxShipSpawnSize == StarshipTypes.Small)
					jumpOutStartPosDistance = 6;
				else
					jumpOutStartPosDistance = 120;
			} else {
				// Update Jump Out Timer
				jumpOutLifeTime += Time.deltaTime;

				if (!jumpOutShipInPosition) {					
					// Keep Jump Out Position Updated
					jumpOutStartPosition = myTransform.position + (myTransform.forward * jumpOutStartPosDistance);
					Quaternion desiredLookRot = Quaternion.LookRotation (myTransform.position - StarshipToJumpOut.position);

					distanceToJumpOutPos = Vector3.Distance (StarshipToJumpOut.position, jumpOutStartPosition);
					if (distanceToJumpOutPos < 10.0f) {
						if (Quaternion.Angle (StarshipToJumpOut.rotation, desiredLookRot) < 10) {
							jumpOutShipInPosition = true;
						}
					}

					StarshipToJumpOut.position = Vector3.Lerp (StarshipToJumpOut.position, jumpOutStartPosition, jumpMoveToPosSpeed * Time.deltaTime);
					StarshipToJumpOut.rotation = Quaternion.Lerp (StarshipToJumpOut.rotation, desiredLookRot, (jumpMoveToPosSpeed / 2) * Time.deltaTime);
				} else {
					if (!jumpOutShip) {
						// Starship Ready to Jump Out
						jumpOutPosition = myTransform.position + (-myTransform.forward * 5);
						Quaternion desiredLookRot = Quaternion.LookRotation (myTransform.position - StarshipToJumpOut.position);

						distanceToJumpOutPos = Vector3.Distance (StarshipToJumpOut.position, jumpOutPosition);
						if (distanceToJumpOutPos < 10.0f) {
							jumpOutShip = true;
						}	

						StarshipToJumpOut.position = Vector3.Lerp (StarshipToJumpOut.position, jumpOutPosition, jumpMoveToPosSpeed * 2 * Time.deltaTime);
						StarshipToJumpOut.rotation = Quaternion.Lerp (StarshipToJumpOut.rotation, desiredLookRot, jumpMoveToPosSpeed * Time.deltaTime);
					} else {
						if (!jumpOutEffectSpawned) {
							CreateJumpOutEffect ();
							jumpOutEffectSpawned = true;
						} else {
							if (jumpOutDelayTimer < jumpOutDelayTimerFreq) {
								jumpOutDelayTimer += Time.deltaTime;
								StarshipToJumpOut.position += (StarshipToJumpOut.forward * jumpMoveToPosSpeed * Time.deltaTime);
							} else {
								// Jump Out Ship
								if (StarshipToJumpOut.localScale.z > 0.1f) {
									StarshipToJumpOut.position += (StarshipToJumpOut.forward * 25 * Time.deltaTime);
									Vector3 localScale = StarshipToJumpOut.localScale;
									localScale.z -= 2.5f * Time.deltaTime;
									StarshipToJumpOut.localScale = localScale;
								} else {
									Destroy (StarshipToJumpOut.gameObject);
									StarshipToJumpOut = null;
									jumpingOutStarship = false;
								}
							}
						}
					}
				}
			}
		}

		// Rotate Local Rotational Sections
		if (LocalRotationSections.Length > 0) {
			RotateLocalRotationSections ();
		}

		// Spawn Starship After Spawn Delay
		if (!canSpawn) {
			if (spawnDelayTimer < spawnDelayTimerFreq) {
				spawnDelayTimer += Time.deltaTime;
			} else {
				CreateStarship ();
				canSpawn = true;
			}
		}

		// Update Spawned Ships Scale
		if (spawnedShipsToUpdate.Count > 0) {
			for (int i = 0; i < spawnedShipsToUpdate.Count; i++) {
				if (spawnedShipsToUpdate [i].localScale.z < 1.0f) {
					Vector3 currentPosition = spawnedShipsToUpdate [i].position;
					if (MaxShipSpawnSize == StarshipTypes.Medium)
						currentPosition += spawnedShipsToUpdate[i].forward * 100 * Time.deltaTime;
					else if (MaxShipSpawnSize == StarshipTypes.Small)
						currentPosition += spawnedShipsToUpdate[i].forward * 50 * Time.deltaTime;
					else
						currentPosition += spawnedShipsToUpdate[i].forward * 150 * Time.deltaTime;
					spawnedShipsToUpdate [i].position = currentPosition;

					Vector3 currentScale = spawnedShipsToUpdate [i].localScale;
					currentScale.z += Time.deltaTime;
					spawnedShipsToUpdate [i].localScale = currentScale;
				} else {
					Vector3 currentScale = spawnedShipsToUpdate [i].localScale;
					currentScale.z = 1.0f;
					spawnedShipsToUpdate [i].localScale = currentScale;

					if (DestroySpawnedStarship) {
						spawnedShipsToUpdate [i].transform.root.gameObject.AddComponent<BFPWarpOutTimed> ();
					}

					spawnedShipsCompleteList.Add (spawnedShipsToUpdate [i]);
				}
			}
		}
		if (spawnedShipsCompleteList.Count > 0) {
			for (int i = 0; i < spawnedShipsCompleteList.Count; i++) {
				spawnedShipsToUpdate.Remove (spawnedShipsCompleteList [i]);
			}
			spawnedShipsCompleteList.Clear ();
		}
	}

	private void SpawnStarship() {
		if (canSpawn) {
			CreateJumpgateEffect ();
			spawnDelayTimer = 0;
			canSpawn = false;
		}
	}

	private void CreateJumpgateEffect() {
		GameObject newSpawnEffect = GameObject.Instantiate (JumpgateSpawnEffectPrefab, myTransform.position, myTransform.rotation) as GameObject;
		newSpawnEffect.name = "JumpgateSpawnEffect";		
	}

	private void CreateJumpOutEffect() {
		GameObject newSpawnEffect = GameObject.Instantiate (JumpOutEffectPrefab, myTransform.position, myTransform.rotation) as GameObject;
		newSpawnEffect.name = "JumpOutSpawnEffect";		
	}

	private void CreateStarship() {
		// Limit Spawn Size
		if (MaxShipSpawnSize == StarshipTypes.Small) {
			// Nothing Larger than Small Spawned Here
			if (TestTypeToSpawn != StarshipTypes.Small)
				TestTypeToSpawn = StarshipTypes.Small;
		}
		else if (MaxShipSpawnSize == StarshipTypes.Medium) {
			// Nothing Larger than Small Spawned Here
			if (TestTypeToSpawn == StarshipTypes.Large)
				TestTypeToSpawn = StarshipTypes.Medium;
			if (TestTypeToSpawn == StarshipTypes.Carrier)
				TestTypeToSpawn = StarshipTypes.Medium;
		}

		if (StarshipPrefabManager.GlobalAccess != null) {
			TestShipToSpawnPrefab = StarshipPrefabManager.GlobalAccess.GetStarshipPrefab (TestTypeToSpawn, TestColorToSpawn, TestShipIndex);
		}

		float spawnExitSpeed = 5;
		Vector3 spawnPosition = (myTransform.position - (myTransform.forward * 120));
		if (MaxShipSpawnSize == StarshipTypes.Medium) {
			spawnPosition = (myTransform.position - (myTransform.forward * 60));
			spawnExitSpeed = 2.5f;
		}
		if (MaxShipSpawnSize == StarshipTypes.Small) {
			spawnPosition = (myTransform.position - (myTransform.forward * 30));
			spawnExitSpeed = 1f;
		}
		GameObject testShipGO = GameObject.Instantiate (TestShipToSpawnPrefab, spawnPosition, myTransform.rotation) as GameObject;
		testShipGO.transform.localScale = new Vector3 (1, 1, 0);
		StarshipMovement moveScript = testShipGO.AddComponent<StarshipMovement> ();
		StarshipHealth healthScript = testShipGO.AddComponent<StarshipHealth> ();
		if (TestTypeToSpawn == StarshipTypes.Medium) {
			healthScript.SetupStarshipHealth (40);
		}
		else if (TestTypeToSpawn == StarshipTypes.Small) {
			healthScript.SetupStarshipHealth (10);
		}
		else if (TestTypeToSpawn == StarshipTypes.Large) {
			healthScript.SetupStarshipHealth (80);
		}
		else if (TestTypeToSpawn == StarshipTypes.Carrier) {
			healthScript.SetupStarshipHealth (60);
		}
		healthScript.SetupStarshipHealth (10);
		moveScript.Speed = spawnExitSpeed;
		moveScript.FlyForward = true;

		spawnedShipsToUpdate.Add (testShipGO.transform);
	}

	private bool rotateForward = true;
	private Vector3 localRotationEulers = Vector3.zero;
	private void RotateLocalRotationSections() {
		// Randomize Rotation Angle and Speed
		if (RandomizeRotationAngles) {
			if (randomChangeTimer < randomChangeTimerFreq) {
				randomChangeTimer += Time.deltaTime;
			} else {

				// Random Direction and Speed
				if (Random.Range (0, 100) < 50)
					rotateForward = true;
				else
					rotateForward = false;

				currentRotationSpeed = Random.Range (LocalRotationSpeedMIN, LocalRotationSpeedMAX);
				randomChangeTimer = 0;
				randomChangeTimerFreq = Random.Range (1.0f, 4.5f);
			}
		}

		// Rotate Sections
		if (rotateForward) {
			localRotationEulers.z += currentRotationSpeed * Time.deltaTime;
		} else {
			localRotationEulers.z -= currentRotationSpeed * Time.deltaTime;
		}

		for (int i = 0; i < LocalRotationSections.Length; i++) {
			if (i == 0)
				LocalRotationSections [i].localRotation = Quaternion.Euler (localRotationEulers);
			else
				LocalRotationSections [i].localRotation = Quaternion.Euler (-localRotationEulers);
		}
	}
}
