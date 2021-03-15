using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuDemo : MonoBehaviour {

	public DemoController DemoControlScript;

	public bool IsShowoffController = false;

	private float spawnTimer = 0;
	private float spawnTimerFreq = 5.0f;

	private List<GameObject> spawnedStarships;

	public DemoWeaponLauncher[] WeaponLaunchers;

	// Use this for initialization
	void Start () {
		spawnedStarships = new List<GameObject> ();
	}

	private Ray ray;
	private RaycastHit rayHit;
	private void DoStarshipMouseSelection() {
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out rayHit, 500)) {
			if (rayHit.collider.gameObject.name != null) {
				Debug.Log ("Clicked on: " + rayHit.collider.gameObject.name);
				if (WeaponLaunchers.Length > 0) {
					for (int i = 0; i < WeaponLaunchers.Length; i++) {
						WeaponLaunchers [i].FireWeapon (rayHit.collider.gameObject.transform);
					}
				}
			}
		} 
	}

	// Update is called once per frame
	void Update () {

		// Do Mouse Firing
		if (Input.GetMouseButtonUp (0)) {
			DoStarshipMouseSelection ();
		}

		// Random Spawning
		if (!IsShowoffController) {
			if (spawnTimer < spawnTimerFreq) {
				spawnTimer += Time.deltaTime;
			} else {

				// Spawn Starship
				if (DemoControlScript != null) {
					StarshipTypes typeToSpawn = StarshipTypes.Small;
					float randomTypeNum = Random.Range (0, 100);
					if (randomTypeNum < 25)
						typeToSpawn = StarshipTypes.Small;
					else if (randomTypeNum >= 25 && randomTypeNum < 50)
						typeToSpawn = StarshipTypes.Medium;
					else if (randomTypeNum >= 50 && randomTypeNum < 75)
						typeToSpawn = StarshipTypes.Large;
					else if (randomTypeNum >= 75)
						typeToSpawn = StarshipTypes.Carrier;

					ColorSchemeTypes colorToUse = ColorSchemeTypes.Red;
					float randomColorNum = Random.Range (0, 100);
					if (randomColorNum < 10)
						colorToUse = ColorSchemeTypes.Red;
					else if (randomColorNum >= 10 && randomColorNum < 20)
						colorToUse = ColorSchemeTypes.Gray;
					else if (randomColorNum >= 20 && randomColorNum < 45)
						colorToUse = ColorSchemeTypes.Green;
					else if (randomColorNum >= 45 && randomColorNum < 65)
						colorToUse = ColorSchemeTypes.Purple;
					else if (randomColorNum >= 65 && randomColorNum < 75)
						colorToUse = ColorSchemeTypes.Blue;
					else if (randomColorNum >= 75)
						colorToUse = ColorSchemeTypes.Teal;

					int indexToUse = 0;
					switch (typeToSpawn) {
					case StarshipTypes.Small:
						indexToUse = Random.Range (0, 5);
						break;
					case StarshipTypes.Medium:
						indexToUse = Random.Range (0, 4);
						break;
					case StarshipTypes.Large:
						indexToUse = Random.Range (0, 4);
						break;
					case StarshipTypes.Carrier:
						indexToUse = Random.Range (0, 4);
						break;
					default:
					
						break;
					}

					GameObject newStarship = DemoControlScript.SpawnMainMenuShip (transform, typeToSpawn, indexToUse, colorToUse);
					newStarship.transform.position = new Vector3 (Random.Range (-30, 30), Random.Range (-35, 35), Random.Range (-160, -120));
					newStarship.GetComponent<StarshipMovement> ().FlyForward = true;
					newStarship.GetComponent<StarshipMovement> ().Speed = Random.Range (10, 20);
					spawnedStarships.Add (newStarship);
				}

				spawnTimer = 0;
			}
		}
	}
}
