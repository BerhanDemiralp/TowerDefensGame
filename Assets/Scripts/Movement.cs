using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Movement : MonoBehaviour
{
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

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        target = transform;
        
        if(gameObject.tag == "MainRoadEnemy"){road = GameObject.Find("Path");paths = GameObject.Find("Path").transform;}
        if(gameObject.tag == "SideRoadEnemy"){road = GameObject.Find("SidePath");paths = GameObject.Find("SidePath").transform;}

        vectorList = new List<Vector3>(); // Liste �evirilme amac� anl�k olarak vectore ekleme ��karma yapabilme

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
        defaultSpeed = tween.timeScale;
        StopTime();
        StartTime();
    }

    /*private void Update()
    {
        //vectorList[]
    }*/

    public void SetSpeed(float speedAmplifier)
    {
        tween.timeScale = defaultSpeed * ((100 - speedAmplifier)/100);
    }

    public void SetSpeedDefault()
    {
        tween.timeScale = defaultSpeed ;
    }    

    IEnumerator EndOfRoad()
    {
        yield return tween.WaitForCompletion();
        if(gameObject.tag == "MainRoadEnemy"){gameManager.HpLost(1);}
        if(gameObject.tag == "SideRoadEnemy"){gameManager.AddLego(1);}
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
}
