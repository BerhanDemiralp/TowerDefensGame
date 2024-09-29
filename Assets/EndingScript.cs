using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScene : MonoBehaviour
{
    public void StartingScene()
    {
        SceneManager.LoadScene("StartingScene");
    }
    public void StartFirstLevel()
    {
        SceneManager.LoadScene("Towers");
    }
}
