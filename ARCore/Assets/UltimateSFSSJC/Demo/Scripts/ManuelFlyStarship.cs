using UnityEngine;
using System.Collections;

public class ManuelFlyStarship : MonoBehaviour {

	private Transform myTransform;

	public float FlyForwardSpeed = 5;
	public float FlySidewaysSpeed = 5;
	public float RotationSpeed = 2.5f;

	// Use this for initialization
	void Start () {
		myTransform = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.UpArrow)) {
			myTransform.RotateAround (myTransform.position, myTransform.right, RotationSpeed * 2 * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			myTransform.RotateAround (myTransform.position, -myTransform.right, RotationSpeed * 2 * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			myTransform.RotateAround (myTransform.position, -myTransform.up, RotationSpeed * 2 * Time.deltaTime);
			//myTransform.position -= myTransform.right * FlyForwardSpeed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			myTransform.RotateAround (myTransform.position, myTransform.up, RotationSpeed * 2 * Time.deltaTime);
			//myTransform.position += myTransform.right * FlyForwardSpeed * Time.deltaTime;
		}

		myTransform.position += myTransform.forward * FlyForwardSpeed * Time.deltaTime;
	}
}
