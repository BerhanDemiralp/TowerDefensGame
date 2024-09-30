using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShredderBullet : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float lifeTime = 10f;


    private float timeUntilDestroy = 0f;
    public float damage = 100f;
    private float speed = 2f;
    private Rigidbody2D rb;
    private Transform target;
    public Vector2 directedSpeed;
    public GameManager gameManager;

    private int hitCount = 0;

    private int level = 1;
    private int redBlock;
    private int blueBlock;
    private int greenBlock;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameManager.GetTime())
        {
            transform.localRotation *= new Quaternion(0,0,Time.deltaTime,1);
            timeUntilDestroy += Time.deltaTime;
            if(timeUntilDestroy >= lifeTime)
            {
                Destroy(gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        if(!gameManager.GetTime())
        {
            rb.velocity = directedSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Edge")
        {
            if(other.transform.position.x == 0)
            {
                directedSpeed = new Vector2(directedSpeed.x ,directedSpeed.y * -1);
            }else if(other.transform.position.y == 0)
            {
                directedSpeed = new Vector2(directedSpeed.x * -1,directedSpeed.y);
            }
        }

        if(other.gameObject.tag == "MainRoadEnemy")
        {
            MainRoadEnemy targerScript = other.GetComponent<MainRoadEnemy>();
            targerScript.DealDamage(damage);
            Debug.Log(damage + " damage dealt!");
            hitCount++;
            if(hitCount >= 3 && level < 2)
            {
                Destroy(gameObject);
            }

        }
        if(other.gameObject.tag == "SideRoadEnemy")
        {
            SideRoadEnemy targerScript = other.GetComponent<SideRoadEnemy>();
            targerScript.DealDamage(damage);
            Debug.Log(damage + " damage dealt!");
            hitCount++;
            if(hitCount >= 3 && level < 2)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetDirection(Vector3 position)
    {
        directedSpeed = position.normalized * speed;
    }

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }

    public void SetLegos(int redBlockTemp, int blueBlockTemp, int greenBlockTemp)
    {
        redBlock = redBlockTemp;
        blueBlock = blueBlockTemp;
        greenBlock = greenBlockTemp;
    }

    public void SetLevel(int _level)
    {
        level = _level;
    }
}
