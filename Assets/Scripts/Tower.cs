using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.IO.LowLevel.Unsafe;
using System.Diagnostics.Tracing;
using System.Diagnostics;
using Unity.VisualScripting;

public class Tower : MonoBehaviour
{
    [Header("Legos")] 
    [SerializeField] private int blueBlock;
    [SerializeField] private int greenBlock;
    [SerializeField] private int redBlock;

    protected Transform target;
    protected Quaternion targetRotation;
    protected SpriteRenderer this_SpriteRenderer;


    private string indicator;

    private void Start()
    {
        //gameObject.AddComponent<Standart>();
    }

    public void SetLegos(int red, int blue, int green)
    {
        redBlock = red;
        blueBlock = blue;
        greenBlock = green;
    }

    public void SetIndicator(int _indicator)
    {
        switch(_indicator){
            case 1:
                Bomber BomberScript = gameObject.AddComponent<Bomber>();
                BomberScript.SetBlocks(redBlock, blueBlock, greenBlock);
                break;
            case 2:
                Gunner gunnerScript = gameObject.AddComponent<Gunner>();
                gunnerScript.SetBlocks(redBlock, blueBlock, greenBlock);
                break;
            default:
                Standart standartScript = gameObject.AddComponent<Standart>();
                standartScript.SetBlocks(redBlock, blueBlock, greenBlock);
                break;
        }
    }







}
