using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreationSystem : MonoBehaviour
{
    private int blue_block_count;
    private int green_block_count;
    private int red_block_count;
    private string indicatorType;
    private int block_count = 0;

    //Her kule seviyesi için gerekli lego sayıları
    private const int LEVEL1 = 6;
    private const int LEVEL2 = 12;
    private const int LEVEL3 = 24;

    public TextMeshProUGUI blueBlockCount;
    public TextMeshProUGUI greenBlockCount;
    public TextMeshProUGUI redBlockCount;

    public Button myButton;

    void Start()
    {
        clearCounts();
        

    }

    // Update is called once per frame
    void Update()
    {
        blueBlockCount.text = blue_block_count.ToString();
        greenBlockCount.text = green_block_count.ToString();
        redBlockCount.text = red_block_count.ToString();
    }

    private void clearCounts()
    {
        blue_block_count= 0;
        green_block_count= 0;
        red_block_count= 0;
    }
    private bool addControl()
    {
        addBlocks();
        if (block_count == LEVEL3) { return false; }
        //Buraya gerek olmadığı için deaktive ettim.
        /*switch (type)
        {
            case "blue":
                if (blue_block_count == GameManager.instance.BlueBlockCount) { return false; }
                break;
            case "green":
                if (green_block_count == GameManager.instance.GreenBlockCount) { return false; };
                break;
            case "red":
                if (red_block_count == GameManager.instance.RedBlockCount) { return false; };
                break;
        }*/
            
        return true;
    }

    public void selectIndicator(string indicator_type)
    {
        indicatorType = indicator_type;
    }
    private bool removeControl(int block_count)
    {
        if (block_count == 0) { return false; }
        return true;
    }
    public void addBlueBlock()
    {
        if (addControl())
        {
            Debug.Log("Blue");
            blue_block_count++;
            
        }
    }
    public void addGreenBlock()
    {
        if (addControl())
        {
            Debug.Log("Green");
            green_block_count++;
            
        }
    }
    public void addRedBlock()
    {
        if (addControl())
        {
            Debug.Log("Red");
            red_block_count++;
            
        }
    }
    public void removeBlueBlock()
    {
        if (removeControl(blue_block_count))
        {
            blue_block_count--;
        }
    }
    public void removeGreenBlock()
    {
        if (removeControl(green_block_count))
        {
            green_block_count--;
        }
    }
    public void removeRedBlock()
    {
        if (removeControl(red_block_count))
        {
            red_block_count--;
        }
    }
    public void addBlocks()
    {block_count = blue_block_count + red_block_count + green_block_count;}

    public void createTower()
    {
        addBlocks();
        switch (block_count){
            case LEVEL1:
                Debug.Log("Wow you have a Level 1 Tower!"); 
                break;
            case LEVEL2:
                Debug.Log("Wow you have a Level 2 Tower!");
                break;
            case LEVEL3:
                Debug.Log("Wow you have a Level 3 Tower!");
                break;
            default:
                Debug.Log("Towers must have 6, 12 or 24 blocks!");
                break;
    }
        
    }

}
