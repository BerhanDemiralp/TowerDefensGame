using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class MainRoadEnemy : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] public float hitPoints = 100;

    private  GameObject gameManager;
    private GameManager gameManagerScript;
    
    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        Debug.Log("My hp is " + hitPoints);
    }

    public void setHitPoints(float _hitPoints)
    {
        hitPoints = _hitPoints;
    }

    public void dealDamage(float damage)
    {
        hitPoints -= damage;
        Debug.Log("Current hp is " + damage);
        if(hitPoints <= 0)
        {
            gameManagerScript.enemyCount--;
            Destroy(gameObject);
        }
    }
}
