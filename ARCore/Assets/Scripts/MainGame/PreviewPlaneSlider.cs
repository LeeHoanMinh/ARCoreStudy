using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PreviewPlaneSlider : MonoBehaviour
{
    Slider slider;
    void Start()
    { 
        slider = this.GetComponent<Slider>();
        slider.onValueChanged.AddListener(delegate { ChangePlaneSize(); });
    }
    
    void ChangePlaneSize()
    {
        GameObject placementIndicator = IndicatorManager.instance.PlacementIndicator;
        Vector3 planeSize = placementIndicator.GetComponentInChildren<DefaultPlane>().transform.localScale;
        IndicatorManager.instance.ResizePlacementIndicator(new Vector3(0.1f + 1.9f * slider.value, planeSize.y, 0.1f + 1.9f * slider.value));
    }

 
}
