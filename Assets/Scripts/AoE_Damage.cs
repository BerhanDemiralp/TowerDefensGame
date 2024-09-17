using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class AoE_Damage : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float damage = 0;
    [SerializeField] private float areaRadius = 5;
    
    
    private float timeUntilDestroy = 0f;

    
    
    

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilDestroy += Time.deltaTime;
        transform.localScale += new UnityEngine.Vector3(areaRadius * Time.deltaTime, areaRadius * Time.deltaTime, 0);

        if(timeUntilDestroy >= 0.25f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "MainRoadEnemy" || other.gameObject.tag == "SideRoadEnemy")
        {
        MainRoadEnemy targerScript = other.GetComponent<MainRoadEnemy>();
        targerScript.DealDamage(damage);
        Debug.Log(damage + " damage dealt!");
        }
             
    }

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }
    
}
