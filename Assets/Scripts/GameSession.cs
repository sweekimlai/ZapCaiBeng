using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public string[] allLevels = new string[3];
    public static GameSession gameSession { get; private set; }
    public float Earnings { get; set; }

    public float levelTime = 100.0f;
    public float levelTimeDecremnt = 10.0f;
    public int errorCount = 0;
    public Dictionary<string, int> allLevelDict = new Dictionary<string, int>();

    private void Awake()
    {
        if (gameSession == null)
        {
            gameSession = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        foreach (string level in allLevels)
        {
            allLevelDict.Add(level, 0);
        }
    }

    public void ResetGameSession()
    {
        foreach (string level in allLevels)
        {
            allLevelDict[level] = 0;
        }
        Earnings = 0f;
        errorCount = 0;
        levelTime = 100.0f;
    }
}
