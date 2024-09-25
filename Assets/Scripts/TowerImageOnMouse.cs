using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class TowerImageOnMouse : MonoBehaviour
{
    [Header("References")]
    //[SerializeField] private GameObject creationSystem;

    public bool isColliding;

    UnityEngine.Vector3 mouseWorldPos;
    TagAttribute TagAttribute;

    //private CreationSystem cs;

    // Update is called once per frame
    void Update()
    {
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f; // zero z
        transform.position = mouseWorldPos;
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        Debug.Log("stay");
        if (col.collider.CompareTag("Tower") || col.collider.CompareTag("Road"))
        {
            isColliding = false;
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        Debug.Log("exit");

        if (col.collider.CompareTag("Tower") || col.collider.CompareTag("Road"))
        {
            isColliding= true;
        }
    }
}