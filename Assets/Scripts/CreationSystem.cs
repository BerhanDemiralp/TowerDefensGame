using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreationSystem : MonoBehaviour
{
    private bool isUIon = false;
    private bool canPlace = false;

    private int blue_block_count;
    private int green_block_count;
    private int red_block_count;
    private int indicatorType = 0;
    private int block_count = 0;

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
    GameObject towerTemp;

    void Start()
    {
        ClearCounts();
        indicatorType = 0;
        creationPanel.transform.position += new Vector3(0, -400, 0);
    }

    // Update is called once per frame
    void Update()
    {
        blueBlockCount.text = blue_block_count.ToString();
        greenBlockCount.text = green_block_count.ToString();
        redBlockCount.text = red_block_count.ToString();

        Debug.Log(canPlace);

        if (Input.GetMouseButtonDown(0))
        {
            if(canPlace)
            {
                towerTemp = Instantiate(tower, towerOnMouseTemp.transform.position, towerOnMouseTemp.transform.rotation);
                towerTemp.GetComponent<Tower>().SetBlocks(red_block_count, blue_block_count, green_block_count);
                towerTemp.GetComponent<Tower>().SetIndicator(indicatorType);
                towerTemp.GetComponent<Tower>().SetColor(color);
                Destroy(towerOnMouseTemp);
                StartCoroutine(WaitForNextTowerCreation());
                SetDefaults();
            } 
        } 
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
                indicatorText.text = "Dulandar";
                break;
            case 3:
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
        if (block_count == LEVEL3) { return false; }
        return true;
    }
    private bool RemoveControl(int block_count)
    {
        if (block_count == 0) { return false; }
        return true;
    }
    public void AddBlueBlock()
    {
        if (AddControl())
        {
            blue_block_count++;
        }
    }
    public void AddGreenBlock()
    {
        if (AddControl())
        {
            green_block_count++;
        }
    }
    public void AddRedBlock()
    {
        if (AddControl())
        {
            red_block_count++;
        }
    }
    public void RemoveBlueBlock()
    {
        if (RemoveControl(blue_block_count))
        {
            blue_block_count--;
        }
    }
    public void RemoveGreenBlock()
    {
        if (RemoveControl(green_block_count))
        {
            green_block_count--;
        }
    }
    public void RemoveRedBlock()
    {
        if (RemoveControl(red_block_count))
        {
            red_block_count--;
        }
    }
    public void AddBlocks()
    {block_count = blue_block_count + red_block_count + green_block_count;}

    public void CreateTower()
    {
        if(!canPlace){
            AddBlocks();
            switch (block_count){
                case LEVEL1:
                    Debug.Log("Wow you have a Level 1 Tower!"); 
                    CloseUI();
                    TowerOnMouse();
                    break;
                case LEVEL2:
                    Debug.Log("Wow you have a Level 2 Tower!");
                    CloseUI();
                    TowerOnMouse();
                    break;
                case LEVEL3:
                    Debug.Log("Wow you have a Level 3 Tower!");
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
            isUIon = true;
            Debug.Log("Opening UI!");
        }
    }

    public void CloseUI()
    {
        if(isUIon && !canPlace){
            creationPanel.transform.position += new Vector3(0, -400, 0);
            isUIon = false;
            Debug.Log("Closing UI!");
        }
    }

    public void TowerOnMouse()
    {
        towerOnMouseTemp = Instantiate(towerOnMouseObj);
        canPlace = true;
    }
    
    public void SetDefaults()
    {
        ClearCounts();
        indicatorType = 0;
        indicatorText.text = "Standart";
        color = Color.white;
    }
    
    private IEnumerator WaitForNextTowerCreation()
    {
        yield return new WaitForSeconds(0.1f);
        canPlace = false;
    }    


    
    



}
