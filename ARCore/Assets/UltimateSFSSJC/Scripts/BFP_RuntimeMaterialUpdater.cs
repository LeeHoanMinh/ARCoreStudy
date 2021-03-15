using UnityEngine;
using System.Collections;

public class BFP_RuntimeMaterialUpdater : MonoBehaviour {

	public Material[] OriginalMaterials;
	public Material[] NewMaterials;

	public bool DoUpdate = false;
	public bool MaterialsUpdated = false;

	private Renderer[] myRenderers;

	// Use this for initialization
	void Start () {
		myRenderers = gameObject.GetComponentsInChildren<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (DoUpdate && !MaterialsUpdated) {
			UpdateMaterials ();
		}
	}

	private void UpdateMaterials() {

		if (myRenderers.Length > 0) {
			if (OriginalMaterials.Length == NewMaterials.Length) {

				for (int i = 0; i < myRenderers.Length; i++) {
					for (int j = 0; j < OriginalMaterials.Length; j++) {
						if (myRenderers [i].sharedMaterial == OriginalMaterials [j]) {
							myRenderers [i].sharedMaterial = NewMaterials [j];
						}
					}
				}

			}
		}

		MaterialsUpdated = true;
	}
}
