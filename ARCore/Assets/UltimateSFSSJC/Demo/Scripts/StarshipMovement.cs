using UnityEngine;
using System.Collections;

public class StarshipMovement : MonoBehaviour {

	public bool Setup = false;
	private Transform myTransform;
	public bool FlyErratically = false;
	public bool FlyForward = false;
	public bool FlyBackward = false;
	public bool FlyDown = false;
	public bool FlyLeft = false;
	public bool FlyRight = false;
	public float Speed = 5;
	public float SidewaysSpeed = 5;
	private Transform LocalRotationEulers;
	private Vector3 currentLocRotationEulers;
	private float zRotation = 0;

	// Use this for initialization
	void Start () {
		myTransform = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (Setup) {
			DoErraticMovement ();
		}
		if (FlyForward) {
			MoveForward ();
		}
		if (FlyBackward) {
			MoveBackward ();
		}
		if (FlyDown) {
			MoveDownward ();
		}
		if (FlyLeft) {
			MoveLeft ();
		}
		if (FlyRight) {
			MoveRight ();
		}
	}

	public void SetupMovement(Transform rotationEulerTransform, float erraticChangeFreqIn, float errratRotAddSpeedIn, float erratRotSpeedIn, float erratMoveSpdIn) {
		LocalRotationEulers = rotationEulerTransform;

		// Setup Erratic Motion
		erraticChangeTimerFreq = erraticChangeFreqIn;
		erraticRotationAddSpeed = errratRotAddSpeedIn;
		erraticRotationSpeed = erratRotSpeedIn;
		erraticMoveSpeed = erratMoveSpdIn;

		FlyErratically = true;
		Setup = true;
	}

	private float erraticChangeTimer = 0;
	private float erraticChangeTimerFreq = 1.0f;
	private bool erraticLeft = true;

	private float erraticRotationAddSpeed = 5.5f;
	private float erraticRotationSpeed = 20f;
	private float erraticMoveSpeed = 0.2f;

	private float erraticPauseTimer = 0;
	private float erraticPauseTimerFreq = 1.0f;

	private float erraticLifeSpanTimer = 0;
	private float erraticLifeSpanTimerFreq = 3.0f;

	private void MoveForward() {
		myTransform.position += (myTransform.forward * Speed * Time.deltaTime);
	}

	private void MoveBackward() {
		myTransform.position -= (myTransform.forward * Speed * Time.deltaTime);
	}

	private void MoveDownward() {
		myTransform.position -= (myTransform.up * Speed * Time.deltaTime);
	}

	private void MoveLeft() {
		myTransform.position -= (myTransform.right * SidewaysSpeed * Time.deltaTime);
	}

	private void MoveRight() {
		myTransform.position += (myTransform.right * SidewaysSpeed * Time.deltaTime);
	}

	private void DoErraticMovement() {

		if (erraticPauseTimer < erraticPauseTimerFreq) {
			erraticPauseTimer += Time.deltaTime;
		} else {
			if (erraticLifeSpanTimer < erraticLifeSpanTimerFreq) {
				erraticLifeSpanTimer += Time.deltaTime;
			} else {
				erraticLifeSpanTimer = 0;
				erraticLifeSpanTimerFreq = (float)Random.Range (2.5f, 4.5f);
				erraticPauseTimer = 0;
				erraticPauseTimerFreq = (float)Random.Range (0.5f, 2.5f);
			}
		}

		if (FlyErratically) {
			if (LocalRotationEulers != null) {
				if (erraticChangeTimer < erraticChangeTimerFreq) {
					erraticChangeTimer += Time.deltaTime;
				} else {
					erraticLeft = !erraticLeft;
					if (Random.Range (0, 100) < 50) {
						erraticLeft = !erraticLeft;
					} else {
						erraticLeft = false;
					}
					erraticChangeTimerFreq = Random.Range (1.5f, 2.0f);
					erraticChangeTimer = 0;
				}

				if (erraticLeft) {
					zRotation += erraticRotationAddSpeed * Time.deltaTime;
					currentLocRotationEulers.z = zRotation;
					LocalRotationEulers.localRotation = Quaternion.Lerp (LocalRotationEulers.localRotation, Quaternion.Euler (currentLocRotationEulers), erraticRotationSpeed * Time.deltaTime);
					LocalRotationEulers.transform.localPosition += LocalRotationEulers.transform.up * erraticMoveSpeed * Time.deltaTime;
				} else {
					zRotation -= erraticRotationAddSpeed * Time.deltaTime;
					currentLocRotationEulers.z = zRotation;
					LocalRotationEulers.localRotation = Quaternion.Lerp (LocalRotationEulers.localRotation, Quaternion.Euler (currentLocRotationEulers), erraticRotationSpeed * Time.deltaTime);
					LocalRotationEulers.transform.localPosition -= LocalRotationEulers.transform.up * erraticMoveSpeed * Time.deltaTime;
				}
				// Drift
				if (zRotation > 0) {
					LocalRotationEulers.transform.localPosition -= LocalRotationEulers.transform.right * erraticMoveSpeed * Time.deltaTime;
				} else {
					LocalRotationEulers.transform.localPosition += LocalRotationEulers.transform.right * erraticMoveSpeed * Time.deltaTime;
				}
			}
		}
	}
}
