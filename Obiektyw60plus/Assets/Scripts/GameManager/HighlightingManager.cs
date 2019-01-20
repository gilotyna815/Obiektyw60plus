using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightingManager : MonoBehaviour {


    #region Singleton

    public static HighlightingManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of HighlightingManager found!");
            return;
        }
        instance = this;
    }

    #endregion

}
