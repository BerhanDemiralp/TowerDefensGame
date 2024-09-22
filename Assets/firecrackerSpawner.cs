using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class firecrackerSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject firecracker;
    
    [Header("Attributes")]
    [SerializeField] private float lifeTime = 2;
    [SerializeField] private float spawnRate = 10;
    [SerializeField] private float FirecrackerDamage = 10;

    private float areaRadius = 0.4f;
    private GameObject creator;
    private Bomber creatorScript;
    private GameObject firecrackerTemp;
    private firecracker fcScript;
    private float timeUntilDestroy = 0;
    private float timeUntilSpawn = 0;
    private int blueBlock = 0;

    private int killCount = 0;
    private bool canSpawn = true;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilDestroy += Time.deltaTime;
        timeUntilSpawn += Time.deltaTime;
        if(canSpawn && timeUntilSpawn >= 1f/spawnRate)
        {
            SummonFireCracker();
            timeUntilSpawn = 0;
        }
        spawnRate -= spawnRate*Time.deltaTime/2f;
        if(killCount >= 3)
        {
            //creator.Upgrade
        }
        if(timeUntilDestroy >= lifeTime){Destroy(gameObject);}
    }

    private void SummonFireCracker()
    {
        firecrackerTemp = Instantiate(firecracker, transform.position + new Vector3(Random.Range(-1f*areaRadius, areaRadius), Random.Range(-1f*areaRadius, areaRadius), 0), transform.rotation);
        fcScript = firecrackerTemp.GetComponent<firecracker>();
        fcScript.SetDamage(FirecrackerDamage);
        fcScript.SetCreator(gameObject);
        fcScript.SetAreaRadius(areaRadius);
    }

    public void SetDamage(float _damage, int _blueBlock)
    {
        //FirecrackerDamage = _damage/4f;
        blueBlock = _blueBlock;
    }

    public void SetCreator(GameObject _creator)
    {
        creator = _creator;
        creatorScript = creator.GetComponent<Bomber>();
    }

    public void SetAreaRadius(float _areaRadius)
    {
        areaRadius = _areaRadius * 0.4f / 5;
        spawnRate = (areaRadius *40) + (blueBlock / 2.4f);
    }
    
    public void IncreaseKillCount()
    {
        creatorScript.IncreaseKillCount();
    }

    
}
