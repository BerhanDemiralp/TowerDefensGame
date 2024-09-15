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

        tween = target.DOPath(vectorList.ToArray(), 3f, PathType.CatmullRom, PathMode.TopDown2D)
            .SetUpdate(UpdateType.Fixed)
            .SetSpeedBased()
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SpeedDown();
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            SpeedUp();
        }
    }

    void SpeedDown()
    {
        Debug.Log("SpeedUp");
        tween.timeScale = tween.timeScale / 2;
    }
    void SpeedUp()
    {
        Debug.Log("SpeedDown");
        tween.timeScale = tween.timeScale *2;
    }    
}
