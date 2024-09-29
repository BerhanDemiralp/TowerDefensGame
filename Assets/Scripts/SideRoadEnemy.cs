using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class SideRoadEnemy : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] public float hitPoints = 100;
    [SerializeField] public float enemyCount = 0;
    [SerializeField] private Movement movement;
    [SerializeField] private GameObject legoGainEffect;

    private float _speed;
    private float damageAmplifier = 1;
    private SpriteRenderer spriteRenderer;
    private Color defaultColor;

    private  GameObject gameManager;
    private GameManager gameManagerScript;
    private bool canDO = true;
    
    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
    }

    public void SetHitPoints(float _hitPoints)
    {
        hitPoints = _hitPoints;
        Debug.Log("Health set!");
    }

    public void SetCount(int _enemyCount)
    {
        enemyCount = _enemyCount;
    }

    public void DealDamage(float damage)
    {
        hitPoints -= damage * damageAmplifier;
        StartCoroutine(SetDamageColor());
        Debug.Log("Current hp is " + hitPoints);
        if(hitPoints <= 0)
        {
            Debug.Log("Gonna die!");
            Instantiate(legoGainEffect, transform.position, transform.rotation);
            gameManagerScript.AddLego(1);
            Debug.Log("Effect on!");
            gameManagerScript.enemyCountTemp--;
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float _speedMultiplier, float _time)
    {
        movement.speed = _speed;
        StartCoroutine(SetSpeedDefault(_speedMultiplier,_time));
    }
    public void SetDamageAmplifier(float _damageAmplifier, float _time)
    {
        StartCoroutine(DamageAmplifier(_damageAmplifier, _time));
    }   

    IEnumerator SetSpeedDefault(float _speedMultiplier,float _time)
    {
        movement.SetSpeed(_speedMultiplier);
        yield return new WaitForSeconds(_time);
        movement.SetSpeedDefault();
    }

    IEnumerator DamageAmplifier(float _damageAmplifier, float _time)
    {
        damageAmplifier = (100 + _damageAmplifier)/100;
        yield return new WaitForSeconds(_time);
        damageAmplifier = 1;
    }

    IEnumerator SetDamageColor()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = defaultColor;
    }
    
    
}
