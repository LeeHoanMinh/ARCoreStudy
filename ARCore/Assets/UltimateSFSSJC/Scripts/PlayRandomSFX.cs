using UnityEngine;
using System.Collections;

public class PlayRandomSFX : MonoBehaviour {

	public AudioClip[] PossibleSounds;
	private AudioSource myAudioSource;

	// Use this for initialization
	void Awake () {
		myAudioSource = gameObject.GetComponent<AudioSource> ();
		if (PossibleSounds.Length > 0) {
			int randomAudioToPlay = Random.Range (0, PossibleSounds.Length);
			myAudioSource.clip = PossibleSounds [randomAudioToPlay];
			myAudioSource.Play ();
		}
	}
}
