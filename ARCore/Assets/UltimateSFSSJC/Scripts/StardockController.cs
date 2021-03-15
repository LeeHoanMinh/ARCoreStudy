using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StardockController : MonoBehaviour {

	public bool IsDemoStarDock = false;
	private Transform myTransform;

	[Space(10)]
	public bool TestBuildStarship = false;
	public bool DestroyBuiltStarship = false;
	public StarshipTypes TypeToBuild = StarshipTypes.Small;
	public ColorSchemeTypes ColorToBuild = ColorSchemeTypes.Red;
	public int ShipToBuildIndex = 0;
	[Space(10)]
	public GameObject ShipToBuildPrefab;
	[Space(10)]
	public bool ShipInStarDock = false;
	public GameObject CurrentShipBeingBuilt;
	public ConstructStarship CurrentShipConstructScript;

	public AudioClip ConstructionLoop01;
	private AudioSource myAudioSource;

	public Light[] StarDockLights;

	// Use this for initialization
	void Start () {
		myTransform = gameObject.transform;
		if (ConstructionLoop01 != null) {
			myAudioSource = gameObject.AddComponent<AudioSource> ();
			myAudioSource.spatialBlend = 0.85f;
			myAudioSource.playOnAwake = false;
			myAudioSource.Stop ();
			myAudioSource.volume = 0;
			myAudioSource.loop = true;
			myAudioSource.clip = ConstructionLoop01;
		}
		if (StarDockLights.Length > 0) {
			for (int i = 0; i < StarDockLights.Length; i++) {
				StarDockLights [i].intensity = 0;
			}
		}
	}

	public void DestroyInConstructionStarship() {
		if (CurrentShipConstructScript != null) {
			Destroy(CurrentShipConstructScript.gameObject.transform.root.gameObject);
			CurrentShipConstructScript = null;
			if (myAudioSource.isPlaying)
				myAudioSource.Stop ();
			ShipInStarDock = false;
		}
	}

	// Update is called once per frame
	void Update () {

		// Test Building Starship
		if (TestBuildStarship) {
			if (!ShipInStarDock) {
				BuildStarship (TypeToBuild, ColorToBuild, ShipToBuildIndex);
				TestBuildStarship = false;
			}
		}

		// Manage Current Starship
		if (ShipInStarDock) {
			// Play Construction Sounds
			if (myAudioSource != null) {
				if (myAudioSource.volume < 0.85f)
					myAudioSource.volume += Time.deltaTime;
				if (!myAudioSource.isPlaying)
					myAudioSource.Play ();
			}

			if (StarDockLights.Length > 0) {
				for (int i = 0; i < StarDockLights.Length; i++) {
					if (StarDockLights [i].intensity < 3)
						StarDockLights [i].intensity += Time.deltaTime;
				}
			}

			if (CurrentShipConstructScript != null) {
				if (CurrentShipConstructScript.ConstructionComplete) {
					// Remove Starship From StarDock
					if (IsDemoStarDock) {
						// Demo Ships Move Forward After Construction
						if (DestroyBuiltStarship) {
							CurrentShipBeingBuilt.transform.root.gameObject.AddComponent<BFPWarpOutTimed> ();
						}
						StarshipMovement moveScript = CurrentShipBeingBuilt.AddComponent<StarshipMovement> ();
						moveScript.Speed = 3.5f;
						moveScript.FlyForward = true;
					}
					CurrentShipBeingBuilt = null;
					CurrentShipConstructScript = null;
					ShipInStarDock = false;
				}
			}
		} else {
			// Stop Construction Sounds
			if (myAudioSource != null) {
				if (myAudioSource.isPlaying) {
					if (myAudioSource.volume > 0f)
						myAudioSource.volume -= Time.deltaTime;
					if (myAudioSource.volume <= 0)
						myAudioSource.Stop ();
				}
			}

			if (StarDockLights.Length > 0) {
				for (int i = 0; i < StarDockLights.Length; i++) {
					if (StarDockLights [i].intensity > 0)
						StarDockLights [i].intensity -= Time.deltaTime;
					else
						StarDockLights [i].intensity = 0;
				}
			}
		}
	}

	private void BuildStarship(StarshipTypes typeToBuild, ColorSchemeTypes coloringToUse, int prefabIndex) {

		if (StarshipPrefabManager.GlobalAccess != null) {
			ShipToBuildPrefab = StarshipPrefabManager.GlobalAccess.GetStarshipPrefab (typeToBuild, coloringToUse, prefabIndex);
		}

		GameObject shipBuiltGO = GameObject.Instantiate (ShipToBuildPrefab, myTransform.position, myTransform.rotation) as GameObject;
		shipBuiltGO.name = ShipToBuildPrefab.name + "_CONSTRUCTING";

		// Add Construction Script and Initialize
		ConstructStarship constructScript = shipBuiltGO.AddComponent<ConstructStarship>();
		constructScript.ConstructOnStart = true;
		constructScript.SetupConstruction (1.0f, typeToBuild, coloringToUse);

		CurrentShipConstructScript = constructScript;
		CurrentShipBeingBuilt = shipBuiltGO;
		ShipInStarDock = true;

	}
}
