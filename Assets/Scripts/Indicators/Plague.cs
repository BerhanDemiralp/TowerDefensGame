using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
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

    private Transform indicator;
    private float timeUntilFire = 10;

    private float speedMultiplier = 0;
    private float slowTime = 0.5f;
    private float damageAmplifier = 0;
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
        if(gameManager.getTime())
        {
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
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, (Vector2)transform.position, 0f, enemyMask);
        if(hits.Length > 0){
            foreach (RaycastHit2D hit in hits)
            {
                if(hit.transform.tag == "MainRoadEnemy")
                {
                    target = hit.transform.gameObject;
                    MainRoadEnemy targetScript = target.GetComponent<MainRoadEnemy>();
                    targetScript.SetSpeed(speedMultiplier, slowTime);
                    Debug.Log(target.name + " slowed!");
                    if(level >= 2)
                    {
                    targetScript.SetDamageAmplifier(damageAmplifier, damageAmplifierTime);
                    Debug.Log(target.name + " damaged!");
                    }
                }
                if(hit.transform.tag == "SideRoadEnemy")
                {
                    SideRoadEnemy targetScript = target.GetComponent<SideRoadEnemy>();
                    targetScript.SetSpeed(speedMultiplier, slowTime);
                    Debug.Log(target.name + " slowed!");
                    if(level >= 2)
                    {
                    targetScript.SetDamageAmplifier(damageAmplifier, damageAmplifierTime);
                    Debug.Log(target.name + " damaged!");
                    }
                }
                
                
            }
            
        }
    }
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, range);

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

    private void SetStats()
    {
        speedMultiplier = 30f + (blueBlock * 50f/24f);
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
