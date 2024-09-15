using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("References")]
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject[] mainRoadEnemies;

    private GameObject summon;
    private float timeUntilNextSpawn = 0f;

    public int enemyCount = 0;

    public int BlueBlockCount;
    public int GreenBlockCount;
    public int RedBlockCount;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    
    private void Update()
    {
        timeUntilNextSpawn += Time.deltaTime;

        if(timeUntilNextSpawn >= 3)
        {
            summon = Instantiate(mainRoadEnemies[0]);
            MainRoadEnemy summonScript = summon.GetComponent<MainRoadEnemy>();
            enemyCount++;
            summonScript.setHitPoints(50);
            Debug.Log("Enemy summoned with " + summonScript.hitPoints + "hit points!");
            timeUntilNextSpawn = 0;
        }
    }




}