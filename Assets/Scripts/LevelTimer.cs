using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [Tooltip("Our level timer in seconds")]
    float levelTime;
    bool timesUp = false;

    private void Start()
    {
        setupLevelTime();
    }

    private void setupLevelTime()
    {
        GameSession.gameSession.levelTime -= GameSession.gameSession.levelTimeDecremnt;
        levelTime = GameSession.gameSession.levelTime;
        Debug.Log(string.Format("Level Time is {0}", levelTime));
    }

    void Update()
    {
        if (timesUp) { return; }
        //Normalise the time value between 0 -1 
        GetComponent<Slider>().value = Time.timeSinceLevelLoad / levelTime;       

        if(Time.timeSinceLevelLoad >= levelTime)
        {
            timesUp = true;
            Debug.Log("Times Up Game Over");
            SceneManager.LoadScene("GameOverScreen");
        }
    }
}
