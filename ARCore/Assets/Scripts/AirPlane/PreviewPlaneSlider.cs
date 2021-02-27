using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PreviewPlaneSlider : MonoBehaviour
{
    [SerializeField]
    Slider slider;
    void Start()
    { 
        slider = this.GetComponent<Slider>();
        slider.onValueChanged.AddListener(delegate { ChangePlaneSize(); });
    }
    
    void ChangePlaneSize()
    {
        GameObject placementIndicator = IndicatorManager.instance.PlacementIndicator;
        IndicatorManager.instance.ResizePlacementIndicator(new Vector3(0.1f + 1.9f * slider.value, placementIndicator.transform.localScale.y, 0.1f + 1.9f * slider.value));
    }

 
}
