using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CloseApplication : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
            }
            else
            {
                Application.Quit();
            }
        }
    }
}
