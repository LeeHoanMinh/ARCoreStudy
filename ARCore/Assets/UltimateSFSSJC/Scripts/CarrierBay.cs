using UnityEngine;
using System.Collections;

public class CarrierBay : MonoBehaviour {

	public Transform StarshipSpawnPoint;

	public Transform BayDoorRightTransform;
	public Transform BayDoorLeftTransform;

	public bool OpenBayDoors = false;
	public bool CloseBayDoors = false;
	public bool DoorsOpening = false;
	public bool DoorsClosing = false;
	private float doorOpenTimer = 0;
	private float doorOpenTimerFreq = 2.5f;
	public float OpeningSpeed = 35;
	public bool BayDoorsOpen = false;

	private Vector3 leftDoorLocalEulers;
	private Vector3 rightDoorLocalEulers;

	// Use this for initialization
	void Start () {
		leftDoorLocalEulers = Vector3.zero;
		rightDoorLocalEulers = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		// Trigger Opening Bay Doors
		if (OpenBayDoors && !DoorsOpening && !BayDoorsOpen && !DoorsClosing) {
			// Start Bay Doors Opening
			DoorsOpening = true;
			OpenBayDoors = false;
		}
		// Trigger Closing Bay Doors
		if (CloseBayDoors && !DoorsOpening && BayDoorsOpen && !DoorsClosing) {
			// Start Bay Doors Opening
			DoorsClosing = true;
			CloseBayDoors = false;
		}

		// Open Doors
		if (DoorsOpening) {
			if (doorOpenTimer < doorOpenTimerFreq) {
				doorOpenTimer += Time.deltaTime;

				if (BayDoorLeftTransform != null) {
					leftDoorLocalEulers += new Vector3 (0, -(OpeningSpeed * Time.deltaTime), 0);
					BayDoorLeftTransform.localRotation = Quaternion.Euler (leftDoorLocalEulers);
				}
				if (BayDoorRightTransform != null) {
					rightDoorLocalEulers += new Vector3 (0, OpeningSpeed * Time.deltaTime, 0);
					BayDoorRightTransform.localRotation = Quaternion.Euler (rightDoorLocalEulers);
				}
			}
			else {
				// Door Opening Complete
				doorOpenTimer = 0;
				DoorsOpening = false;
				BayDoorsOpen = true;
			}
		}

		// Close Doors
		if (DoorsClosing) {
			if (doorOpenTimer < doorOpenTimerFreq) {
				doorOpenTimer += Time.deltaTime;

				if (BayDoorLeftTransform != null) {
					leftDoorLocalEulers -= new Vector3 (0, -(OpeningSpeed * Time.deltaTime), 0);
					BayDoorLeftTransform.localRotation = Quaternion.Euler (leftDoorLocalEulers);
				}
				if (BayDoorRightTransform != null) {
					rightDoorLocalEulers -= new Vector3 (0, OpeningSpeed * Time.deltaTime, 0);
					BayDoorRightTransform.localRotation = Quaternion.Euler (rightDoorLocalEulers);
				}
			}
			else {
				// Door Closing Complete
				doorOpenTimer = 0;
				DoorsClosing = false;
				BayDoorsOpen = false;
			}
		}
	}
}
