using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Movement : MonoBehaviour
{
    public GameObject road;

    public Transform target;
    public float speed = 3f;
    public Transform paths;

    private Tween tween;
    private float speedTemp = 0;
    public List<Vector3> vectorList = new List<Vector3>();
    private bool slowed;

    void Start()
    {
        road = GameObject.Find("Path");
        paths = GameObject.Find("Path").transform;

        vectorList = new List<Vector3>(); // Liste �evirilme amac� anl�k olarak vectore ekleme ��karma yapabilme

        foreach (Transform path in paths)
        {
            vectorList.Add(path.position);
        }
        //Debug.Log(vectorList.Count);

        tween = target.DOPath(vectorList.ToArray(), speed, PathType.CatmullRom, PathMode.TopDown2D)
            .SetUpdate(UpdateType.Fixed)
            .SetSpeedBased()
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo);
        speedTemp = tween.timeScale;
    }
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.M))
        {
            SpeedDown();
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            SpeedUp();
        }*/
    }

    public void SetSpeed(float speedAmplifier)
    {
        Debug.Log("Speed was " + tween.timeScale);
        tween.timeScale = speedTemp * speedAmplifier/100;
        Debug.Log("Speed is " + tween.timeScale);
    }
    public void SetSpeedDefault()
    {
        tween.timeScale = speedTemp ;
    }    
}
