using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum StarshipDebrisTypes {
	Carrier,
	Large,
	Medium,
	Small
}

public enum StarshipDebrisColors {
	Red,
	Gray,
	Blue,
	Green,
	Purple,
	Teal
}

public class GenerateDebrisOnDestroy : MonoBehaviour {

	public bool SelfDestruct = false;

	public StarshipTypes StarshipType = StarshipTypes.Small;
	public StarshipDebrisColors StarshipColoring = StarshipDebrisColors.Gray;

	public Renderer MainRenderer;
	public Transform[] DebrisSectionTransforms;

	public MeshCollider[] MeshCollidersToDisable;
	private CarrierManager carrierManagerScript;
	private CarrierBay[] carrierBayScripts;

	public bool DebrisModelGenerated = false;

	public bool InitialSetupComplete = false;

	private Transform myTransform;

	void Start() {
		myTransform = gameObject.transform;
		carrierBayScripts = gameObject.GetComponentsInChildren<CarrierBay> ();
		carrierManagerScript = gameObject.GetComponent<CarrierManager>();
		MeshCollidersToDisable = gameObject.GetComponentsInChildren<MeshCollider> ();
		MainRenderer = gameObject.GetComponent<Renderer> ();
		Renderer[] starshipRenderers = gameObject.GetComponentsInChildren<Renderer> ();
		if (starshipRenderers.Length > 0) {
			DebrisSectionTransforms = new Transform[starshipRenderers.Length];
			for (int i = 0; i < starshipRenderers.Length; i++) {
				DebrisSectionTransforms[i] = starshipRenderers [i].transform;
			}
		}
		fallBackOrForwardSpeed = Random.Range(0.5f, 0.75f);
		InitialSetupComplete = true;
	}

	private float fallBackOrForwardSpeed = 0;

	void Update() {
		if (Application.isEditor) {
			if (Input.GetKeyUp (KeyCode.F5)) {
				SelfDestruct = true;
			}
		}

		if (SelfDestruct) {
			DestroyStarship ();
			SelfDestruct = false;
		}
		if (DebrisModelGenerated) {
			// Fall Back as Destroyed
			myTransform.position -= (Vector3.forward * fallBackOrForwardSpeed * Time.deltaTime);
		}
	}

	public void DestroyStarship() {
		// Generate Debris Model and Destroy!
		GenerateDebrisModel ();
	}

	public void GenerateDebrisModel() {
		if (transform.root != this.transform) {
			GameObject parentGameobject = transform.root.gameObject;
			transform.parent = null;
			Destroy (parentGameobject);
		}

		if (!InitialSetupComplete) {
			carrierBayScripts = gameObject.GetComponentsInChildren<CarrierBay> ();
			carrierManagerScript = gameObject.GetComponent<CarrierManager>();
			MeshCollidersToDisable = gameObject.GetComponentsInChildren<MeshCollider> ();
			MainRenderer = gameObject.GetComponent<Renderer> ();
			Renderer[] starshipRenderers = gameObject.GetComponentsInChildren<Renderer> ();
			if (starshipRenderers.Length > 0) {
				DebrisSectionTransforms = new Transform[starshipRenderers.Length];
				for (int i = 0; i < starshipRenderers.Length; i++) {
					DebrisSectionTransforms[i] = starshipRenderers [i].transform;
				}
			}
		}

		StarshipMovement moveScript = gameObject.GetComponent<StarshipMovement> ();
		if (moveScript != null)
			moveScript.enabled = false;

		// Disable Carrier Scripts
		if (carrierManagerScript != null)
			carrierManagerScript.enabled = false;
		if (carrierBayScripts != null) {
			if (carrierBayScripts.Length > 0) {
				for (int i = 0; i < carrierBayScripts.Length; i++) {
					carrierBayScripts [i].enabled = false;
				}
			}
		}

		// Disable All Mesh Colliders
		if (MeshCollidersToDisable.Length > 0) {
			for (int i = 0; i < MeshCollidersToDisable.Length; i++) {
				if (MeshCollidersToDisable [i] != null)
					MeshCollidersToDisable [i].enabled = false;
			}
		}

		// Add Starship Debris Script
		BFPStarshipDebris debrisScript = gameObject.AddComponent<BFPStarshipDebris>();

		if (StarshipDestructionManager.GlobalAccess != null) {
			StarshipDestructionManager.GlobalAccess.GetDebrisMaterials (StarshipType, StarshipColoring, debrisScript, DebrisSectionTransforms);
		}

		DebrisModelGenerated = true;
	}
}
