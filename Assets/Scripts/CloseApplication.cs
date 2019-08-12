using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CloseApplication : MonoBehaviour
{
    public bool EscapeStopsEditor = true;

    void Update()
    {
        if (Input.GetKey("escape"))
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying && EscapeStopsEditor)
            {
                EditorApplication.isPlaying = false;
            }
#endif

            Application.Quit();
        }
    }
}
