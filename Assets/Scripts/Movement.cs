using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Movement : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    public Transform paths;

    private Tween tween;
    public List<Vector3> vectorList = new List<Vector3>();
    private bool slowed;

    void Start()
    {
        vectorList = new List<Vector3>(); // Liste çevirilme amacý anlýk olarak vectore ekleme çýkarma yapabilme

        foreach (Transform path in paths)
        {
            vectorList.Add(path.position);
        }
        Debug.Log(vectorList.Count);

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
