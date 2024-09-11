using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class TowerImageOnMouse : MonoBehaviour
{
    UnityEngine.Vector3 mouseWorldPos;

    // Update is called once per frame
    void Update()
    {
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f; // zero z
        transform.position = mouseWorldPos;

    }
}
