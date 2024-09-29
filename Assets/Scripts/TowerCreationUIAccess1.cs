using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCreationUIAccess1s : MonoBehaviour
{
    public CreationSystem creationSystem;
    public void OnMouseUp()
    {
        creationSystem.OpenUI();
    }
}
