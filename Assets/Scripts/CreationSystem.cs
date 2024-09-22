using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreationSystem : MonoBehaviour
{
    private bool isUIon = false;
    private bool canPlace;

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
    GameObject towerTemp;

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

        if (canPlace && Input.GetMouseButtonDown(0))
        {
            CreateTower(red_block_count, blue_block_count, green_block_count, indicatorType, color);
            Destroy(towerOnMouseTemp);
            StartCoroutine(WaitForNextTowerCreation());
            SetDefaults();
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
        green_block_count= 6;
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
            SetBlockTexts();
        }
    }
    public void AddGreenBlock()
    {
        if (AddControl())
        {
            green_block_count++;
            SetBlockTexts();
        }
    }
    public void AddRedBlock()
    {
        if (AddControl())
        {
            red_block_count++;
            SetBlockTexts();
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log("OnPointerDown called.");
    }

    public void RemoveBlueBlock()
    {
        if (RemoveControl(blue_block_count))
        {
            blue_block_count--;
            SetBlockTexts();
        }
    }
    public void RemoveGreenBlock()
    {
        if (RemoveControl(green_block_count))
        {
            green_block_count--;
            SetBlockTexts();
        }
    }
    public void RemoveRedBlock()
    {
        if (RemoveControl(red_block_count))
        {
            red_block_count--;
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
            isUIon = true;
        }
    }

    public void CloseUI()
    {
        if(isUIon && !canPlace){
            creationPanel.transform.position += new Vector3(0, -400, 0);
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

    public void SetCanPlace(bool _canPlace)
    {
        canPlace = _canPlace;
    }
    
    public void SetDefaults()
    {
        ClearCounts();
        SetBlockTexts();
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
