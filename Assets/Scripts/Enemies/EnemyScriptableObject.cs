using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MainRoadEnemy",menuName ="Enemies/MainRoadEnemy",order =1)]
public class EnemyScriptableObject : ScriptableObject
{
    public string type;
    public float _speed;
    public float damageAmplifier = 1;
    public float hitPoints = 100;
    public float possibility;
    public Color color;
}
