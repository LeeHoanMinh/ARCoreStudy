using UnityEngine;
using System.Collections;

public class JumpgateEffectController : MonoBehaviour {

	private Transform myTransform;

	public float TotalLifeSpan = 10;

	private float lifeTime = 0;

	public Light[] Lights;

	private float currentLightIntensity = 0;
	private float maxLightIntensity = 6;
	public float LightFadeTime = 3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		lifeTime += Time.deltaTime;

		if (lifeTime < LightFadeTime) {
			// Fade In Lights
			if (currentLightIntensity < maxLightIntensity) {
				currentLightIntensity += 4 * Time.deltaTime;
			}
		} else {
			// Fade out Lights
			if (currentLightIntensity > 0) {
				currentLightIntensity -= 4 * Time.deltaTime;
			}
		}

		// Update Lights
		if (Lights.Length > 0) {
			for (int i = 0; i < Lights.Length; i++) {
				Lights [i].intensity = currentLightIntensity;
			}
		}

		if (lifeTime > TotalLifeSpan) {
			Destroy (gameObject);
		}
	}
}
