using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int BlueBlockCount;
    public int GreenBlockCount;
    public int RedBlockCount;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }




}