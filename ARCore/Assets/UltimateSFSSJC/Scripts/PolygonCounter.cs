using UnityEngine;
using System.Collections;

public class PolygonCounter : MonoBehaviour {

	public int TotalMeshFilters = 0;
	private MeshFilter[] ObjectMeshFilters;

	public int PolyCount = 0;

	// Use this for initialization
	void Start () {
		ObjectMeshFilters = gameObject.GetComponentsInChildren<MeshFilter> ();
		TotalMeshFilters = ObjectMeshFilters.Length;
		if (TotalMeshFilters > 0) {
			int numberOfPolys = 0;
			// Count Polys
			for (int i = 0; i < ObjectMeshFilters.Length; i++) {
				numberOfPolys += (ObjectMeshFilters [i].mesh.triangles.Length / 3);
			}
			PolyCount = numberOfPolys;
			Debug.Log (gameObject.name + " Poly Count = " + PolyCount.ToString() + ".");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
