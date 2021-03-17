using UnityEngine;
using System.Collections;
using System;
namespace SciFiArsenal
{
    public class MyBullets : MonoBehaviour
    {
        public GameObject impactParticle;
        public GameObject projectileParticle;
        public GameObject muzzleParticle;
        public GameObject[] trailParticles;

        public Collider collider;
        [HideInInspector]
        public Vector3 impactNormal; //Used to rotate impactparticle.
        private bool hasCollided = false;

        void Start()
        {
            projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation) as GameObject;
            projectileParticle.transform.parent = transform;
            if (muzzleParticle)
            {
                muzzleParticle = Instantiate(muzzleParticle, transform.position, transform.rotation) as GameObject;
                muzzleParticle.transform.rotation = transform.rotation * Quaternion.Euler(180, 0, 0);
                Destroy(muzzleParticle, 1.5f); // Lifetime of muzzle effect.
            }
        }

        void OnCollisionEnter(Collision hit)
        {
            if (!hasCollided)
            {
                hasCollided = true;
                impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;
                Destroy(projectileParticle, 3f);
                Destroy(impactParticle, 5f);
                Destroy(gameObject);

            }
        }
    }
}