using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SciFiArsenal;
using System;
using TMPro;
public class Player : MonoBehaviour
{
    public static Player instance;

    [SerializeField]
    int level;
    [SerializeField]
    int dame;
    [SerializeField]
    float shootingSpeed;

    [SerializeField]
    HealthBar expBar;
    [SerializeField]
    TextMeshProUGUI lvText;

    int exp;
    Camera currentCamera;
    float timeValueToShoot;


    [SerializeField]
    float bulletSpeed;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {
        currentCamera = ModeManager.instance.GetMainCamera();   
    }

    void Update()
    {
        timeValueToShoot += Time.deltaTime;
        CheckLevelUp();
        UpdateExpBar();
    }
    void UpdateExpBar()
    {
        int current, max;
        current = exp;
        max = LevelManager.instance.levels[level + 1].expNeed;
        current -= LevelManager.instance.levels[level].expNeed;
        max -= LevelManager.instance.levels[level].expNeed;
        
        expBar.UpdateFilledAmount(current,max);
        lvText.text = "Level " + (level + 1).ToString();
    }
    void CheckLevelUp()
    {
     
        if(exp >= LevelManager.instance.levels[level + 1].expNeed)
        {
            Debug.Log("Level up: " + (level + 1).ToString());
            level++;
            dame += LevelManager.instance.levels[level].damePlus;
            shootingSpeed -= LevelManager.instance.levels[level].speedPlus;
        }
    }

    public void PlusExp(int value)
    {
        exp += value;
    }
    public void Shoot()
    {
        if (timeValueToShoot > shootingSpeed)
        {
            Ray ray = currentCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            RaycastHit[] hitObject = new RaycastHit[10];
            int hitCnt = Physics.RaycastNonAlloc(ray, hitObject);




            GameObject projectile = Instantiate(ObjectsManager.instance.playerProjectile, currentCamera.transform.position, Quaternion.identity) as GameObject;

            //projectile.transform.LookAt(hitObject[0].point);
            projectile.transform.rotation = Quaternion.LookRotation(ray.direction);
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * bulletSpeed);
            projectile.GetComponent<MyBullets>().impactNormal = hitObject[0].normal;
            projectile.GetComponent<MyBullets>().dame = dame;
            timeValueToShoot = 0;
        }
        timeValueToShoot += Time.deltaTime;
        
        
    }



}
