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




public class Gunner : MonoBehaviour
{

    private const string BULLETNAME = "GunnerBullet";

    [Header("References")]
    [SerializeField] protected Transform turretRotationPoint;
    [SerializeField] protected Transform firingPoint;
    [SerializeField] protected LayerMask enemyMask;
    [SerializeField] protected GameObject bullet;
    private GameManager gameManager;
    
    
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

    private int level = 0;
    private int redBlock;
    private int blueBlock;
    private int greenBlock;


    void Start()
    {
        BaseSetter();
    }

    void Update()
    {
        if(!gameManager.getTime())
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
        
    }

    private void Shoot()
    {
        var _bullet = Instantiate(bullet, firingPoint.position, Quaternion.identity);
        GunnerBullet bulletScript = _bullet.GetComponent<GunnerBullet>();
        bulletScript.SetTarget(target);
        bulletScript.SetCreator(gameObject);
        bulletScript.SetDamage(damage);
        bulletScript.SetLevel(level);
        bulletScript.SetLegos(redBlock, blueBlock, greenBlock);
        Debug.Log("Gunner bullet shot!");
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
        SetStats();
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
        SetColor(Color.blue);
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

}
