using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class TowerImageOnMouse : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject creationSystem;

    UnityEngine.Vector3 mouseWorldPos;
    TagAttribute TagAttribute;

    private CreationSystem cs;

    // Update is called once per frame
    void Update()
    {
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f; // zero z
        transform.position = mouseWorldPos;
        cs = creationSystem.GetComponent<CreationSystem>();
    }

    
    /*private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("touched false");
        if (other.gameObject.CompareTag("Tower") || other.gameObject.CompareTag("Road") || other.gameObject.CompareTag("Obstacle"))
        {
            cs.SetCanPlace(false);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("now true");
        if (other.gameObject.CompareTag("Tower") || other.gameObject.CompareTag("Road") || other.gameObject.CompareTag("Obstacle"))
        {
            cs.SetCanPlace(true);
        }
    }*/

    private void OnCollisionStay2D(Collision2D col)
    {
        Debug.Log("stay");
        if (col.collider.CompareTag("Tower") || col.collider.CompareTag("Road"))
        {
            cs.SetCanPlace(false);
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        Debug.Log("exit");

        if (col.collider.CompareTag("Tower") || col.collider.CompareTag("Road"))
        {
            cs.SetCanPlace(true);
        }

    
    }
}