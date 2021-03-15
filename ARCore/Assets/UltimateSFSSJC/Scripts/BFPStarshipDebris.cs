using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BFPStarshipDebris : MonoBehaviour {

	public bool LeaveDerilect = true;

	private bool derilectCreated = false;

	private Transform myTransform;
	public float MoveSpeedMIN = 5;
	public float MoveSpeedMAX = 5;
	private float moveSpeed = 0;
	public float TumbleSpeedMIN = 4f;
	public float TumbleSpeedMAX = 7f;
	private float tumbleSpeed = 0f;
	private bool tiltForward = false;
	private bool tiltUp = false;
	private bool tiltRight = false;

	public bool MaterialsSetup = false;
	public Material OriginalTexture01;
	public Material DebrisTexture01;
	public Material OriginalTexture02;
	public Material DebrisTexture02;
	public Material OriginalTexture03;
	public Material DebrisTexture03;
	public Material OriginalTexture04;
	public Material DebrisTexture04;
	public Material OriginalTexture05;
	public Material DebrisTexture05;
	public Material OriginalTexture06;
	public Material DebrisTexture06;
	public Material OriginalTexture07;
	public Material DebrisTexture07;
	public GameObject SmallFlamesPrefab;
	public void SetupMaterials(Transform[] debrisTransforms, GameObject explosionPrefab, GameObject flamesSmokePrefab, Material originalMat01, Material originalMat02, Material originalMat03, Material originalMat04, Material originalMat05, Material originalMat06, Material originalMat07, Material debrisMat01, Material debrisMat02, Material debrisMat03, Material debrisMat04, Material debrisMat05, Material debrisMat06, Material debrisMat07) {
		DebrisSections = debrisTransforms;
		SmallFlamesPrefab = flamesSmokePrefab;
		SpawnOnStartObject = explosionPrefab;
		SpawnOnDeathObject = explosionPrefab;
		SectionExplosion = explosionPrefab;

		// Collect Renderers
		debrisRenderers = new List<Renderer>();
		derilectSectionList = new List<Transform> ();
		if (DebrisSections != null) {
			if (DebrisSections.Length > 0) {
				debrisSectionList = new List<Transform> ();
				for (int i = 0; i < DebrisSections.Length; i++) {
					debrisSectionList.Add (DebrisSections [i]);
					debrisRenderers.Add( DebrisSections [i].gameObject.GetComponent<Renderer> ());
				}
			} 
		}

		OriginalTexture01 = originalMat01;
		OriginalTexture02 = originalMat02;
		OriginalTexture03 = originalMat03;
		OriginalTexture04 = originalMat04;
		OriginalTexture05 = originalMat05;
		OriginalTexture06 = originalMat06;
		OriginalTexture07 = originalMat07;
		DebrisTexture01 = debrisMat01;
		DebrisTexture02 = debrisMat02;
		DebrisTexture03 = debrisMat03;
		DebrisTexture04 = debrisMat04;
		DebrisTexture05 = debrisMat05;
		DebrisTexture06 = debrisMat06;
		DebrisTexture07 = debrisMat07;

		// Setup Debris Explosion
		DebrisSectionDestroyFreqMIN = 0.15f;
		DebrisSectionDestroyFreqMAX = 0.25f;
		MoveSpeedMIN = 0.1f;
		MoveSpeedMAX = 0.2f;
		TumbleSpeedMIN = 0.2f;
		TumbleSpeedMIN = 0.25f;
		LeaveDerilect = false;

		MaterialsSetup = true;
	}

	public void SetupDebrisSection() {
		DebrisSectionDestroyFreqMIN = 0.075f;
		DebrisSectionDestroyFreqMAX = 0.125f;
		MoveSpeedMIN = 1.0f;
		MoveSpeedMAX = 1.5f;
		TumbleSpeedMIN = 0.5f;
		TumbleSpeedMIN = 0.75f;
	}

	public int RemainingItems = 0;
	public Transform[] DebrisSections;
	private List<Renderer> debrisRenderers;
	private List<Transform> debrisSectionList;
	private List<Transform> derilectSectionList;
	public Transform MainDebrisSection;
	private float debrisSectionDestroyTimer = 0;
	public float DebrisSectionDestroyFreqMIN = 1.0f;
	public float DebrisSectionDestroyFreqMAX = 2.0f;
	private float debrisSectionDestroyFreq = 1.0f;

	public ParticleSystem SmokePSystem;
	public ParticleSystem FlamesPSystem;
	public GameObject SpawnOnDeathObject;
	public GameObject SpawnOnStartObject;

	// Use this for initialization
	void Start () {
		myTransform = gameObject.transform;

		// Randomize Tumble and Speed
		moveSpeed = Random.Range(MoveSpeedMIN, MoveSpeedMAX);
		tumbleSpeed = Random.Range(TumbleSpeedMIN, TumbleSpeedMAX);
		if (Random.Range (0, 100) < 50)
			tiltForward = true;
		if (Random.Range (0, 100) < 50)
			tiltUp = true;
		if (Random.Range (0, 100) < 50)
			tiltRight = true;
	}

	private bool spawnedOnStart = false;
	private bool finishedDestructions = false;
	// Update is called once per frame
	void Update () {
		if (MaterialsSetup) {
			if (!spawnedOnStart) {
				if (SpawnOnStartObject != null) {
					Debug.Log ("Spawning Explosion Effect...");
					GameObject newESEffect = GameObject.Instantiate (SpawnOnStartObject, transform.position, transform.rotation) as GameObject;
					newESEffect.name = "ExplosionEffect";
				}
				spawnedOnStart = true;
			}
			if (!finishedDestructions) {
				if (!derilectCreated) {
					if (debrisSectionList != null) {
						if (debrisSectionList.Count > 0) {
							// Destroy Debris Sections
							if (debrisSectionDestroyTimer < debrisSectionDestroyFreq) {
								debrisSectionDestroyTimer += Time.deltaTime;
							} else {
								RemainingItems = 0;
								for (int i = 0; i < debrisSectionList.Count; i++) {
									if (debrisSectionList [i] != null)
										RemainingItems++;
								}
								if (RemainingItems == 0) {
									finishedDestructions = true;
								}

								int randomDebrisSection = Random.Range (0, debrisSectionList.Count);
								if (debrisSectionList [randomDebrisSection] == null) {
									// Make Sure It's Never Null
									for (int i = 0; i < debrisSectionList.Count; i++) {
										if (debrisSectionList [i] != null)
											randomDebrisSection = i;
									}								
								}
								if (debrisSectionList [randomDebrisSection] != null) {
									// Change Material
									if (debrisRenderers [randomDebrisSection].sharedMaterial == OriginalTexture01)
										debrisRenderers [randomDebrisSection].sharedMaterial = DebrisTexture01;
									if (debrisRenderers [randomDebrisSection].sharedMaterial == OriginalTexture02)
										debrisRenderers [randomDebrisSection].sharedMaterial = DebrisTexture02;
									if (debrisRenderers [randomDebrisSection].sharedMaterial == OriginalTexture03)
										debrisRenderers [randomDebrisSection].sharedMaterial = DebrisTexture03;
									if (debrisRenderers [randomDebrisSection].sharedMaterial == OriginalTexture04)
										debrisRenderers [randomDebrisSection].sharedMaterial = DebrisTexture04;
									if (debrisRenderers [randomDebrisSection].sharedMaterial == OriginalTexture05)
										debrisRenderers [randomDebrisSection].sharedMaterial = DebrisTexture05;
									if (debrisRenderers [randomDebrisSection].sharedMaterial == OriginalTexture06)
										debrisRenderers [randomDebrisSection].sharedMaterial = DebrisTexture06;
									if (debrisRenderers [randomDebrisSection].sharedMaterial == OriginalTexture07)
										debrisRenderers [randomDebrisSection].sharedMaterial = DebrisTexture07;
									// Unhitch Debris Section
									if (!LeaveDerilect)
										debrisSectionList [randomDebrisSection].parent = null;
									BFPStarshipDebris debrisSectionScript = debrisSectionList [randomDebrisSection].gameObject.AddComponent<BFPStarshipDebris> ();
									GameObject newESEffect = GameObject.Instantiate (SectionExplosion, debrisSectionList [randomDebrisSection].transform.position, debrisSectionList [randomDebrisSection].transform.rotation) as GameObject;
									newESEffect.name = "ExplosionEffect";
									//newESEffect.transform.parent = debrisSectionList [randomDebrisSection].transform;
									debrisSectionScript.MaterialsSetup = true;
									debrisSectionScript.SpawnOnDeathObject = SpawnOnDeathObject;
									debrisSectionScript.SetupDebrisSection ();
									if (!LeaveDerilect)
										Destroy (debrisSectionList [randomDebrisSection].gameObject, Random.Range (1, 2));
									if (LeaveDerilect) {
										SpawnExplosionAtSection (debrisSectionList [randomDebrisSection].position);
										derilectSectionList.Add (debrisSectionList [randomDebrisSection]);
									}
									debrisSectionList.RemoveAt (randomDebrisSection);
									debrisRenderers.RemoveAt (randomDebrisSection);
								}
								// set new freq
								debrisSectionDestroyFreq = Random.Range (DebrisSectionDestroyFreqMIN, DebrisSectionDestroyFreqMAX);
								debrisSectionDestroyTimer = 0;
							}
						} else {
							// Final Debris Remaining
							if (SmokePSystem != null) {		
								ParticleSystem.MainModule newMainModule = SmokePSystem.main;
								newMainModule.loop = false;
								SmokePSystem.transform.parent = null;
								Destroy (SmokePSystem.gameObject, 6);
							}
							if (FlamesPSystem != null) {
								ParticleSystem.MainModule newMainModule = SmokePSystem.main;
								newMainModule.loop = false;
								FlamesPSystem.transform.parent = null;
								Destroy (FlamesPSystem.gameObject, 6);
							}
							if (SpawnOnDeathObject != null) {
								GameObject newESEffect = GameObject.Instantiate (SpawnOnDeathObject, transform.position, transform.rotation) as GameObject;
								newESEffect.name = "ExplosionEffect";
							}
							derilectCreated = true;
							if (!LeaveDerilect)
								Destroy (gameObject);
						}
					}
				}
			} else {
				Destroy (gameObject);
			}

			myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
			if (tiltUp)
				myTransform.RotateAround (myTransform.position, myTransform.up, tumbleSpeed * Time.deltaTime);
			else
				myTransform.RotateAround (myTransform.position, -myTransform.up, tumbleSpeed * Time.deltaTime);

			if (tiltForward)
				myTransform.RotateAround (myTransform.position, myTransform.forward, tumbleSpeed * Time.deltaTime);
			else
				myTransform.RotateAround (myTransform.position, -myTransform.forward, tumbleSpeed * Time.deltaTime);

			if (tiltRight)
				myTransform.RotateAround (myTransform.position, myTransform.right, tumbleSpeed * Time.deltaTime);
			else
				myTransform.RotateAround (myTransform.position, -myTransform.right, tumbleSpeed * Time.deltaTime);
		}
	}

	public GameObject SectionExplosion;

	private void SpawnExplosionAtSection(Vector3 positionToSpawnAt) {
		if (SectionExplosion != null) {
			GameObject newESEffect = GameObject.Instantiate (SectionExplosion, positionToSpawnAt, Quaternion.identity) as GameObject;
			newESEffect.name = "ExplosionEffect";
		}
	}
}
