using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
    public static EditorManager instance;
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

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}
