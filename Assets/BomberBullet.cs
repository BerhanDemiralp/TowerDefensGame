using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class BomberBullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject AoE;
    
    
    [Header("Attribute")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float rotationSpeed = 300f;

    private Transform target;
    private Vector3 explodePos;
    private GameObject creator;
    private Vector2 direction;

    public float damage = 0f;


    // Start is called before the first frame update
    void Start()
    {   
        GameObject rotationPoint = creator.transform.GetChild(0).gameObject;
        transform.rotation = rotationPoint.transform.rotation ;
    }

    
    private void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
        }

        if(Vector3.Distance(transform.position,explodePos) <= 0.1f)
        {
            AoE_Damage();
            Destroy(gameObject);
        }
    }
    
    void FixedUpdate()
    {
        //RotateTowardsTarget();
        rb.velocity = direction * speed;
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
        explodePos = target.position;
        direction = (target.position - transform.position).normalized;
        Debug.Log("Target is " + target.name);
    }

    /*private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "MainRoadEnemy")
        {
            if(other.gameObject.transform == target)
            {
                MainRoadEnemy targerScript = other.GetComponent<MainRoadEnemy>();
                Instantiate(AoE , transform.position, new Quaternion(0,0,0,0));
                
                Destroy(this.gameObject);
            }
            
        }
    }*/

    public void SetCreator(GameObject _creator)
    {
        creator = _creator;
    }

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }

    private void AoE_Damage()
    {
        Debug.Log("Arrived!");
        var _AoE = Instantiate(AoE , transform.position, new Quaternion(0,0,0,0));
        AoE_Damage _AoEScript = _AoE.GetComponent<AoE_Damage>();
        _AoEScript.SetDamage(damage);
    }




    public void ANIR()
    {
        Debug.Log("Ai ai");
    }
}
