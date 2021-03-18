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

        public int dame;
        [HideInInspector]
        public Vector3 impactNormal; //Used to rotate impactparticle.
        private bool hasCollided = false;
        Camera mainCamera;
        void Start()
        {
            mainCamera = ModeManager.instance.GetMainCamera();
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

                GameObject Obj = hit.gameObject;

                for (int t = 0; t < 6; t++)
                {
                    if (Obj.transform.parent != null)
                        Obj = Obj.transform.parent.gameObject;
                }
                if(Obj.GetComponent<EnemyClass>())
                {
                    Obj.GetComponent<EnemyClass>().BeShot(dame);
                }

                if (Obj.GetComponent<MainBuilding>())
                {
                    Obj.GetComponent<MainBuilding>().BeShot(dame);
                }
            }
        }

        void Update()
        {
            if (Vector3.Distance(this.transform.position, mainCamera.transform.position) > 6f)
                Destroy(this.gameObject);
        }
    }
}