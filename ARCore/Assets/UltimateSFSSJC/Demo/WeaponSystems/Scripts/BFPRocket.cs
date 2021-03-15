using UnityEngine;
using System.Collections;

public enum RocketGuidanceTypes {
	Balistic,
	Homing
}

public enum RocketTypes {
	RocketType1
}

public class BFPRocket : MonoBehaviour {

	// Rocket Type or Rocket Level
	public RocketTypes RocketType = RocketTypes.RocketType1;

	// External Prefabs
	public Transform explosionPrefab;

	public RocketGuidanceTypes GuidanceType = RocketGuidanceTypes.Balistic;

	// Damage Variables
	public float RocketDamage = 10;
	public float RocketSpeed = 10;

	// Targeting Variables
	public Transform target;
	public bool GotTarget = false;
	public float targetAquisitionDelay = 6.0f;
	private float targetAquTimer = 0;
	private float timeWithoutTarget = 0;
	private float timeWithoutTargetFreq = 12.0f;

	// LifeTime Variables
	private float currentLifeTime = 0;
	/// <summary>
	/// The total life time of the rocket.
	/// </summary>
	public float LifeTime = 160;

	// Rocket Balalistic Variables
	public float BalisticAngle = 45;
	private float BalisticTimeToTarget = 5.0f;
	private Vector3 throwSpeed = Vector3.zero;
	private Vector3 startingPosition = Vector3.zero;
	public void SetStartingPosition(Vector3 startingPos) {
		startingPosition = startingPos;
	}

	// Rocket Guidance Variables
	private Transform meshTransform;

	public float MaxTurnRate = 180.0f;	// turn rate in degrees per second
	public float MaxThrust = 10.0f;		// maximum acceleration
	public float ThrustRamp = 3.0f;		// how fast full thrust activates
	public float TurnRamp = 3.0f;		// how fast full turning activates
	public float NC = 3.0f;				// set between 3 and 5
	public float IgnitionStartDelay = 1.0f;	// How long after launch before thrusting
	public bool DestroyTrail = true;  // Destroy Rocket Trail on collision (Usually set to false)

	// Realtime Variables
	private bool rocketLaunched = false;
	private float activeTime;
	//private Rigidbody myRigidbody;
	public ParticleSystem myThrusterParticleSystem;

	public TrailRenderer myTrailRenderer;
	public float DestroyTrailTime = 3.0f;

	private float currentThrust = 0.0f;
	private float currentTurnRate = 0.0f;
	private Vector3 lineOfSight = Vector3.zero;  // line of sight
	private Vector3 myAcceleration = Vector3.zero;

	private Transform myTransform;

	// Use this for initialization
	void Start () {
		myTransform = gameObject.transform;

		Vector3 myRotationEulers = myTransform.rotation.eulerAngles;
		myRotationEulers.z = Random.Range (0, 360);
		myTransform.rotation = Quaternion.Euler (myRotationEulers);

//		if (myThrusterParticleSystem)
//			myThrusterParticleSystem.enableEmission = false;
		if (myTrailRenderer != null)
			myTrailRenderer.enabled = false;
		//myRigidbody = gameObject.GetComponent<Rigidbody>();

		MeshRenderer rocketMesh = gameObject.GetComponentInChildren<MeshRenderer>();
		if (rocketMesh != null) {
			meshTransform = rocketMesh.transform;
			meshTransform.name = "myMeshTransform";
		}
	}

	private float distanceToTarget = 0;
	public float DistanceToDetonate = 3;

	public LayerMask CollisionMask;
	private Ray collisionRay;
	private RaycastHit outHit;
	private void CheckForCollisions() {
		collisionRay = new Ray(myTransform.position, myTransform.forward);
		if (Physics.Raycast (collisionRay, out outHit, 0.5f, CollisionMask)) {
			Detonate (outHit.collider.transform);
		}
	}

