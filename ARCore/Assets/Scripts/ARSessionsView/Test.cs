using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
public class Test : MonoBehaviour
{
    public Text displayText;
    string text;

    void Start()
    {

    }
    void Update()
    {
      
     
        displayText.text = ARSession.state.ToString();
    }
}
