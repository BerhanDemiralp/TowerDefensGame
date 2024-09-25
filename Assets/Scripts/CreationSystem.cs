using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreationSystem : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject[] allEnemies;
    private GameObject[] allTowers;

    private bool isUIon = false;
    private bool canPlace;
    private bool isColliding = false;

    private int blue_block_count;
    private int green_block_count;
    private int red_block_count;
    private int indicatorType = 0;
    private int block_count = 0;
    private int towerLevel = 1;

    private Color color = Color.white;

    //Her kule seviyesi için gerekli lego sayıları
    private const int LEVEL1 = 6;
    private const int LEVEL2 = 12;
    private const int LEVEL3 = 24;

    public TextMeshProUGUI blueBlockCount;
    public TextMeshProUGUI greenBlockCount;
    public TextMeshProUGUI redBlockCount;
    public TextMeshProUGUI indicatorText;

    public Button myButton;

    public GameObject creationPanel;
    public GameObject towerOnMouseObj;
    public GameObject tower;
    private GameObject towerOnMouseTemp;
    private GameObject towerTemp;

    void Start()
    {
        SetDefaults();
        indicatorType = 0;
        creationPanel.transform.position += new Vector3(0, -400, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("canPlace is " + canPlace);
        
        /*if (canPlace && Input.GetMouseButtonDown(0))
        {
            CreateTower(red_block_count, blue_block_count, green_block_count, indicatorType, color);
            Destroy(towerOnMouseTemp);
            StartCoroutine(WaitForNextTowerCreation());
            SetDefaults();
        }*/

        if (canPlace && Input.GetMouseButtonDown(0))
        {
            isColliding = towerOnMouseTemp.GetComponent<TowerImageOnMouse>().isColliding;
            if (!isColliding)
            {
                CreateTower(red_block_count, blue_block_count, green_block_count, indicatorType, color);
                Destroy(towerOnMouseTemp);
                StartCoroutine(WaitForNextTowerCreation());
                SetDefaults();
                canPlace= false;
            }
            else
            {
                
            }
            
        }
    }

    public void CreateTower(int _redBlockCount, int _blueBlockCount, int _greenBlockCount, int _indicatorType, Color _color)
    {
        towerTemp = Instantiate(tower, towerOnMouseTemp.transform.position, towerOnMouseTemp.transform.rotation);
        towerTemp.GetComponent<Tower>().SetLegos(_redBlockCount, _blueBlockCount, _greenBlockCount);
        towerTemp.GetComponent<Tower>().SetIndicator(_indicatorType);
    }

    public void ChangeIndicator()
    {
        switch(indicatorType){
            case 0:
                indicatorText.text = "Bomber";
                color = Color.red;
                break;
            case 1:
                indicatorText.text = "Gunner";
                color = Color.black;
                break;
            case 2:
                indicatorText.text = "Shredder";
                break;
            case 3:
                indicatorText.text = "Plague";
                break;
            case 4:
                indicatorText.text = "Standart";
                color = Color.white;
                indicatorType = -1;
                break;
        }
        indicatorType++;
    }
    private void ClearCounts()
    {
        blue_block_count= 0;
        green_block_count= 0;
        red_block_count= 0;
    }
    private bool AddControl()
    {
        AddBlocks();
        if(gameManager.blockCount <= 0){return false;}
        if (block_count == LEVEL3) {return false;}
        return true;
    }
    private bool RemoveControl(int block_count)
    {
        if (block_count == 0) { return false; }
        return true;
    }

    public void AddRedBlock()
    {
        if (AddControl())
        {
            red_block_count++;
            gameManager.RemoveLego(1);
            SetBlockTexts();
        }
    }
    public void AddBlueBlock()
    {
        if (AddControl())
        {
            blue_block_count++;
            gameManager.RemoveLego(1);
            SetBlockTexts();
        }
    }
    public void AddGreenBlock()
    {
        if (AddControl())
        {
            green_block_count++;
            gameManager.RemoveLego(1);
            SetBlockTexts();
        }
    }

    public void RemoveBlueBlock()
    {
        if (RemoveControl(blue_block_count))
        {
            blue_block_count--;
            gameManager.AddLego(1);
            SetBlockTexts();
        }
    }
    public void RemoveGreenBlock()
    {
        if (RemoveControl(green_block_count))
        {
            green_block_count--;
            gameManager.AddLego(1);
            SetBlockTexts();
        }
    }
    public void RemoveRedBlock()
    {
        if (RemoveControl(red_block_count))
        {
            red_block_count--;
            gameManager.AddLego(1);
            SetBlockTexts();
        }
    }
    public void AddBlocks()
    {block_count = blue_block_count + red_block_count + green_block_count;}

    public void CreateTowerButton()
    {
        if(!canPlace){
            AddBlocks();
            switch (block_count){
                case LEVEL1:
                    towerLevel = 1;
                    CloseUI();
                    TowerOnMouse();
                    break;
                case LEVEL2:
                    towerLevel = 2;
                    CloseUI();
                    TowerOnMouse();
                    break;
                case LEVEL3:
                    towerLevel = 3;
                    CloseUI();
                    TowerOnMouse();
                    break;
                default:
                    Debug.Log("Towers must have 6, 12 or 24 blocks!");
                    break;
            }
        }
        
        
    }

    public void OpenUI()
    {
        if(!isUIon && !canPlace){
            creationPanel.transform.position += new Vector3(0, 400, 0);
            StopTime();
            isUIon = true;
        }
    }

    public void CloseUI()
    {
        if(isUIon && !canPlace){
            creationPanel.transform.position += new Vector3(0, -400, 0);
            StartTime();
            isUIon = false;
        }
    }

    private void SetBlockTexts()
    {   
        blueBlockCount.text = blue_block_count.ToString();
        greenBlockCount.text = green_block_count.ToString();
        redBlockCount.text = red_block_count.ToString();
    }

    public void TowerOnMouse()
    {
        towerOnMouseTemp = Instantiate(towerOnMouseObj);
        canPlace = true;
    }
    
    public void SetDefaults()
    {
        ClearCounts();
        SetBlockTexts();
        indicatorType = 0;
        indicatorText.text = "Standart";
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        color = Color.white;
    }

    public void StopTime()
    {
        gameManager.GetComponent<GameManager>().StopTime();
        StopTimeForEnemies();
        StopTimeForTowers();
    }

    public void StartTime()
    {
        gameManager.GetComponent<GameManager>().StartTime();
        StartTimeForEnemies();
        StartTimeTowers();
    }

    public void StopTimeForEnemies()
    {
        allEnemies = null;
        var mainRoadEnemies = GameObject.FindGameObjectsWithTag("MainRoadEnemy");
        var sideRoadEnemies = GameObject.FindGameObjectsWithTag("SideRoadEnemy");
        allEnemies = mainRoadEnemies.Concat(sideRoadEnemies).ToArray();
        foreach (GameObject gameObject in allEnemies)
        {
            gameObject.GetComponent<Movement>().StopTime();
            
        }
    }

    public void StartTimeForEnemies()
    {
        foreach (GameObject gameObject in allEnemies)
        {
            gameObject.GetComponent<Movement>().StartTime();
        }
    }
    
    public void StopTimeForTowers()
    {
        allTowers = null;
        allTowers = GameObject.FindGameObjectsWithTag("Tower");
        foreach (GameObject gameObject in allTowers)
        {
            gameObject.GetComponent<Tower>().StopTime();
            
        }
    }

    public void StartTimeTowers()
    {
        foreach (GameObject gameObject in allTowers)
        {
            gameObject.GetComponent<Tower>().StartTime();
        }
    }

    private IEnumerator WaitForNextTowerCreation()
    {
        yield return new WaitForSeconds(0.1f);
        canPlace = false;
    }


    
    



}
