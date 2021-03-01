using UnityEngine;
using System.Collections;

// PLEASE NOTE! THIS SCRIPT IS FOR DEMO PURPOSES ONLY :) //

public class Plane : MonoBehaviour {
	public GameObject prop;
	public GameObject propBlured;
	public float height;
	public float radius;
	public float rotateSpeed;
	Vector3 center;
	float angle;
	public bool engenOn;


	private void Start()
	{
		StartCoroutine(MoveUp());
	}
	void Update () 
	{
		if (engenOn) {
			prop.SetActive (false);
			propBlured.SetActive (true);
			propBlured.transform.Rotate (1000 * Time.deltaTime, 0, 0);
		} else {
			prop.SetActive (true);
			propBlured.SetActive (false);
		}
	}
	IEnumerator MoveUp()
	{
		Vector3 newPosition = this.transform.position + new Vector3(0f,height,0f);
		while (this.transform.position != newPosition)
		{
			this.transform.position = Vector3.MoveTowards(this.transform.position, newPosition, 0.001f);
			yield return new WaitForSeconds(.02f);
		}

		center = this.transform.position;
		newPosition = this.transform.position + new Vector3(0f, 0f, radius);
		while (this.transform.position != newPosition)
		{
			this.transform.position = Vector3.MoveTowards(this.transform.position, newPosition, 0.001f);
			yield return new WaitForSeconds(.02f);
		}
		
		StartCoroutine(MoveCircle());
		yield return null;
	}
	
	IEnumerator MoveCircle()
	{
		while(true)
		{
			angle += rotateSpeed * Time.deltaTime;
			var offset = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * radius;
			yield return new WaitForSeconds(0.02f);
			transform.position = center + offset;
		
		}
	}
}

// PLEASE NOTE! THIS SCRIPT IS FOR DEMO PURPOSES ONLY :) //