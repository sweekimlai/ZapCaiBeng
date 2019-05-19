//using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] float loadLevelTime = 1.0f;

    void Start()
    {
        DebugLevelDict();
        StartCoroutine(WaitTimeBeforeStart());        
    }

    private void DebugLevelDict()
    {
        foreach (var i in GameSession.gameSession.allLevelDict)
        {
            Debug.Log(string.Format("Key is: {0}, Value is: {1}", i.Key, i.Value));           
        }
    }

    private int GetSmallestOccuranceNumber(Dictionary<string,int> dict)
    {
        int smallestNumber = 999;
        foreach (var i in dict)
        {
            if(i.Value < smallestNumber)
            {
                smallestNumber = i.Value;
            }
        }
        return smallestNumber;
    }    

    private string GetNextScene()
    {
        Dictionary<string, int> allLevelDict = GameSession.gameSession.allLevelDict;
        string[] allScenes = GameSession.gameSession.allLevels;

        int smallestNumber = GetSmallestOccuranceNumber(allLevelDict);
        string nextSceneName = null;
        bool notFound = true;

        while(notFound)
        {
            int rnd = Random.Range(0, allScenes.Length);
            if(allLevelDict[allScenes[rnd]] == smallestNumber)
            {
                nextSceneName = allScenes[rnd];
                GameSession.gameSession.allLevelDict[nextSceneName] += 1;
                notFound = false;
            }
        }

        return nextSceneName;
    }

    IEnumerator WaitTimeBeforeStart()
    {
        string nextScene = GetNextScene();
        yield return new WaitForSeconds(loadLevelTime);
        SceneManager.LoadScene(nextScene);
    }
}
