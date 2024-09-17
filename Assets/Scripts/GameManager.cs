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

    [Header("References")]
    
    public int maxEnemyCount = 10;
    public float spawnCooldown = 3f;
    public int enemyCount = 0;
    public int enemyCountTemp = 0;

    public int blockCount = 30;
    public int PlayerHp = 10;

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

        if(timeUntilNextSpawn >= spawnCooldown && enemyCountTemp < maxEnemyCount)
        {
            summon = Instantiate(mainRoadEnemies[0]);
            MainRoadEnemy summonScript = summon.GetComponent<MainRoadEnemy>();
            enemyCount++;
            enemyCountTemp++;
            summonScript.SetHitPoints(50f);
            summonScript.SetCount(enemyCount);
            Debug.Log("Enemy summoned with " + summonScript.hitPoints + "hit points!");
            timeUntilNextSpawn = 0;
        }
    }




}