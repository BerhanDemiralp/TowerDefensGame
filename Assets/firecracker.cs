using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firecracker : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float lifeTime = 0.5f;
    [SerializeField] public float damage = 0;
    private GameObject creator;
    private MainRoadEnemy targerScript;
    private firecrackerSpawner creatorScript;
    private float timeUntilDestroy = 0;
    private float areaRadius = 0.4f;
    private Vector3 vector3;
    void Start()
    {
        transform.localScale = new Vector3(0.1f,0.1f,1f);
        vector3 = new Vector3(0.002f,0.002f,0);
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilDestroy += Time.deltaTime;
        transform.localScale += vector3;

        if(timeUntilDestroy >= lifeTime){Destroy(gameObject);}
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "MainRoadEnemy" || other.gameObject.tag == "SideRoadEnemy")
        {
            targerScript = other.GetComponent<MainRoadEnemy>();
            targerScript.DealDamage(damage);
            //if(targerScript.hitPoints <= 0){creatorScript.IncreaseKillCount();}
            Debug.Log(damage + " damage dealt!");
            Destroy(gameObject);
        }
             
    }

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }
    
    public void SetCreator(GameObject _creator)
    {
        creator = _creator;
        creatorScript = creator.GetComponent<firecrackerSpawner>();
    }

    public void SetAreaRadius(float _areaRadius)
    {
        areaRadius = _areaRadius*0.02f/4f;
    }
}
