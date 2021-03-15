using UnityEngine;
using System.Collections;

public class NanobotController : MonoBehaviour {
	public Transform BotModelTransform;

	public StarshipTypes SizeClassBeingBuilt = StarshipTypes.Small;

	void Start () {
		if (Beam01Renderer != null) {
			Beam01Renderer.enabled = false;
		}
		if (Beam02Renderer != null) {
			Beam02Renderer.enabled = false;
		}
	}

	// Nano Beam Variables
	private float nanoBeamFreq = 1.0f;
	public void SetNanoBeamFreq(float freqIn) {
		nanoBeamFreq = freqIn;
	}

	// Sound SFX
	public AudioSource LaunchSFX;
	public void PlayLaunchSFX() {
		if (LaunchSFX != null)
			LaunchSFX.Play ();
	}
	public AudioSource DockSFX;
	public void PlayDockSFX() {
		if (DockSFX != null)
			DockSFX.Play ();
		Debug.Log ("Playing DockSFX.");
	}
	public AudioSource Beam01SFX;
	public AudioSource Beam02SFX;

	// Nano Beam 01
	public bool NanoBeam01Active = false;
	private bool hasSpawnedHitEffects01 = false;
	private bool hasPlayedSFX01 = false;
	private bool beam01Firing = false;
	public Transform NanobeamPort01;
	private float beam01Timer = 0;
	private float beam01Width = 0.5f;
	public LineRenderer Beam01Renderer;
	private Transform beam01TargetTransform;
	public bool FireBeam01(Transform beamTarget01) {
		bool beamFired = false;

		if (!NanoBeam01Active) {
			beam01TargetTransform = beamTarget01;
			if (!beam01Firing)
				beam01Firing = true;
			NanoBeam01Active = true;
			beamFired = true;
		}

		return beamFired;
	}

	// Nano Beam 02
	public bool NanoBeam02Active = false;
	private bool hasSpawnedHitEffects02 = false;
	private bool hasPlayedSFX02 = false;
	private bool beam02Firing = false;
	public Transform NanobeamPort02;
	private float beam02Timer = 0;
	private float beam02Width = 0.5f;
	public LineRenderer Beam02Renderer;
	private Transform beam02TargetTransform;
	public bool FireBeam02(Transform beamTarget02) {
		bool beamFired = false;
		if (!NanoBeam02Active) {
			beam02TargetTransform = beamTarget02;
			if (!beam02Firing)
				beam02Firing = true;
			NanoBeam02Active = true;
			beamFired = true;
		}
		return beamFired;
	}

	void Update () {
		if (NanoBeam01Active) {
			if (beam01Timer < nanoBeamFreq) {

				if (!hasSpawnedHitEffects01) {
					if (SizeClassBeingBuilt == StarshipTypes.Small)
						StarshipPrefabManager.GlobalAccess.AddNanobeamHitSparksSmall (beam01TargetTransform.position);
					else if (SizeClassBeingBuilt == StarshipTypes.Medium)
						StarshipPrefabManager.GlobalAccess.AddNanobeamHitSparksMedium (beam01TargetTransform.position);
					else if (SizeClassBeingBuilt == StarshipTypes.Large)
						StarshipPrefabManager.GlobalAccess.AddNanobeamHitSparksLarge (beam01TargetTransform.position);
					hasSpawnedHitEffects01 = true;
				}
				if (!hasPlayedSFX01) {
					Beam01SFX.Play ();
					hasPlayedSFX01 = true;
				}

				if (Beam01Renderer != null) {
					Beam01Renderer.SetPosition (0, NanobeamPort01.position);
					Beam01Renderer.SetPosition (1, beam01TargetTransform.position);
					if (beam01Timer < nanoBeamFreq / 2)
						beam01Width += 6 * Time.deltaTime;
					else
						beam01Width -= 6 * Time.deltaTime;
					Beam01Renderer.startWidth = beam01Width;
					Beam01Renderer.endWidth = beam01Width;
					if (!Beam01Renderer.enabled)
						Beam01Renderer.enabled = true;					
				}

				beam01Firing = true;
				beam01Timer += Time.deltaTime;
			
			} else {
				
				hasSpawnedHitEffects01 = false;
				hasPlayedSFX01 = false;
				if (Beam01Renderer != null) {
					if (Beam01Renderer.enabled) {
						beam01Width = 0;
						Beam01Renderer.startWidth = beam01Width;
						Beam01Renderer.endWidth = beam01Width;
						Beam01Renderer.enabled = false;
					}
				}
				beam01Timer = 0;
				beam01Width = 0.5f;
				beam01Firing = false;
				NanoBeam01Active = false;

			}
		}
		
		if (NanoBeam02Active) {
			if (beam02Timer < nanoBeamFreq) {

				if (!hasSpawnedHitEffects02) {
					StarshipPrefabManager.GlobalAccess.AddNanobeamHitSparksSmall (beam02TargetTransform.position);
					hasSpawnedHitEffects02 = true;
				}
				if (!hasPlayedSFX02) {
					Beam02SFX.Play ();
					hasPlayedSFX02 = true;
				}

				if (Beam02Renderer != null) {
					Beam02Renderer.SetPosition (0, NanobeamPort02.position);
					Beam02Renderer.SetPosition (1, beam02TargetTransform.position);
					if (beam02Timer < nanoBeamFreq / 2)
						beam02Width += 6 * Time.deltaTime;
					else
						beam02Width -= 6 * Time.deltaTime;
					Beam02Renderer.startWidth = beam02Width;
					Beam02Renderer.endWidth = beam02Width;
					if (!Beam02Renderer.enabled) {
						Beam02Renderer.enabled = true;
					}
				}

				beam02Firing = true;
				beam02Timer += Time.deltaTime;
				
			} else {
				
				hasSpawnedHitEffects02 = false;
				hasPlayedSFX02 = false;
				beam02Width = 0;
				Beam02Renderer.startWidth = beam02Width;
				Beam02Renderer.endWidth = beam02Width;
				if (Beam02Renderer != null) {
					if (Beam02Renderer.enabled) {
						Beam02Renderer.enabled = false;
					}
				}
				beam02Timer = 0;
				beam02Width = 0.5f;
				beam02Firing = false;
				NanoBeam02Active = false;
			}			
		}
	}

	public void UpdateLocalRotation(Vector3 localEulers) {
		if (BotModelTransform != null)
			BotModelTransform.localRotation = Quaternion.Euler (localEulers);
	}
}
