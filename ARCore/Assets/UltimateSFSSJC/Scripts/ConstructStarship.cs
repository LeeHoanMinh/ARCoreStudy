using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ConstructStarship : MonoBehaviour {

	// ConstructOnStart Variables
	public bool ConstructOnStart = false;
	public float BuildSpeed = 0.5f;
	private AudioSource myAudioSource;

	private Transform myTransform;
	private Vector3 startingPosition;
	public bool ConstructionStarted = false;
	public bool AutoStartConstruction = false;
	public bool ConstructionComplete = false;
	public ColorSchemeTypes ActiveColorScheme = ColorSchemeTypes.Red;
	public StarshipTypes ActiveTypeBeingBuilt = StarshipTypes.Small;

	private float constructionTimer = 0;
	private float TimeToConstructOneStep = 2.0f;
	private float ConstructionTime = 0f;
	public float ConstructionPercent = 0;

	private int constructionStepIndex = 0;
	public int CurrentStep = 1;
	private float constructionStepTimer = 0;
	private float constructionStepTimerFreq = 0.5f;

	public List<Transform> SortedRendererTransformList;
	public List<Renderer> SortedRendererList;
	public List<Material> OriginalMaterialList;

	private Renderer[] starshipRenderers;

	public Material[] OriginalMaterials;
	public Material[] ConstructionMaterials;

	// Construction Nanobots Variables
	private bool hasNanoBots = false;
	public List<NanobotController> AvailableNanobots;
	public void RegisterNanobots(List<NanobotController> availNanos) {
		AvailableNanobots = availNanos;
		if (AvailableNanobots.Count > 0) {
			hasNanoBots = true;
		} else {
			hasNanoBots = false;
		}
	}

	// Use this for initialization
	void Awake () {
		myTransform = gameObject.transform;
		startingPosition = myTransform.position;
		startingPosition.x = 0;
		SortedRendererTransformList = new List<Transform> ();
		SortedRendererList = new List<Renderer> ();
		OriginalMaterialList = new List<Material> ();
	}

	void Start () {
		if (ConstructOnStart) {
			GenerateDebrisOnDestroy debrisScript = gameObject.GetComponent<GenerateDebrisOnDestroy> ();
			if (debrisScript != null) {
				ColorSchemeTypes coloringToUse = ColorSchemeTypes.Red;
				switch (debrisScript.StarshipColoring) {
				case StarshipDebrisColors.Red:
					coloringToUse = ColorSchemeTypes.Red;
					break;
				case StarshipDebrisColors.Blue:
					coloringToUse = ColorSchemeTypes.Blue;
					break;
				case StarshipDebrisColors.Gray:
					coloringToUse = ColorSchemeTypes.Gray;
					break;
				case StarshipDebrisColors.Green:
					coloringToUse = ColorSchemeTypes.Green;
					break;
				case StarshipDebrisColors.Purple:
					coloringToUse = ColorSchemeTypes.Purple;
					break;
				case StarshipDebrisColors.Teal:
					coloringToUse = ColorSchemeTypes.Teal;
					break;
				default:
					coloringToUse = ColorSchemeTypes.Red;
					break;
				}
				AutoStartConstruction = true;
				SetupConstruction (BuildSpeed, debrisScript.StarshipType, coloringToUse);
			} else {
				Debug.LogError ("Can't Find GenerateDebrisOnDestroy.cs Script on GameObject.");
			}
		}
	}

	public void ConstructFromDebrisScript() {
		GenerateDebrisOnDestroy debrisScript = gameObject.GetComponent<GenerateDebrisOnDestroy> ();
		if (debrisScript != null) {
			ColorSchemeTypes coloringToUse = ColorSchemeTypes.Red;
			switch (debrisScript.StarshipColoring) {
			case StarshipDebrisColors.Red:
				coloringToUse = ColorSchemeTypes.Red;
				break;
			case StarshipDebrisColors.Blue:
				coloringToUse = ColorSchemeTypes.Blue;
				break;
			case StarshipDebrisColors.Gray:
				coloringToUse = ColorSchemeTypes.Gray;
				break;
			case StarshipDebrisColors.Green:
				coloringToUse = ColorSchemeTypes.Green;
				break;
			case StarshipDebrisColors.Purple:
				coloringToUse = ColorSchemeTypes.Purple;
				break;
			case StarshipDebrisColors.Teal:
				coloringToUse = ColorSchemeTypes.Teal;
				break;
			default:
				coloringToUse = ColorSchemeTypes.Red;
				break;
			}
			SetupConstruction (BuildSpeed, debrisScript.StarshipType, coloringToUse);
		}
	}

	public void SetupConstruction(float timeBuildOneStep, StarshipTypes starShipType, ColorSchemeTypes starshipColorScheme) {

		// Is Construct On Start add Sound FX
		if (ConstructOnStart) {
			myAudioSource = gameObject.AddComponent<AudioSource> ();
			// Setup Audio Clip
			myAudioSource.clip = StarshipPrefabManager.GlobalAccess.ConstructionSoundFX;
			myAudioSource.volume = 0.0f;
			myAudioSource.spatialBlend = 0.85f;
			myAudioSource.loop = true;
			myAudioSource.Play ();
		}

		// Setup Materials
		ActiveColorScheme = starshipColorScheme;
		ActiveTypeBeingBuilt = starShipType;
		OriginalMaterials = StarshipPrefabManager.GlobalAccess.GetMaterialSet (ActiveColorScheme);
		ConstructionMaterials = StarshipPrefabManager.GlobalAccess.GetConstructionMaterials ();

		// Find and Store All Renderers
		SortedRendererTransformList = new List<Transform> ();
		SortedRendererList = new List<Renderer> ();
		OriginalMaterialList = new List<Material> ();

		starshipRenderers = gameObject.GetComponentsInChildren<Renderer>();
		// Disable All Renderers to start
		if (starshipRenderers.Length > 0) {
			for (int i = 0; i < starshipRenderers.Length; i++) {
				if (starshipRenderers [i] != null) {
					SortedRendererTransformList.Add (starshipRenderers [i].gameObject.transform);
					starshipRenderers [i].enabled = false;
				}
			}
		}

		// Setup Step Timer Freq - 3 * 15 = 45 / 60
		TimeToConstructOneStep = timeBuildOneStep;
		ConstructionTime = TimeToConstructOneStep * SortedRendererTransformList.Count;
		constructionStepTimerFreq = TimeToConstructOneStep / 4;

		// Sort List by Distance
		Vector3 centerOfStarship = GetCenterPointOfStarship(SortedRendererTransformList);
		if (SortedRendererTransformList.Count > 1) {
			SortedRendererTransformList.Sort (delegate(Transform a, Transform b) {
				if (a != null && b != null) {
					if (Vector3.Distance (centerOfStarship, b.position) < Vector3.Distance (centerOfStarship, a.position)) {
						return 1;
					}
					else {
						return -1;
					}
				}
				else {
					return 0;
				}
			});
		}
			
		// Add Sorted Renderers to List
		if (SortedRendererTransformList.Count > 0) {
			for (int i = 0; i < SortedRendererTransformList.Count; i++) {
				SortedRendererList.Add (SortedRendererTransformList [i].gameObject.GetComponent<Renderer> ());
			}
		}

		// Store Original Materials
		if (SortedRendererList.Count > 0) {
			for (int i = 0; i < SortedRendererList.Count; i++) {
				OriginalMaterialList.Add (SortedRendererList [i].sharedMaterial);
			}
		}

		ConstructionStarted = AutoStartConstruction;
	}

	private Vector3 GetCenterPointOfStarship(List<Transform> transformListToCalculate) {
		Vector3 sum = Vector3.zero;
		if (transformListToCalculate == null || transformListToCalculate.Count == 0) {
			return sum;
		}

		foreach (Transform trans in transformListToCalculate) {
			sum += trans.position;
		}

		return sum / transformListToCalculate.Count;
	}

	// Update is called once per frame
	void Update () {



		// Construction Started - Build Ship
		if (ConstructionStarted) {

			// Fade In SoundFX
			if (myAudioSource != null) {
				if (myAudioSource.volume < 0.85f) {
					myAudioSource.volume += Time.deltaTime;
				}
			}

			// Do Construction Material Changes and Renderer Enabling
			if (CurrentStep < 5) {
				if (constructionStepIndex < SortedRendererTransformList.Count) {
					if (constructionStepTimer < constructionStepTimerFreq) {
						constructionStepTimer += Time.deltaTime;
					} else {
						// Do Next Construction Step
						if (SortedRendererTransformList.Count > 0) {

							// Construction Sparks
							if (ActiveTypeBeingBuilt == StarshipTypes.Small) {
								if (StarshipPrefabManager.GlobalAccess != null)
									StarshipPrefabManager.GlobalAccess.AddSmallConstructionSparks (SortedRendererTransformList [constructionStepIndex].position);
							} else {
								if (StarshipPrefabManager.GlobalAccess != null)
									StarshipPrefabManager.GlobalAccess.AddConstructionSparks (SortedRendererTransformList [constructionStepIndex].position);
							}

							if (hasNanoBots) {
								ActivateNanobotBeam (SortedRendererList [constructionStepIndex].transform);
							}

							SortedRendererList [constructionStepIndex].enabled = true;

							// Update Construction Material Based On Current Step
							if (CurrentStep == 1) {
								// Step 1
								if (SortedRendererList [constructionStepIndex].sharedMaterial == OriginalMaterials [0]) {
									// BloodTexture01
									SortedRendererList [constructionStepIndex].sharedMaterial = ConstructionMaterials [0];
								} else if (SortedRendererList [constructionStepIndex].sharedMaterial == OriginalMaterials [1]) {
									// BloodTexture01
									SortedRendererList [constructionStepIndex].sharedMaterial = ConstructionMaterials [3];
								} else if (SortedRendererList [constructionStepIndex].sharedMaterial == OriginalMaterials [2]) {
									// BloodTexture01
									SortedRendererList [constructionStepIndex].sharedMaterial = ConstructionMaterials [6];
								} else if (SortedRendererList [constructionStepIndex].sharedMaterial == OriginalMaterials [3]) {
									// BloodTexture01
									SortedRendererList [constructionStepIndex].sharedMaterial = ConstructionMaterials [9];
								} else if (SortedRendererList [constructionStepIndex].sharedMaterial == OriginalMaterials [4]) {
									// BloodTexture01
									SortedRendererList [constructionStepIndex].sharedMaterial = ConstructionMaterials [12];
								} else if (SortedRendererList [constructionStepIndex].sharedMaterial == OriginalMaterials [5]) {
									// BloodTexture01
									SortedRendererList [constructionStepIndex].sharedMaterial = ConstructionMaterials [15];
								} else if (SortedRendererList [constructionStepIndex].sharedMaterial == OriginalMaterials [6]) {
									// BloodTexture01
									SortedRendererList [constructionStepIndex].sharedMaterial = ConstructionMaterials [18];
								}
							} else if (CurrentStep == 2) {
								// Step 2
								if (SortedRendererList [constructionStepIndex].sharedMaterial == ConstructionMaterials [0]) {
									// BloodTexture01
									SortedRendererList [constructionStepIndex].sharedMaterial = ConstructionMaterials [1];
								} else if (SortedRendererList [constructionStepIndex].sharedMaterial == ConstructionMaterials [3]) {
									// BloodTexture01
									SortedRendererList [constructionStepIndex].sharedMaterial = ConstructionMaterials [4];
								} else if (SortedRendererList [constructionStepIndex].sharedMaterial == ConstructionMaterials [6]) {
									// BloodTexture01
									SortedRendererList [constructionStepIndex].sharedMaterial = ConstructionMaterials [7];
								} else if (SortedRendererList [constructionStepIndex].sharedMaterial == ConstructionMaterials [9]) {
									// BloodTexture01
									SortedRendererList [constructionStepIndex].sharedMaterial = ConstructionMaterials [10];
								} else if (SortedRendererList [constructionStepIndex].sharedMaterial == ConstructionMaterials [12]) {
									// BloodTexture01
									SortedRendererList [constructionStepIndex].sharedMaterial = ConstructionMaterials [13];
								} else if (SortedRendererList [constructionStepIndex].sharedMaterial == ConstructionMaterials [15]) {
									// BloodTexture01
									SortedRendererList [constructionStepIndex].sharedMaterial = ConstructionMaterials [16];
								} else if (SortedRendererList [constructionStepIndex].sharedMaterial == ConstructionMaterials [18]) {
									// BloodTexture01
									SortedRendererList [constructionStepIndex].sharedMaterial = ConstructionMaterials [19];
								}
							} else if (CurrentStep == 3) {
								// Step 2
								if (SortedRendererList [constructionStepIndex].sharedMaterial == ConstructionMaterials [1]) {
									// BloodTexture01
									SortedRendererList [constructionStepIndex].sharedMaterial = ConstructionMaterials [2];
								} else if (SortedRendererList [constructionStepIndex].sharedMaterial == ConstructionMaterials [4]) {
									// BloodTexture01
									SortedRendererList [constructionStepIndex].sharedMaterial = ConstructionMaterials [5];
								} else if (SortedRendererList [constructionStepIndex].sharedMaterial == ConstructionMaterials [7]) {
									// BloodTexture01
									SortedRendererList [constructionStepIndex].sharedMaterial = ConstructionMaterials [8];
								} else if (SortedRendererList [constructionStepIndex].sharedMaterial == ConstructionMaterials [10]) {
									// BloodTexture01
									SortedRendererList [constructionStepIndex].sharedMaterial = ConstructionMaterials [11];
								} else if (SortedRendererList [constructionStepIndex].sharedMaterial == ConstructionMaterials [13]) {
									// BloodTexture01
									SortedRendererList [constructionStepIndex].sharedMaterial = ConstructionMaterials [14];
								} else if (SortedRendererList [constructionStepIndex].sharedMaterial == ConstructionMaterials [16]) {
									// BloodTexture01
									SortedRendererList [constructionStepIndex].sharedMaterial = ConstructionMaterials [17];
								} else if (SortedRendererList [constructionStepIndex].sharedMaterial == ConstructionMaterials [19]) {
									// BloodTexture01
									SortedRendererList [constructionStepIndex].sharedMaterial = ConstructionMaterials [20];
								}
							} else if (CurrentStep == 4) {
								// Reset to Original Material
								SortedRendererList [constructionStepIndex].sharedMaterial = OriginalMaterialList [constructionStepIndex];
							}

							constructionStepIndex++;
						}
						constructionStepTimer = 0;
					}
				} else {
					constructionStepIndex = 0;
					CurrentStep++;
				}
			} else {

				// Disable SoundFX
				if (myAudioSource != null) {
					myAudioSource.enabled = false;
				}

				ConstructionComplete = true;
				this.enabled = false;
			}

			if (constructionTimer < ConstructionTime) {
				constructionTimer += Time.deltaTime;
				ConstructionPercent = constructionTimer / ConstructionTime;
			}
			else {
				// Construction Complete

			}

		}

	}

	private void ActivateNanobotBeam(Transform beamFireTarget) {
		bool hasFiredABeam = false;
		for (int i = 0; i < AvailableNanobots.Count; i++) {
			if (!hasFiredABeam) {
				if (!AvailableNanobots [i].NanoBeam01Active) {
					hasFiredABeam = AvailableNanobots [i].FireBeam01 (beamFireTarget);
				} else {
					if (!hasFiredABeam) {
						if (!AvailableNanobots [i].NanoBeam02Active) {
							hasFiredABeam = AvailableNanobots [i].FireBeam02 (beamFireTarget);
						}
					}
				}
			}
		}
	}
}
