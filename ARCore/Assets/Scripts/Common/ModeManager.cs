using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManager : MonoBehaviour
{
    public static ModeManager instance;
    [SerializeField]
    Camera ArCamera, EditorCamera;
    Camera currentCamera;

    [SerializeField]
    GameObject ArComponent, EditorComponent;
    public enum AppMode
    {
        ARMode,
        EditorMode
    };
    [SerializeField]
    AppMode appMode;

    public bool InEditorMode()
    {
        return (appMode == AppMode.EditorMode);
    }

    public Camera GetMainCamera()
    {
        return currentCamera;
    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        if(InEditorMode())
        {
            currentCamera = EditorCamera;
            EditorComponent.SetActive(true);
        }
        else
        {
            currentCamera = ArCamera;
            ArComponent.SetActive(true);
        }
    }
}
