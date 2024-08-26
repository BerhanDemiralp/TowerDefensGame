using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int realBlueBlockCount;
    public int realGreenBlockCount;
    public int realRedBlockCount;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }




}