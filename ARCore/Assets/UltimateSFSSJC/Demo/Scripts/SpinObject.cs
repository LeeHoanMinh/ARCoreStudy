using UnityEngine;
using System.Collections;

public class SpinObject : MonoBehaviour {

	private Transform myTransform;

	public bool DoSpin = true;
	public float SpinSpeed = 5.0f;

	public bool RandomizeSpinSpeed = false;

	// Use this for initialization
	void Start () {
		myTransform = gameObject.transform;
		if (RandomizeSpinSpeed) {
			if (Random.Range (0, 100) < 50) {
				SpinSpeed = Random.Range (1, 5);
			} else {
				SpinSpeed = Random.Range (-5, -1);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (DoSpin) {
			myTransform.RotateAround (myTransform.position, Vector3.up, SpinSpeed * Time.deltaTime);
		}
	}
}
