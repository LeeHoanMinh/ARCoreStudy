using UnityEngine;
using System.Collections;

public class LookAtScript : MonoBehaviour {

	private Transform myTransform;

	public bool LookAtCamera = false;
	public Transform Target;

	// Use this for initialization
	void Start () {
		myTransform = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (!LookAtCamera) {
			if (Target != null) {
				myTransform.LookAt (Target.position);
			}
		} else {
			myTransform.LookAt (Camera.main.transform.position);
		}
	}
}
