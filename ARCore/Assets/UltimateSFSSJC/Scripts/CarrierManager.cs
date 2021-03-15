using UnityEngine;
using System.Collections;

public class CarrierManager : MonoBehaviour {

	public int CarrierBaysFound = 0;
	private CarrierBay[] CarrierBayScripts;

	public bool DoorsOpen = false;
	public bool OpenBayDoors = false;
	public bool CloseBayDoors = false;

	// Use this for initialization
	void Start () {
		CarrierBayScripts = gameObject.GetComponentsInChildren<CarrierBay> ();
		if (CarrierBayScripts.Length > 0) {
			CarrierBaysFound = CarrierBayScripts.Length;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (OpenBayDoors) {
			if (CarrierBayScripts.Length > 0) {
				for (int i = 0; i < CarrierBayScripts.Length; i++) {
					CarrierBayScripts [i].OpenBayDoors = true;
				}
			}
			OpenBayDoors = false;
			DoorsOpen = true;
		}
		if (CloseBayDoors) {
			if (CarrierBayScripts.Length > 0) {
				for (int i = 0; i < CarrierBayScripts.Length; i++) {
					CarrierBayScripts [i].CloseBayDoors = true;
				}
			}
			CloseBayDoors = false;
			DoorsOpen = false;
		}
	}
}
