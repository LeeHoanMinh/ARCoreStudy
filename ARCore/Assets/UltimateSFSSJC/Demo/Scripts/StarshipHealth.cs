using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StarshipHealth : MonoBehaviour {

	public bool Alive = true;
	public bool MainMenuShip = false;
	public float Health = 100;
	public float HealthMAX = 100;

	private GenerateDebrisOnDestroy debrisScript;

	// Use this for initialization
	void Start () {
		debrisScript = gameObject.GetComponent<GenerateDebrisOnDestroy> ();
		if (debrisScript == null)
			debrisScript = gameObject.GetComponentInChildren<GenerateDebrisOnDestroy> ();

		Health = HealthMAX;
		Alive = true;

		if (SceneManager.GetActiveScene ().buildIndex == 0) {
			// Starship Spawned on Demo Main Menu
			MainMenuShip = true;
		}
	}

	public void SetupStarshipHealth(float healthMax) {
		HealthMAX = healthMax;
		Health = HealthMAX;
		Alive = true;
	}

	// Update is called once per frame
	void Update () {
		if (MainMenuShip) {
			if (transform.position.z > 120) {
				Destroy (gameObject);
			}
		}

		if (!Alive) {
			// Destroy Starship
			if (debrisScript != null) {
				if (!debrisScript.DebrisModelGenerated) {
					debrisScript.DestroyStarship ();
					this.enabled = false;
				}
			}
		}
	}

	void Damage(float damageIn) {
		//Debug.Log ("Ship Taking Damage...");
		TakeDamage (damageIn);
	}

	private void TakeDamage(float damageIn) {
		Health -= damageIn;
		if (Health < 0) {
			Health = 0;
			Alive = false;
		}
	}
}
