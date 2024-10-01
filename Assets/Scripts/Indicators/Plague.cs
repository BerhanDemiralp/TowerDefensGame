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
using Unity.VisualScripting;

public class Plague : MonoBehaviour
{

    private const string BULLETNAME = "StandartBullet";


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

    protected GameObject target;
    protected Quaternion targetRotation;
    protected SpriteRenderer this_SpriteRenderer;

    private SideRoadEnemy sideTargetScript;
    private MainRoadEnemy mainTargetScript;

    private Transform indicator;
    private float timeUntilFire = 10;

    public float speedMultiplier = 0;
    private float slowTime = 0.5f;
    public float damageAmplifier = 0;
    private float damageAmplifierTime = 0.5f;
    private int level = 1;
    private int totalBlockCount = 0;
    private int redBlock;
    private int blueBlock;
    private int greenBlock;
    

    void Start()
    {
        BaseSetter();
    }

    void Update()
    {
        Debug.Log("Update!");
        if(!gameManager.GetTime())
        {
            Debug.Log(gameManager.GetTime());
            timeUntilFire += Time.deltaTime;
            if(timeUntilFire >= 0.1f)
            {
            FindTarget();
            timeUntilFire = 0;
            return;
            
            }
        }
        
    }
    private void FindTarget()
    {
        Debug.Log("Finding target!");
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, (Vector2)transform.position, 0f, enemyMask);
        if(hits.Length > 0){
            foreach (RaycastHit2D hit in hits)
            {
                Debug.Log("Enemy found!");
                target = hit.transform.gameObject;
                if(hit.transform.tag == "MainRoadEnemy")
                {
                    Debug.Log("MainRoadEnemy found!");
                    
                    mainTargetScript = target.GetComponent<MainRoadEnemy>();
                    mainTargetScript.SetSpeed(speedMultiplier, slowTime);
                    Debug.Log(target.name + " slowed!");
                    if(level >= 2)
                    {
                    mainTargetScript.SetDamageAmplifier(damageAmplifier, damageAmplifierTime);
                    Debug.Log(target.name + " damaged!");
                    }
                }else if(hit.transform.tag == "SideRoadEnemy")
                {
                    Debug.Log("SideRoadEnemy found!");
                    sideTargetScript = target.GetComponent<SideRoadEnemy>(); 
                    sideTargetScript.SetSpeed(speedMultiplier, slowTime);
                    Debug.Log(target.name + " slowed!");
                    if(level >= 2)
                    {
                    sideTargetScript.SetDamageAmplifier(damageAmplifier, damageAmplifierTime);
                    Debug.Log(target.name + " damaged!");
                    }
                }
                
                
            }
            
        }
    }
    #if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, range);
    }
    #endif

    public void SetBlocks(int redBlockTemp, int blueBlockTemp, int greenBlockTemp)
    {
        redBlock = redBlockTemp;
        blueBlock = blueBlockTemp;
        greenBlock = greenBlockTemp;
        totalBlockCount = redBlock + blueBlock + greenBlock;
        SetLevel();
        SetStats();
        
    }

    private void SetStats()
    {
        speedMultiplier = 20f + (blueBlock * 30f/24f);
        slowTime = 0.5f + (blueBlock * 1/48);
        damageAmplifier = 20f + (redBlock * 30f/24f);
        damageAmplifierTime = 0.5f + (redBlock * 1/48);
        range = 2 + (greenBlock * 0.22f);
        if(level == 3){range *= 5/4;}
    }

    private void SetLevel()
    {
        switch(totalBlockCount){
            case 6:
                level = 1;
                break;
            case 12:
                level = 2;
                break;
            case 24:
                level = 3;
                break;
        }
    }
    
    public void BaseSetter()
    {
        SetBullet(BULLETNAME);
        SetColor(Color.white);
        timeUntilFire = 10f;
        turretRotationPoint = gameObject.transform.GetChild(0);
        indicator = gameObject.transform.GetChild(0).GetChild(0);
        firingPoint = gameObject.transform.GetChild(0).GetChild(0).GetChild(0);
        enemyMask = LayerMask.GetMask("Enemy");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Destroy(firingPoint.gameObject);
        Destroy(indicator.gameObject);
        Destroy(turretRotationPoint.gameObject);
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
