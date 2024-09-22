using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.IO.LowLevel.Unsafe;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;

public class Standart : MonoBehaviour
{

    private const string BULLETNAME = "StandartBullet";


    [Header("References")]
    [SerializeField] protected Transform turretRotationPoint;
    [SerializeField] protected Transform firingPoint;
    [SerializeField] protected LayerMask enemyMask;
    [SerializeField] protected GameObject bullet;
    
    
    
    [Header("Attribute")]
    [SerializeField] protected float damage = 0;
    [SerializeField] protected float attackSpeed = 2; 
    [SerializeField] protected float range = 5f;
    [SerializeField] protected float rotationSpeed = 500f;
    [SerializeField] protected bool canShoot = true;

    protected Transform target;
    protected Quaternion targetRotation;
    protected SpriteRenderer this_SpriteRenderer;

    private float timeUntilFire = 10;

    private int redBlock;
    private int blueBlock;
    private int greenBlock;
    

    void Start()
    {
        BaseSetter();
    }

    void Update()
    {
        timeUntilFire += Time.deltaTime;
        

        if(target == null)
        {
            FindTarget();
            return;
            
        }
        
        RotateTowardsTarget();

       if(!CheckTargetIsInRange())
       {
            target = null;
       }else
        {
            
            if(timeUntilFire >= 1f / attackSpeed && CompareQuaternion(turretRotationPoint.rotation, targetRotation))
            {
                Shoot();
                timeUntilFire = 0;
            }
            
        }
    }

    private void Shoot()
    {
        var _bullet = Instantiate(bullet, firingPoint.position, Quaternion.identity);
        StandartBullet bulletScript = _bullet.GetComponent<StandartBullet>();
        bulletScript.SetTarget(target);
        bulletScript.setCreator(gameObject);
        bulletScript.setDamage(damage);
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, (Vector2)transform.position, 0f, enemyMask);
        if(hits.Length > 0){
            target = hits[0].transform;
        }
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90;

        targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, range);

    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= range;
    }

    public void SetBlocks(int redBlockTemp, int blueBlockTemp, int greenBlockTemp)
    {
        redBlock = redBlockTemp;
        blueBlock = blueBlockTemp;
        greenBlock = greenBlockTemp;
        SetStats();
    }

    private void SetStats()
    {
        damage = 6f + (redBlock* 4f);
        attackSpeed = 0.33f + (blueBlock * 0.15f);
        range = 2 + (greenBlock * 0.22f);
        Debug.Log(damage + " - " + attackSpeed + " - " + range);
        
    }
    
    public void BaseSetter()
    {
        SetBullet(BULLETNAME);
        SetColor(Color.white);
        timeUntilFire = 10f;
        turretRotationPoint = gameObject.transform.GetChild(0);
        firingPoint = gameObject.transform.GetChild(0).GetChild(0).GetChild(0);
        enemyMask = LayerMask.GetMask("Enemy");
    }

    public void SetBullet(string bulletName)
    {
        string _bulletName = bulletName;
        bulletName = "Prefabs/Bullets/" + _bulletName;
        bullet = Resources.Load<GameObject>(bulletName);
    }


    /* Anıt olarak bırakıyorum burada.
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "MainRoadEnemy")
        {
            //Düşmana hasar vur.
            Destroy(gameObject);
        }
    }*/

    public bool CompareQuaternion(Quaternion q1,Quaternion q2)
    {
        if (q1.x == q2.x&& q1.y == q2.y && q1.z == q2.z && q1.w == q2.w)
        {
        return true;
        }       

    return false;
    }
    
    public void SetColor(Color _color)
    {
        this_SpriteRenderer = GetComponent<SpriteRenderer>();
        this_SpriteRenderer.color = _color;
    }

}
