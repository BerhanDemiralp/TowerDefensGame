using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class GunnerBullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask enemyMask;
    
    
    [Header("Attribute")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float rotationSpeed = 300f;
    [SerializeField] private float range = 0.5f;
    [SerializeField] private float maxTarget = 3f;
    [SerializeField] private float targetCount = 0f;

    private Transform target;
    private GameObject creator;
    private GameObject currentTarget;

    public float damage = 0f;

    private float damageTemp = 0f;

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
            if(!FindTarget()){Destroy(gameObject);};
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

    private bool FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, (Vector2)transform.position, 0f, enemyMask);
        int i = 0;
        foreach (RaycastHit2D hit in hits)
        {
            if(hits.Length > 0 && hit.collider.GetComponent<MainRoadEnemy>().enemyCount > currentTarget.GetComponent<MainRoadEnemy>().enemyCount)
            {
                target = hits[i].transform;
                if(targetCount != 0){speed *= targetCount;}
                return true;
            }else{i++;}
        }
        return false;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "MainRoadEnemy")
        {
            if(other.gameObject.transform == target)
            {
                currentTarget = other.gameObject;
                MainRoadEnemy targetScript = other.GetComponent<MainRoadEnemy>();
                damage -= targetScript.hitPoints;
                targetScript.DealDamage(damageTemp);
                damageTemp = damage;
                targetCount++;
                if(targetCount == maxTarget || damage <= 0){Destroy(gameObject);}
                FindTarget();
                Debug.Log(damage + " damage dealt!");
                Debug.Log("Remaining damage is " + damage);
            }
            
        }
    }

    public void SetCreator(GameObject _creator)
    {
        creator = _creator;
    }

    public void SetDamage(float _damage)
    {
        damage = _damage;
        damageTemp = _damage;
    }




    public void ANIR()
    {
        Debug.Log("Ai ai");
    }
}
