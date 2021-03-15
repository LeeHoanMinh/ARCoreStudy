using UnityEngine;
using System.Collections;

public class ComponentViewer : MonoBehaviour {

	public int CurrentComponentIndex = 0;

	public Transform CameraTargetTransform;
	public Transform CameraTransform;
	public Transform ComponentPlaneTransform;

	public GameObject[] ComponentPrefabs;

	private Transform myTransform;

	public GameObject CurrentDisplayedComponent;

	// Use this for initialization
	void Start () {
		myTransform = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (CurrentDisplayedComponent == null) {
			SpawnComponent ();
		}
	}

	public void NextComponent() {
		if (CurrentComponentIndex < ComponentPrefabs.Length - 1) {
			CurrentComponentIndex++;
		} else {
			CurrentComponentIndex = 0;
		}

		SpawnComponent ();
	}

	public void PreviousComponent() {
		if (CurrentComponentIndex > 0) {
			CurrentComponentIndex--;
		} else {
			CurrentComponentIndex = ComponentPrefabs.Length - 1;
		}

		SpawnComponent ();
	}

	private void SpawnComponent() {
		if (CurrentDisplayedComponent != null) {
			Destroy (CurrentDisplayedComponent);
		}

		// Spawn Selected Component
		GameObject newComponent = GameObject.Instantiate(ComponentPrefabs[CurrentComponentIndex], myTransform.position, myTransform.rotation) as GameObject;
		newComponent.name = ComponentPrefabs [CurrentComponentIndex].name;
		CurrentDisplayedComponent = newComponent;
		newComponent.AddComponent<SpinObject> ();

		Renderer componentRenderer = newComponent.GetComponent<Renderer>();

		float boundZDistance = componentRenderer.bounds.max.z * 4;
		float boundYDistance = componentRenderer.bounds.max.y * 5;
//		Debug.Log ("Bounds Z Distance: " + boundZDistance.ToString());
//		Debug.Log ("Bounds Y Distance: " + boundYDistance.ToString());
		if (ComponentPlaneTransform != null) {
			ComponentPlaneTransform.position = new Vector3 (0, -boundYDistance, 0);
		}
		if (CameraTargetTransform != null) {
			CameraTargetTransform.position = componentRenderer.bounds.center;
		}
		if (CameraTransform != null) {
			CameraTransform.localPosition = new Vector3 (0, boundZDistance / 2, boundZDistance);
		}
	}
}
