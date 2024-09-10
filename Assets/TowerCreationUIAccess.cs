using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCreationUIAccess : MonoBehaviour
{
    public CreationSystem creationSystem;

    public void OnMouseUp()
    {
        creationSystem.openUI();
    }
}
