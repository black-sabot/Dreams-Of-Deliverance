using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    public bool planeEnabled = false;
    public float planeY = 20f;
    public float planeSize = 1000f;
    public GameObject[] Characters;

    // Update is called once per frame
    void Update()
    {
        if (planeEnabled)
        {
            foreach (GameObject obj in Characters)
            {
                if (obj.transform.position.y < planeY)
                {
                    obj.SendMessage("Death");
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (planeEnabled)
        {
            Gizmos.color = new Color(0.8f, 0f, 0f, 0.4f);
            Gizmos.DrawCube(new Vector3(0, planeY, 0), new Vector3(planeSize, 1f, planeSize));
        }
    }
}
