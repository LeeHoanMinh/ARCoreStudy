using UnityEngine;
using System.Collections;

public class BFPSpawnOnDestroy : MonoBehaviour {

	public GameObject SpawnObject;

	private bool isQuitting = false;
	void OnApplicationQuit() {
		isQuitting = true;
	}

	void OnDestroy () {
		if (!isQuitting) {
			GameObject newESEffect = GameObject.Instantiate (SpawnObject, transform.position, transform.rotation) as GameObject;
			newESEffect.name = "DestructionObject";
		}
	}
}
