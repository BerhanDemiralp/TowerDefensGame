using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;
using System.IO;
using Unity.Mathematics;
using Quaternion = UnityEngine.Quaternion;

public class Movement : MonoBehaviour
{
    public const float pi = 3.1415f;
    public GameObject road;
    private GameManager gameManager;
    public Transform target;
    public float speed = 3f;
    public Transform paths;

    private Tween tween;
    private float defaultSpeed = 0;
    private float speedTemp = 0;
    public List<Vector3> vectorList = new List<Vector3>();
    private bool slowed;
    private Vector3 lastPosition = new Vector3(0,0,0);
    private Vector3 rotationAngel = new Vector3(0,0,0);
    private Vector3 targetDirection;
    private Vector3 zero = new Vector3(0,0,0);
    private float timeUntilRotate = 0;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        target = transform;
        
        if(gameObject.tag == "MainRoadEnemy"){road = GameObject.Find("Path");paths = GameObject.Find("Path").transform;}
        if(gameObject.tag == "SideRoadEnemy"){road = GameObject.Find("SidePath");paths = GameObject.Find("SidePath").transform;}

        vectorList = new List<Vector3>(); // Liste �evirilme amac� anl�k olarak vectore ekleme ��karma yapabilme
        //transform.DORotate(new Vector3(0,0,30f), speed, RotateMode.FastBeyond360);

        foreach (Transform path in paths)
        {
            vectorList.Add(path.position);  
        }
        

        tween = target.DOPath(vectorList.ToArray(), speed, PathType.CatmullRom, PathMode.TopDown2D)
            .SetUpdate(UpdateType.Fixed)
            .SetSpeedBased()
            .SetEase(Ease.Linear)
            .SetLoops(0, LoopType.Yoyo);
        
        StartCoroutine(EndOfRoad());
        tween.timeScale = 0.5f;
        defaultSpeed = tween.timeScale;
        StopTime();
        StartTime();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    /*private void Update()
    {
        if(!gameManager.timeStopped)
        {
            timeUntilRotate += Time.deltaTime;
            if(timeUntilRotate >= 0.0033)
            { 
                
                //rotationAngel = new UnityEngine.Vector3(0,0, Mathf.Atan(GetTan(transform.position, lastPosition))*180/pi);
                // Debug.Log(rotationAngel);
                //transform.localRotation *= new Quaternion(0,0, rotationAngel.z, 1f);
                //new Vector3(0,0,Vector3.Angle(transform.position, lastPosition)* 10f)
                //Debug.Log(Vector3.Angle(new Vector3(25,25,25), new Vector3(25,50,50)));

                // Determine which direction to rotate towards
                rotationAngel = transform.position - lastPosition;
                targetDirection = new Vector3(0,0,Mathf.Atan2(rotationAngel.y, rotationAngel.x));
                

                //targetDirection = lastPosition - transform.position;

                // The step size is equal to speed times frame time.
                //float singleStep = speed * Time.deltaTime;

                // Rotate the forward vector towards the target direction by one step
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 360, 0.0f);

                // Draw a ray pointing at our target in
                //Debug.DrawRay(transform.position, newDirection, Color.red);

                // Calculate a rotation a step closer to the target and applies rotation to this object
                transform.rotation = Quaternion.LookRotation(newDirection);

                lastPosition = transform.position;
                timeUntilRotate = 0;
                
            
        }
        
    }
    }*/

    /*private void Update()
    {
        //vectorList[]
    }*/

    public void SetSpeed(float speedAmplifier)
    {
        if(!gameManager.GetTime())
        {
        tween.timeScale = defaultSpeed * ((100 - speedAmplifier)/100);
        }
    }

    public void SetSpeedDefault()
    {
        if(!gameManager.GetTime())
        {
        tween.timeScale = defaultSpeed ;
        }
    }    

    IEnumerator EndOfRoad()
    {
        yield return tween.WaitForCompletion();
        if(gameObject.tag == "MainRoadEnemy"){gameManager.HpLost(1);}
        Destroy(gameObject);
    }

    public void StopTime()
    {
        if(tween != null)
        {
        speedTemp = tween.timeScale;
        tween.timeScale = 0f;
        }
    }

    public void StartTime()
    {
        if(tween != null)
        {
        tween.timeScale = speedTemp;
        }
    }

    private float GetTan(Vector3 x, Vector3 y)
    {
        return (x.y - y.y) / (x.x - y.x);
    }
}
