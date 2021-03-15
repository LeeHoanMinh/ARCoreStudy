using UnityEngine;
using System.Collections;

public enum WeaponTypes {
	Rocket,
	Torpedo
}

public class DemoWeaponLauncher : MonoBehaviour {
	public static DemoWeaponLauncher GlobalAccess;
	void Awake () {
		GlobalAccess = this;
	}

	public bool CyclePosition = true;

	public WeaponTypes WeaponType = WeaponTypes.Rocket;

	public GameObject RocketPrefab;
	public GameObject TorpedoPrefab;

	private Transform myTransform;
	void Start () {
		myTransform = gameObject.transform;
		if (CyclePosition)
			myTransform.position = new Vector3 (myTransform.position.x, 100, myTransform.position.z);
	}

	private void CycleWeaponLauncherPos() {
		if (myTransform.position.y > 0) {
			myTransform.position = new Vector3 (myTransform.position.x, -100, myTransform.position.z);
		} else {
			myTransform.position = new Vector3 (myTransform.position.x, 100, myTransform.position.z);
		}
	}

	public void FireWeapon(Transform targetIn) {
		if (WeaponType == WeaponTypes.Rocket) {
			FireRocket (targetIn);
		} else if (WeaponType == WeaponTypes.Torpedo) {
			FireTorpedo (targetIn);
		}
	}

	public void FireTorpedo(Transform targetIn) {
		// Select Random Transform in Target
		Transform[] targetTransforms = targetIn.gameObject.GetComponentsInChildren<Transform>();
		Transform targetToUse = targetIn;
		if (targetTransforms.Length > 0) {
			int randomTransform = Random.Range (0, targetTransforms.Length);
			targetToUse = targetTransforms [randomTransform];
		}

		Debug.Log ("Firing Torpedo at " + targetIn.gameObject.name);
		GameObject newRocket = GameObject.Instantiate (TorpedoPrefab, myTransform.position, myTransform.rotation) as GameObject;
		newRocket.name = "Torpedo_0001";
		BFPRocket rocketScript = newRocket.GetComponent<BFPRocket> ();
		if (rocketScript != null) {
			rocketScript.Fire (this.gameObject, targetToUse, true, myTransform.position, 4, 10);
		}

		// Cycle Weapon Launcher Position
		if (CyclePosition)
			CycleWeaponLauncherPos();
	}

	public void FireRocket(Transform targetIn) {
		// Select Random Transform in Target
		Transform[] targetTransforms = targetIn.gameObject.GetComponentsInChildren<Transform>();
		Transform targetToUse = targetIn;
		if (targetTransforms.Length > 0) {
			int randomTransform = Random.Range (0, targetTransforms.Length);
			targetToUse = targetTransforms [randomTransform];
		}

		Debug.Log ("Firing Rocket at " + targetIn.gameObject.name);
		GameObject newRocket = GameObject.Instantiate (RocketPrefab, myTransform.position, myTransform.rotation) as GameObject;
		newRocket.name = "Rocket_0001";
		BFPRocket rocketScript = newRocket.GetComponent<BFPRocket> ();
		if (rocketScript != null) {
			rocketScript.Fire (this.gameObject, targetToUse, true, myTransform.position, 4, 10);
		}

		// Cycle Weapon Launcher Position
		if (CyclePosition)
			CycleWeaponLauncherPos();
	}
}
