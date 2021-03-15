using UnityEngine;
using System.Collections;

public class BFPCameraKeyControls : MonoBehaviour {

	private Transform myTransform;
	private Transform parentTransform;

	public float MoveSpeed = 5.0f;
	public float RotationSpeed = 5.0f;

	private Vector3 currentParentPosition;
	private Vector3 currentParentEulers;
	private Vector3 currentLocalPos;

	// Use this for initialization
	void Start () {
		myTransform = gameObject.transform;
		parentTransform = gameObject.transform.root.transform;
		currentParentEulers = parentTransform.rotation.eulerAngles;
		currentParentPosition = parentTransform.position;
		currentLocalPos = myTransform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.W)) {
			currentLocalPos.y += MoveSpeed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.S)) {
			currentLocalPos.y -= MoveSpeed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.Q)) {
			currentLocalPos.z += MoveSpeed * 2 * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.E)) {
			currentLocalPos.z -= MoveSpeed * 2 * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.A)) {
			currentParentEulers.y += RotationSpeed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.D)) {
			currentParentEulers.y -= RotationSpeed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			currentParentPosition.z += MoveSpeed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			currentParentPosition.z -= MoveSpeed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			currentParentPosition.x += MoveSpeed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			currentParentPosition.x -= MoveSpeed * Time.deltaTime;
		}

		myTransform.localPosition = currentLocalPos;
		parentTransform.position = currentParentPosition;
		parentTransform.rotation = Quaternion.Euler (currentParentEulers);
	}
}
