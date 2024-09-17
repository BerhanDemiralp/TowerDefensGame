using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.IO.LowLevel.Unsafe;
using System.Diagnostics.Tracing;

public class Tower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject[] bullets;
    
    
    
    [Header("Attribute")]
    [SerializeField] private float damage = 0;
    [SerializeField] private float attackSpeed = 2; 
    [SerializeField] private float range = 5f;
    [SerializeField] private float rotationSpeed = 30f;
    [SerializeField] private bool canShoot = true;

    private Transform target;
    
    private Quaternion targetRotation;

    private SpriteRenderer this_SpriteRenderer;

    //public Color color;

    private float timeUntilFire = 10;

    private int blueBlock;
    private int greenBlock;
    private int redBlock;

    private string indicator;

    public Tower(int blue_block_count,int green_block_count,int red_block_count,string indicator_type)
    {
        blueBlock = blue_block_count;
        greenBlock = green_block_count;
        redBlock = red_block_count;
        indicator = indicator_type;
    }
    void Start()
    {
        timeUntilFire = 10f;
        //this_SpriteRenderer = GetComponent<SpriteRenderer>();
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
                Fire();
                timeUntilFire = 0;
            }
            
        }
        /*Debug.Log(CompareQuaternion(transform.rotation, targetRotation));
        Debug.Log(transform.rotation);
        Debug.Log(targetRotation);*/

       
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

    public void GetBlocks(int redBlockTemp, int blueBlockTemp, int greenBlockTemp)
    {
        redBlock = redBlockTemp;
        blueBlock = blueBlockTemp;
        greenBlock = greenBlockTemp;
        SetStats();
    }

    private void SetStats()
    {
        damage = 12f + (redBlock* 4f);
        attackSpeed = 0.33f + (blueBlock * 0.10f);
        range = 2 + (greenBlock * 0.22f);
        Debug.Log(damage + " - " + attackSpeed + " - " + range);
    }
    
    public void SetIndicator(int _indicator)
    {
        switch(_indicator){
            case 0:
                indicator = "Standart";
                //this_SpriteRenderer.color = Color.white;
                break;
            case 1: 
                indicator = "Bomber";
                //this_SpriteRenderer.color = Color.red;
                break;
            case 2:
                indicator = "Gunner";
                //this_SpriteRenderer.color = new Color(215, 4, 166, 255);
                break;
        }
        Debug.Log("Indicator is " + indicator);
    }

    private void Fire()
    {
        switch(indicator)
        {
            case "Standart":
                ShootStandart();
                break;
            case "Bomber":
                ShootBomber();
                break;
            case "Gunner":
                ShootGunner();
                break;
        }

    }

    private void ShootStandart()
    {
        var bullet = Instantiate(bullets[0], firingPoint.position, Quaternion.identity);
        StandartBullet bulletScript = bullet.GetComponent<StandartBullet>();
        bulletScript.SetTarget(target);
        bulletScript.setCreator(gameObject);
        bulletScript.setDamage(damage);
    }

    private void ShootBomber()
    {
        var bullet = Instantiate(bullets[1], firingPoint.position, Quaternion.identity);
        BomberBullet bulletScript = bullet.GetComponent<BomberBullet>();
        bulletScript.SetTarget(target);
        bulletScript.SetCreator(gameObject);
        bulletScript.SetDamage(damage);
        Debug.Log("Bomber bullet shot!");
    }

    private void ShootGunner()
    {
        var bullet = Instantiate(bullets[2], firingPoint.position, Quaternion.identity);
        GunnerBullet bulletScript = bullet.GetComponent<GunnerBullet>();
        bulletScript.SetTarget(target);
        bulletScript.SetCreator(gameObject);
        bulletScript.SetDamage(damage);
        Debug.Log("Gunner bullet shot!");
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
