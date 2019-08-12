using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCanvasWithCamera : MonoBehaviour
{
    private Camera camera;
    private RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.rotation = camera.transform.rotation;
    }
}
