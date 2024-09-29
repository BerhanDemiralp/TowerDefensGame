using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class legoGainEffect : MonoBehaviour
{
    private float timeUntilDestroy = 0f;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        transform.Rotate(new Vector3(0,0,0));
    }

    void Update()
    {
        timeUntilDestroy += Time.deltaTime;
        transform.localScale += new Vector3(timeUntilDestroy*-0.015f,timeUntilDestroy*-0.015f,0);
        transform.position += new Vector3(0,Time.deltaTime * 1f,0);
        if(timeUntilDestroy >= 0.6f)
        {
            Destroy(gameObject);
        }
    }
}
