using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("References")]
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject[] mainRoadEnemies;
    [SerializeField] private GameObject[] sideRoadEnemies;
    [SerializeField] public TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI blockCountText;
    

    
    private GameObject summon;
    private GameObject sideSummon;
    private float time = 0f;
    private float timeUntilNextMainRoadSpawn = 0f;
    private float timeUntilNextSideRoadSpawn = 0f;

    [Header("Attributes")]
    public int maxEnemyCount = 10;
    public int mainRoadHitPoints = 50;
    public int sideRoadHitPoints = 50;
    public float mainRoadSpawnCooldown = 3f;
    public float sideRoadSpawnCooldown = 3f;
    public int enemyCount = 0;
    public int enemyCountTemp = 0;

    public int blockCount = 70;
    public int PlayerHp = 10;

    public bool timeStopped = false;

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
    
    private void Start()
    {
        healthText = GameObject.Find("HealthPoints").GetComponent<TextMeshProUGUI>();
        blockCountText = GameObject.Find("BlockCount").GetComponent<TextMeshProUGUI>();
        healthText.text = PlayerHp.ToString();
        blockCountText.text = blockCount.ToString();
        SummonMainRoadEnemy();
        SummonSideRoadEnemy();
    }
    
    private void Update()
    {
        time = Time.deltaTime;
        if(!timeStopped)
        {
            timeUntilNextMainRoadSpawn += Time.deltaTime;
            timeUntilNextSideRoadSpawn += Time.deltaTime;
            if(mainRoadSpawnCooldown >= 0.05 && !timeStopped){mainRoadSpawnCooldown -= 0.02f * Time.deltaTime;}
            if(sideRoadSpawnCooldown >= 0.4 && !timeStopped){sideRoadSpawnCooldown -= 0.005f * Time.deltaTime;}
        }
        //Debug.Log(timeUntilNextSpawn);
        if(!timeStopped && (timeUntilNextMainRoadSpawn >= mainRoadSpawnCooldown) && (enemyCountTemp < maxEnemyCount))
        {
            timeUntilNextMainRoadSpawn = 0;
            SummonMainRoadEnemy();
        }

        if(!timeStopped && (timeUntilNextSideRoadSpawn >= sideRoadSpawnCooldown) && (enemyCountTemp < maxEnemyCount))
        {
            timeUntilNextSideRoadSpawn = 0;
            SummonSideRoadEnemy();
        }

        if(time >= 30 && time <= 60){mainRoadHitPoints = 50; sideRoadHitPoints = 50;}
        if(time >= 60 && time <= 120){mainRoadHitPoints = 75; sideRoadHitPoints = 75;}
        if(time >= 120){mainRoadHitPoints = 100; sideRoadHitPoints = 100;}
    }

    public void StopTime()
    {
        timeStopped = true;
    }

    public void StartTime()
    {
        timeStopped = false;
    }

    public bool GetTime()
    {
        return timeStopped;
    }

    public void HpLost(int damage)
    {
        PlayerHp -= damage;
        healthText.text = PlayerHp.ToString();
        if(PlayerHp <= 0)
        {
            Debug.Log("Game Over!");
            SceneManager.LoadScene("EndingScene");
        }

    }

    public void AddLego(int howMany)
    {
        for (int i = 0; i < howMany; i++)
        {
            blockCount++;
            blockCountText.text = blockCount.ToString();
        }
    }

    public void RemoveLego(int howMany)
    {
        for (int i = 0; i < howMany; i++)
        {
            blockCount--;
            blockCountText.text = blockCount.ToString();
        }
    }

    private void SummonMainRoadEnemy()
    {
        summon = Instantiate(mainRoadEnemies[0]);
        MainRoadEnemy summonScript = summon.GetComponent<MainRoadEnemy>();
        summonScript.SetHitPoints(mainRoadHitPoints);
        summonScript.SetCount(enemyCount);
        enemyCount++;
        enemyCountTemp++;
    }

    private void SummonSideRoadEnemy()
    {
        sideSummon = Instantiate(sideRoadEnemies[0]);
        SideRoadEnemy sideSummonScript = sideSummon.GetComponent<SideRoadEnemy>();
        sideSummonScript.SetHitPoints(sideRoadHitPoints);
        sideSummonScript.SetCount(enemyCount);
        enemyCount++;
        enemyCountTemp++;
    }

    




}