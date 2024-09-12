using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class StandartBullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    
    [Header("Attribute")]
    [SerializeField] private float speed = 30f;
    [SerializeField] private float rotationSpeed = 300f;

    private Transform target;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotateTowardsTarget();
        //transform.position += transform.forward * Time.deltaTime * speed;
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * speed;

        
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
        Debug.Log("Target is " + target.name);
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    public void ANIR()
    {
        Debug.Log("Ai ai");
    }
}
