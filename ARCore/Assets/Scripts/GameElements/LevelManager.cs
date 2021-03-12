using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [Serializable]
    public class SingleLevel
    {
        public int expNeed;
        public int damePlus;
        public float speedPlus;
    }
    
    public SingleLevel[] levels;

    private void Awake()
    {
        if(instance == null)
        {
            LevelManager.instance = this;
        }
    }
}
