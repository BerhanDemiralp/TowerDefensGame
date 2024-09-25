using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class MainRoadEnemy : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] public float hitPoints = 100;
    [SerializeField] public float enemyCount = 0;
    [SerializeField] private Movement movement;

    private float _speed;
    private float damageAmplifier = 0;
    private  GameObject gameManager;
    private GameManager gameManagerScript;
    
    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    public void SetHitPoints(float _hitPoints)
    {
        hitPoints = _hitPoints;
    }

    public void SetCount(int _enemyCount)
    {
        enemyCount = _enemyCount;
    }

    public void DealDamage(float damage)
    {
        hitPoints -= damage * damageAmplifier;
        Debug.Log(damage + " taken!");
        if(hitPoints <= 0)
        {
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

    
    
}
