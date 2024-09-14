using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class TowerImageOnMouse : MonoBehaviour
{
    UnityEngine.Vector3 mouseWorldPos;
    TagAttribute TagAttribute;

    public bool canPlace = true;
    // Update is called once per frame
    void Update()
    {
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f; // zero z
        transform.position = mouseWorldPos;

    }

    private void OnCollisionStay2D(Collision2D col)
    {
        Debug.Log("oncoll");
        if (col.collider.CompareTag("Tower") || col.collider.CompareTag("Road") || col.collider.CompareTag("Obstacle"))
        {
            canPlace= false;
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        Debug.Log("exitcoll");

        if (col.collider.CompareTag("Tower") || col.collider.CompareTag("Road") || col.collider.CompareTag("Obstacle"))
        {
            canPlace = true;
        }
    }
}
