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
        StartCoroutine(WaitTimeBeforeStart());        
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
                notFound = false;
            }
        }

        return nextSceneName;
        //foreach (var i in allLevelDict)
        //{
        //    Debug.Log(string.Format("Key is: {0}, Value is: {1}", i.Key, i.Value));           
       // }
    }

    IEnumerator WaitTimeBeforeStart()
    {
        string nextScene = GetNextScene();
        yield return new WaitForSeconds(loadLevelTime);
        SceneManager.LoadScene(nextScene);
    }
}
