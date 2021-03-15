using UnityEngine;
using System.Collections;

public class BFPMoveObject : MonoBehaviour {

	private Transform myTransform;

	public bool MoveObjectForward = false;
	public bool MoveObjectBackward = false;
	public float MoveSpeed = 5;

	// Use this for initialization
	void Start () {
		myTransform = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (MoveObjectForward) {
			myTransform.position += myTransform.forward * MoveSpeed * Time.deltaTime;
		}
		if (MoveObjectBackward) {
			myTransform.position -= myTransform.forward * MoveSpeed * Time.deltaTime;
		}
	}
}
