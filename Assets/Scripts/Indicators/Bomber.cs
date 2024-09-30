
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.IO.LowLevel.Unsafe;
using System.Diagnostics.Tracing;

public class Bomber : MonoBehaviour
{

    private const string BULLETNAME = "BomberBullet";

    [Header("References")]
    [SerializeField] protected Transform turretRotationPoint;
    [SerializeField] protected Transform firingPoint;
    [SerializeField] protected LayerMask enemyMask;
    [SerializeField] protected GameObject bullet;
    private GameManager gameManager;
    
    
    
    [Header("Attribute")]
    [SerializeField] protected int level = 1;
    [SerializeField] protected float damage = 0;
    [SerializeField] protected float attackSpeed = 2; 
    [SerializeField] protected float range = 5f;
    [SerializeField] protected float rotationSpeed = 500f;
    [SerializeField] protected bool canShoot = true;

    public Tower creatorScript;

    protected Transform target;
    protected Quaternion targetRotation;
    protected SpriteRenderer this_SpriteRenderer;

    private float timeUntilFire = 10;
    private bool isUpgraded = false;
    private int totalBlockCount = 0;
    private int killCount = 0;
    private float damageUpgrade = 1.5f;
    private float areaUpgrade = 1.8f;

    private bool timeStopped = false;

    private int redBlock;
    private int blueBlock;
    private int greenBlock;
    

    void Start()
    {
        BaseSetter();
    }

    void Update()
    {
        if(!gameManager.GetTime())
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
            
                if(!timeStopped && timeUntilFire >= 1f / attackSpeed && CompareQuaternion(turretRotationPoint.rotation, targetRotation))
                {
                    Shoot();
                    timeUntilFire = 0;
                }
            
            }
        }
        
    }

    private void Shoot()
    {
        var _bullet = Instantiate(bullet, firingPoint.position, Quaternion.identity);
        BomberBullet bulletScript = _bullet.GetComponent<BomberBullet>();
        bulletScript.SetTarget(target);
        bulletScript.SetGameManager(gameManager);
        bulletScript.SetCreator(gameObject);
        bulletScript.SetDamage(damage, blueBlock, greenBlock);
        bulletScript.SetLevel(level);
        
        if(isUpgraded){bulletScript.Upgrade(1.5f,1.8f); isUpgraded = false;}
        killCount = 0;
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
    #if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, range);
    }
    #endif

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= range;
    }

    public void SetBlocks(int redBlockTemp, int blueBlockTemp, int greenBlockTemp)
    {
        redBlock = redBlockTemp;
        blueBlock = blueBlockTemp;
        greenBlock = greenBlockTemp;
        totalBlockCount = redBlock + blueBlock + greenBlock;
        SetLevel();
        SetStats();
    }

    private void SetLevel()
    {
        switch(totalBlockCount)
        {
            case 6:
                level = 1;
                damageUpgrade = 1.4f;
                areaUpgrade = 1.8f;
                break;
            case 12:
                level = 2;
                damageUpgrade = 1.4f + (redBlock * 16/240);
                areaUpgrade = 1.8f + (redBlock * 12/240);
                break;
            case 24:
                level = 3;
                damageUpgrade = 1.4f + (redBlock * 16/240);
                areaUpgrade = 1.8f + (redBlock * 12/240);
                break;
        }
    }

    private void SetStats()
    {
        damage = 12f + (redBlock* 4f);
        attackSpeed = 0.33f + (blueBlock * 0.10f);
        range = 2 + (greenBlock * 0.22f);
        Debug.Log(damage + " - " + attackSpeed + " - " + range);
    }

    
    public void BaseSetter()
    {
        SetBullet(BULLETNAME);
        SetColor(Color.red);
        creatorScript = gameObject.GetComponent<Tower>();
        timeUntilFire = 10f;
        turretRotationPoint = gameObject.transform.GetChild(0);
        firingPoint = gameObject.transform.GetChild(0).GetChild(0).GetChild(0);
        enemyMask = LayerMask.GetMask("Enemy");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void SetBullet(string bulletName)
    {
        string _bulletName = bulletName;
        bulletName = "Prefabs/Bullets/" + _bulletName;
        bullet = Resources.Load<GameObject>(bulletName);
    }

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
    
    public void IncreaseKillCount()
    {
        killCount++;
        Debug.Log(killCount);
        if(killCount >= 4){IsUpgraded(); killCount = 0;}
    }

    public void IsUpgraded()
    {
        isUpgraded = true;
        Debug.Log("isUpgraded = true");
    }
    public void StopTime(){timeStopped = true;}
    public void StartTime(){timeStopped = false;}

}