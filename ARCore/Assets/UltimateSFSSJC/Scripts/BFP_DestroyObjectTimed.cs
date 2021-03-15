using UnityEngine;
using System.Collections;

public class BFP_DestroyObjectTimed : MonoBehaviour {

	public float LifeTime = 20;
	public float ChildParticleSystemDestroyTime = 6.0f;
	private float timer = 0;
			
	private ParticleSystem[] childParticleSystems;

	void Start() {
		childParticleSystems = gameObject.GetComponentsInChildren<ParticleSystem> ();
	}

	// Update is called once per frame
	void Update () {
		if (timer < LifeTime) {
			timer += Time.deltaTime;
		} else {
			if (childParticleSystems.Length > 0) {
				for (int i = 0; i < childParticleSystems.Length; i++) {
					childParticleSystems [i].Stop ();
					childParticleSystems [i].transform.parent = null;
					Destroy (childParticleSystems [i].gameObject, ChildParticleSystemDestroyTime);
				}
			}
			Destroy (gameObject);
		}
	}
}
