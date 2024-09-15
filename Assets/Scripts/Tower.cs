using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.IO.LowLevel.Unsafe;

public class Tower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject standartBullet;
    
    
    [Header("Attribute")]
    [SerializeField] private float damage = 0;
    [SerializeField] private float attackSpeed = 2; 
    [SerializeField] private float range = 5f;
    [SerializeField] private float rotationSpeed = 30f;
    [SerializeField] private bool canShoot = true;

    private Transform target;


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
        //standartBullet = GameObject.Find("StandartBullet"); 
    }

    // Update is called once per frame
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
            
            if(timeUntilFire >= 1f / attackSpeed)
            {
                Fire();
                timeUntilFire = 0;
            }
            
        }

       
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

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
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

    public void getBlocks(int redBlockTemp, int blueBlockTemp, int greenBlockTemp)
    {
        redBlock = redBlockTemp;
        blueBlock = blueBlockTemp;
        greenBlock = greenBlockTemp;
        setStats();
    }

    private void setStats()
    {
        damage = 12f + (redBlock* 4f);
        attackSpeed = 0.33f + (blueBlock * 0.10f);
        range = 2 + (greenBlock * 0.22f);
        Debug.Log(damage + " - " + attackSpeed + " - " + range);
    }
    
    public void setIndicator(int indicatorTemp)
    {
        switch(indicatorTemp){
            case 0:
                indicator = "Standart";
                Debug.Log("Indicator is " + indicator);
                break;
            case 1:
                //Belirteç adı.
                break;
            case 2:
                //Belirteç adı.
                break;
        }
    }

    private void Fire()
    {
        switch(indicator)
        {
            case "Standart":
                ShootStandartBullet();
                break;
        }

    }

    private void ShootStandartBullet()
    {
        var bullet = Instantiate(standartBullet, firingPoint.position, Quaternion.identity);
        StandartBullet bulletScript = bullet.GetComponent<StandartBullet>();
        bulletScript.SetTarget(target);
        bulletScript.setCreator(gameObject);
        bulletScript.setDamage(damage);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "MainRoadEnemy")
        {
            //Düşmana hasar vur.
            Destroy(this);
        }
    }

}
