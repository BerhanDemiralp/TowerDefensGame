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
    [SerializeField] private float speed = 3f;
    [SerializeField] private float rotationSpeed = 300f;

    private Transform target;
    private GameObject creator;

    public float damage = 0f;


    // Start is called before the first frame update
    void Start()
    {   
        GameObject rotationPoint = creator.transform.GetChild(0).gameObject;
        transform.rotation = rotationPoint.transform.rotation;
    }

    
    private void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
        }
    }
    
    void FixedUpdate()
    {
        RotateTowardsTarget();
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
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "MainRoadEnemy")
        {
            if(other.gameObject.transform == target)
            {
                MainRoadEnemy targerScript = other.GetComponent<MainRoadEnemy>();
                targerScript.DealDamage(damage);
                Debug.Log(damage + " damage dealt!");
                Destroy(this.gameObject);
            }
            
        }
    }

    public void setCreator(GameObject _creator)
    {
        creator = _creator;
    }

    public void setDamage(float _damage)
    {
        damage = _damage;
    }




    public void ANIR()
    {
        Debug.Log("Ai ai");
    }
}