	// Update is called once per frame
	void Update () {
		if (rocketLaunched) {
			if (collisionCheckTimer < collisionCheckTimerFreq) {
				collisionCheckTimer += Time.deltaTime;
			} else {
				CheckForCollisions ();
				collisionCheckTimer = 0;
			}
			if (target != null) {
				distanceToTarget = Vector3.Distance (myTransform.position, target.position);
				timeWithoutTarget += Time.deltaTime;
				if (timeWithoutTarget > timeWithoutTargetFreq) {
					//Detonate (null);
				}
				if (distanceToTarget < DistanceToDetonate) {
					Detonate (target);
				}
			}
			else {
				if (currentLifeTime > LifeTime)
					Detonate (null);
			}
			currentLifeTime += Time.deltaTime;
			activeTime += Time.deltaTime;
		}
	}

	private GameObject modTargetTrans;

	public void Fire(GameObject launcher, Transform target, bool canFollowTarget, Vector3 startingPos, float volleySpread, float damageIn) {
		activeTime = Time.time;
		rocketLaunched = true;
		SetStartingPosition(startingPos);

		RocketDamage = damageIn;

//		float rocketLevel = (float)RocketType + 1.0f;
//		if (myTrailRenderer != null) {
//			myTrailRenderer.startWidth = rocketLevel * 0.2f;
//			myTrailRenderer.endWidth = rocketLevel * 0.2f;
//		}

		if (GuidanceType == RocketGuidanceTypes.Homing) {
			if (target != null) {
				this.target = target;
				Renderer targetRenderer = target.gameObject.GetComponentInChildren<Renderer> ();
				if (targetRenderer != null)
					DistanceToDetonate = targetRenderer.bounds.size.x / 2;
				else
					DistanceToDetonate = 2;
			}
		}
		else if (GuidanceType == RocketGuidanceTypes.Balistic) {
			GameObject newTargetPos = new GameObject("RocketTargetPosition");
			Vector3 targetingOffset = new Vector3(Random.Range(-volleySpread, volleySpread), 0, Random.Range(-volleySpread, volleySpread));
			targetingOffset = target.position + targetingOffset;
			targetingOffset.y = 0;
			newTargetPos.transform.position = targetingOffset;
			modTargetTrans = newTargetPos;
			this.target = newTargetPos.transform;

			if (GuidanceType == RocketGuidanceTypes.Balistic) {
				// Calculate BalisticTimeToTarget
				//				float distanceToTarget = Vector3.Distance(newTargetPos.transform.position, gameObject.transform.position);
				//				float timeToTargetThrust = 10 / distanceToTarget;
				//				BalisticTimeToTarget = (timeToTargetThrust * 25f) * ThrustRamp;

				throwSpeed = calculateBestThrowSpeed(startingPosition, newTargetPos.transform.position, BalisticTimeToTarget);
				GetComponent<Rigidbody>().AddForce(throwSpeed, ForceMode.VelocityChange);
//				if (GuidanceType == RocketGuidanceTypes.Balistic) {
//					myRigidbody.useGravity = true;
//				}
			}
		}

//		if (myThrusterParticleSystem)
//			myThrusterParticleSystem.enableEmission = true;
		if (myTrailRenderer != null)
			myTrailRenderer.enabled = true;

		// don't collide with our own launcher
//		Collider rocketCollider = GetComponentInChildren<Collider>();
//		Collider[] launcherColliders = launcher.transform.root.GetComponentsInChildren<Collider>();
//		foreach (Collider collider in launcherColliders) {
//			Physics.IgnoreCollision(rocketCollider, collider, true);
//		}

		if (target != null)
			GotTarget = true;

	}

	private float collisionCheckTimer = 0;
	private float collisionCheckTimerFreq = 0.2f;

