using UnityEngine;
using System.Collections;

public class BFPWarpOutTimed : MonoBehaviour {

	private Transform myTransform;

	// Use this for initialization
	void Start () {
		myTransform = gameObject.transform;
	}

	private float lifeTimer = 0;
	private float WarpOutTime = 4.0f;
	private float WarpSpeed = 2;

	// Update is called once per frame
	void Update () {
		lifeTimer += Time.deltaTime;
		if (lifeTimer > WarpOutTime) {
			WarpSpeed += 10 * Time.deltaTime;
			myTransform.position += myTransform.forward * WarpSpeed * Time.deltaTime;
			if (myTransform.position.magnitude > 500) {
				Destroy (gameObject);
			}
		}
	}
}
