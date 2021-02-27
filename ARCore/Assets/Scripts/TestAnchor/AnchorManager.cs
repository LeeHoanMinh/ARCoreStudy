using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class AnchorManager : MonoBehaviour
{
    public GameObject anchoredObject;
    public GameObject unanchoredObject;
    ARAnchor arancore;
    
    private void Update()
    {
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
          
            Instantiate(anchoredObject); 
            Instantiate(unanchoredObject);
            anchoredObject.AddComponent<ARAnchor>();
        }
    }
}
