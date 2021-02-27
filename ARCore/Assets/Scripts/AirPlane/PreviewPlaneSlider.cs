using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PreviewPlaneSlider : MonoBehaviour
{
    [SerializeField]
    ARTapToPlaceObject manager;
    Slider slider;
    void Start()
    {
        Debug.Log("Changed");
        slider = this.GetComponent<Slider>();
        slider.onValueChanged.AddListener(delegate { ChangePlaneSize(); });
    }
    
    void ChangePlaneSize()
    {
        Debug.Log("ChangedValues");
        GameObject plane = manager.placementIndicator;
        plane.transform.localScale = new Vector3(0.1f + 1.9f * slider.value,plane.transform.localScale.y,0.1f + 1.9f * slider.value);
    }

 
}