	public void FixedUpdate() {
		if (!rocketLaunched)
			return;

		if (Time.time - currentLifeTime < IgnitionStartDelay) {
			return;
		}

		if (targetAquTimer < targetAquisitionDelay)
			targetAquTimer += Time.deltaTime;
		if (targetAquTimer >= targetAquisitionDelay)

		if (myThrusterParticleSystem != null) {
			myThrusterParticleSystem.Play();
		}
		if (myTrailRenderer != null) {
			myTrailRenderer.enabled = true;
		}

		if (currentThrust < MaxThrust) {
			// don't go over in case thrustRamp is very small
			float increase = Time.fixedDeltaTime * MaxThrust / ThrustRamp;
			currentThrust = Mathf.Min(currentThrust + increase, MaxThrust);
		}		
		if (currentTurnRate < MaxTurnRate) {
			float increase = Time.fixedDeltaTime * MaxTurnRate / TurnRamp;
			currentTurnRate = Mathf.Min(currentTurnRate + increase, MaxTurnRate);
		}

		Vector3 prevLos = lineOfSight;
		if (target != null) {
			lineOfSight = target.position - transform.position;
		}
		else {
			lineOfSight = transform.forward;
		}

		//		Vector3 balisticForce = Vector3.zero;
		if (GuidanceType == RocketGuidanceTypes.Homing) {
			Vector3 dLos = lineOfSight - prevLos;
			dLos = dLos - Vector3.Project(dLos, lineOfSight);
			myAcceleration = Time.fixedDeltaTime * lineOfSight + dLos * NC + Time.fixedDeltaTime * myAcceleration * NC / 2;
		}
		else if (GuidanceType == RocketGuidanceTypes.Balistic) {
			Vector3 balisticForce = Vector3.zero;
			if (balisticForce == Vector3.zero)
				balisticForce = BallisticVel(BalisticAngle);
		}

		if (GotTarget) {
			if (target != null) {
				if (GuidanceType == RocketGuidanceTypes.Homing) {
					// Homing Rocket

					// limit acceleration to our maximum thrust
					myAcceleration = Vector3.ClampMagnitude(myAcceleration * currentThrust, MaxThrust);

					//					float distanceToTarget = Vector3.Distance(target.position, gameObject.transform.position);

					if (targetAquTimer < targetAquisitionDelay) {

					}
					else {
						Quaternion targetRotation = Quaternion.LookRotation(myAcceleration, transform.up);
						transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * currentTurnRate);
					}

					myTransform.position += myTransform.forward * RocketSpeed * Time.deltaTime;

//					myRigidbody.AddForce(transform.forward * myAcceleration.magnitude, ForceMode.Acceleration);
//					if (GetComponent<Rigidbody>().velocity != Vector3.zero) {
//						if (meshTransform != null)
//							meshTransform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity);
//					}
				}
				else if (GuidanceType == RocketGuidanceTypes.Balistic) {
					// Balistic Rocket
					//myRigidbody.velocity = balisticForce;
//					if (GetComponent<Rigidbody>().velocity != Vector3.zero)
//						transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity);
					//myRigidbody.AddForce(balisticForce, ForceMode.Acceleration);
				}
				//			if (distanceToTarget < 20) {
				//				MaxThrust += 0.1f * Time.deltaTime;
				//				target = null;
				//			}
			}
			else {
				
				GotTarget = false;

				//if (myRigidbody.velocity.sqrMagnitude < MaxThrust)
//				myRigidbody.AddForce(transform.forward * myAcceleration.magnitude, ForceMode.Acceleration);

				if (currentLifeTime > LifeTime) {
					Detonate(null);
				}
			}
		}
		else {
			// No Target
			if (GuidanceType == RocketGuidanceTypes.Homing) {
				// Homing Rocket

				// limit acceleration to our maximum thrust
				myAcceleration = Vector3.ClampMagnitude(myAcceleration * currentThrust, MaxThrust);

				//					float distanceToTarget = Vector3.Distance(target.position, gameObject.transform.position);

				if (targetAquTimer < targetAquisitionDelay) {

				}
				else {
					if (target != null) {
						Quaternion targetRotation = Quaternion.LookRotation (myAcceleration, transform.up);
						transform.rotation = Quaternion.RotateTowards (transform.rotation, targetRotation, Time.deltaTime * currentTurnRate);
					}
				}

				myTransform.position += myTransform.forward * RocketSpeed * Time.deltaTime;

				//					myRigidbody.AddForce(transform.forward * myAcceleration.magnitude, ForceMode.Acceleration);
				//					if (GetComponent<Rigidbody>().velocity != Vector3.zero) {
				//						if (meshTransform != null)
				//							meshTransform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity);
				//					}
			}
			//if (myRigidbody.velocity.sqrMagnitude < MaxThrust)
//			myRigidbody.AddForce(transform.forward * myAcceleration.magnitude, ForceMode.Acceleration);
//
//			if (!myRigidbody.useGravity)
//				myRigidbody.useGravity = true;

			//			if (currentLifeTime > LifeTime) {
//			Detonate(null);
			//			}
		}
	}

	private Vector3 calculateBestThrowSpeed(Vector3 origin, Vector3 target, float timeToTarget) {
		// calculate vectors
		Vector3 toTarget = target - origin;
		Vector3 toTargetXZ = toTarget;
		toTargetXZ.y = 0;

		// calculate xz and y
		float y = toTarget.y;
		float xz = toTargetXZ.magnitude;

		// calculate starting speeds for xz and y. Physics forumulase deltaX = v0 * t + 1/2 * a * t * t
		// where a is "-gravity" but only on the y plane, and a is 0 in xz plane.
		// so xz = v0xz * t => v0xz = xz / t
		// and y = v0y * t - 1/2 * gravity * t * t => v0y * t = y + 1/2 * gravity * t * t => v0y = y / t + 1/2 * gravity * t
		float t = timeToTarget;
		float v0y = y / t + 0.5f * Physics.gravity.magnitude * t;
		float v0xz = xz / t;

		// create result vector for calculated starting speeds
		Vector3 result = toTargetXZ.normalized; // get direction of xz but with magnitude 1
		result *= v0xz; // set magnitude of xz to v0xz (starting speed in xz plane)
		result.y = v0y; // set y to v0y (starting speed of y plane)

		return result;
	}

	private Vector3 BallisticVel(float angle) {
		Vector3 dir = target.position - transform.position;  // get target direction
		float h = dir.y;  // get height difference
		dir.y = 0;  // retain only the horizontal direction
		float dist = dir.magnitude ;  // get horizontal distance
		float a = angle * Mathf.Deg2Rad;  // convert angle to radians
		dir.y = dist * Mathf.Tan(a);  // set dir to the elevation angle
		dist += h / Mathf.Tan(a);  // correct for small height differences
		// calculate the velocity magnitude
		float vel = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
		return vel * dir.normalized;
	}

	public void Detonate(Transform hit) {

		// Damage Hit Object
		if (hit != null) {
			// Damage GameObject Hit
			hit.gameObject.transform.root.gameObject.SendMessage ("Damage", RocketDamage, SendMessageOptions.DontRequireReceiver);
		}

		if (modTargetTrans != null) {
			Destroy(modTargetTrans.gameObject);
		}

		if (!DestroyTrail) {
			// detach trail so it will remain behind
			if (myThrusterParticleSystem != null) {
				myThrusterParticleSystem.transform.parent = null;
				Destroy (myThrusterParticleSystem.gameObject, DestroyTrailTime * 3);
			}
			if (myTrailRenderer != null) {
				myTrailRenderer.time = 0.1f;
				myTrailRenderer.transform.parent = null;
				Destroy (myTrailRenderer.gameObject, DestroyTrailTime);
			}
		} else {
			if (myThrusterParticleSystem != null) {
				myThrusterParticleSystem.transform.parent = null;
				Destroy (myThrusterParticleSystem.gameObject, DestroyTrailTime);
			}
		}

		if (explosionPrefab != null) {
			Transform explosion = (Transform)Instantiate(explosionPrefab, transform.position, Quaternion.identity);
			explosion.name = "RocketExplosion";
			//explosion.rigidbody.velocity = myRigidbody.velocity;
		}
		Destroy(this.gameObject);
	}
}
