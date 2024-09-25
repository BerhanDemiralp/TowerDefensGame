using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class AoE_Damage : MonoBehaviour
{
    [Header("Refences")]
    [SerializeField] private GameObject fireCrackerSpawner;
    
    [Header("Attributes")]
    [SerializeField] private float damage = 0;
    [SerializeField] private float FirecrackerDamage = 0;
    [SerializeField] private float areaRadius = 5;
    private GameManager gameManager;

    public GameObject creator;
    public BomberBullet creatorScript;
    private int blueBlock = 0;
    private int killCount = 0;
    private int level = 1;
    
    private GameObject FcSpawner;
    private firecrackerSpawner FcSpawnerScript;
    private float timeUntilDestroy = 0f;
    private Bomber bomber;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if(!gameManager.getTime())
        {
            timeUntilDestroy += Time.deltaTime;
            transform.localScale += new UnityEngine.Vector3(areaRadius * Time.deltaTime, areaRadius * Time.deltaTime, 0);

            if(killCount >= 3)
            {
                UpgradeNextShot();
            }
        
            if(timeUntilDestroy >= 0.25f)
            {
                if(level == 3){ScSpawner();}
                Destroy(gameObject);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "MainRoadEnemy")
        {
            MainRoadEnemy targerScript = other.GetComponent<MainRoadEnemy>();
            targerScript.DealDamage(damage);
            if(targerScript.hitPoints <=0){UpgradeNextShot();}
            Debug.Log(damage + " damage dealt!");
        }
        if(other.gameObject.tag == "SideRoadEnemy")
        {
            SideRoadEnemy targerScript = other.GetComponent<SideRoadEnemy>();
            targerScript.DealDamage(damage);
            if(targerScript.hitPoints <=0){UpgradeNextShot();}
            Debug.Log(damage + " damage dealt!");
        }
    }

    public void SetDamage(float _damage, int _bluBlock)
    {
        damage = _damage;
        blueBlock = _bluBlock;
    }

    public void SetAreaRadius(float _areaRadius)
    {
        areaRadius = _areaRadius;
    }

    public void SetFromTower(GameObject tower)
    {
        creator = tower;
        bomber = tower.GetComponent<Bomber>();
    }

    public void SetFirecrackerDamage(float _damage)
    {
        FirecrackerDamage = _damage;
    }

    public void SetLevel(int _level)
    {
        level = _level;
    }

    public void UpgradeNextShot()
    {
        bomber.IsUpgraded();
    }


    private void ScSpawner()
    {
        FcSpawner = Instantiate(fireCrackerSpawner, transform.position, transform.rotation);
        FcSpawnerScript = FcSpawner.GetComponent<firecrackerSpawner>();
        FcSpawnerScript.SetDamage(damage, blueBlock);
        FcSpawnerScript.SetCreator(creator);
        FcSpawnerScript.SetAreaRadius(areaRadius);
    }

    private void SetCreator(GameObject _creator)
    {
        creatorScript = _creator.GetComponent<BomberBullet>();
    }

    
    

    
    
}
