using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private int blueBlock;
    private int greenBlock;
    private int redBlock;

    private string indicator;

    public Tower(int blue_block_count,int green_block_count,int red_block_count,string indicator_type)
    {
        blueBlock= blue_block_count;
        greenBlock= green_block_count;
        redBlock= red_block_count;
        indicator= indicator_type;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
