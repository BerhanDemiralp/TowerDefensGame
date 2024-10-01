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
    private const int maxIndicatorCount = 5;
    private const string indicator00 = "Standart";
    private const string indicator01 = "Bomber";
    private const string indicator02 = "Gunner";
    private const string indicator03 = "Shredder";
    private const string indicator04 = "Plague";



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

    private Color color;
    private Color colorChild;

    //Her kule seviyesi için gerekli lego sayıları
    private const int LEVEL1 = 6;
    private const int LEVEL2 = 24;
    private const int LEVEL3 = 60;

    public TextMeshProUGUI blueBlockCount;
    public TextMeshProUGUI greenBlockCount;
    public TextMeshProUGUI redBlockCount;
    public TextMeshProUGUI indicatorTextUp;
    public TextMeshProUGUI indicatorText;
    public TextMeshProUGUI indicatorTextDown;

    public GameObject creationPanel;
    private Image spriteRenderer;
    private Image spriteRendererChild;
    public GameObject towerOnMouseObj;
    public GameObject tower;
    private GameObject towerOnMouseTemp;
    private GameObject towerTemp;

    void Start()
    {
        creationPanel.SetActive(false);
        spriteRenderer = creationPanel.transform.GetChild(2).GetComponent<Image>();
        spriteRendererChild = creationPanel.transform.GetChild(2).GetChild(0).GetComponent<Image>();
        color = spriteRenderer.color;
        colorChild = spriteRendererChild.color;
        SetDefaults();
        indicatorType = 0;
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

    public void UpIndicator()
    {
        indicatorType++;
        if (indicatorType == maxIndicatorCount)
        {
            indicatorType= 0;
        }
        SwitchIndicatorText(indicatorType);
    }
    public void DownIndicator()
    {
        indicatorType--;
        if (indicatorType < 0)
        {
            indicatorType = maxIndicatorCount - 1;
        }
        SwitchIndicatorText(indicatorType);
    }
    private void SwitchIndicatorText(int _indicatorType)
    {
        switch (_indicatorType)
        {
            case 0:
                indicatorTextUp.text = indicator01;
                indicatorText.text = indicator00;
                indicatorTextDown.text = indicator04;
                color = Color.white;
                break;
            case 1:
                indicatorTextUp.text = indicator02;
                indicatorText.text = indicator01;
                indicatorTextDown.text = indicator00;

                color = Color.red;
                break;
            case 2:
                indicatorTextUp.text = indicator03;
                indicatorText.text = indicator02;
                indicatorTextDown.text = indicator01;

                color = Color.black;
                break;
            case 3:
                indicatorTextUp.text =  indicator04;
                indicatorText.text = indicator03;
                indicatorTextDown.text = indicator02;
                break;
            case 4:
                indicatorTextUp.text = indicator00;
                indicatorText.text = indicator04;
                indicatorTextDown.text = indicator03;
                break;
        }
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
    {
        block_count = blue_block_count + red_block_count + green_block_count;
    }

    public void CreateTowerButton()
    {
        if(!canPlace){
            AddBlocks();
            if(block_count >= LEVEL1)
            {
                if(block_count >= LEVEL1 && block_count < LEVEL2)
                {
                    towerLevel = 1;
                }else if(block_count >= LEVEL2 && block_count < LEVEL3)
                {
                    towerLevel = 2;
                }else if(block_count == LEVEL3)
                {
                    towerLevel = 3;
                }
                CloseUI();
                TowerOnMouse();
            }else{Debug.Log("Towers must have at least 6 blocks!");}
            
        }
            
        
        
    }

    public void OpenUI()
    {
        if(!isUIon && !canPlace){
            creationPanel.SetActive(true);
            StopTime();
            isUIon = true;
        }
    }

    public void CloseUI()
    {
        if(isUIon && !canPlace){
            creationPanel.SetActive(false);
            StartTime();
            isUIon = false;
        }
    }

    private void SetBlockTexts()
    {   
        AddBlocks();
        blueBlockCount.text = blue_block_count.ToString();
        greenBlockCount.text = green_block_count.ToString();
        redBlockCount.text = red_block_count.ToString();
        /*if(block_count == 6 || block_count == 12 || block_count == 24)
        {
            spriteRenderer.color = Color.green;
            spriteRendererChild.enabled = true;
        }
        else{spriteRenderer.color = Color.red; spriteRendererChild.enabled = false;}*/
    }
    //new Color(212,207,159)
    public void TowerOnMouse()
    {
        towerOnMouseTemp = Instantiate(towerOnMouseObj);
        canPlace = true;
    }
    
    public void SetDefaults()
    {
        ClearCounts();
        SetBlockTexts();
        SwitchIndicatorText(indicatorType);
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
