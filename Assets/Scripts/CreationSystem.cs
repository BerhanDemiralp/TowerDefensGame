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


    private int MAX_BLOCK_COUNT = 5;

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
    private bool addControl(int block_count,string type)
    {
        if (block_count >= MAX_BLOCK_COUNT) { return false; }
        switch (type)
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
        }
            
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
        if (addControl(blue_block_count,"blue"))
        {
            blue_block_count++;
        }
    }
    public void addGreenBlock()
    {
        if (addControl(green_block_count,"green"))
        {
            green_block_count++;
        }
    }
    public void addRedBlock()
    {
        if (addControl(red_block_count, "red"))
        {
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

}
