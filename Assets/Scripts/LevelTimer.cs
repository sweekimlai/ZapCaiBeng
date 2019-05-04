using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [Tooltip("Our level timer in seconds")]
    [SerializeField] float levelTime = 50;
    bool timesUp = false;

    // Update is called once per frame
    void Update()
    {
        if (timesUp) { return; }
        //Normalise the time value between 0 -1 
        GetComponent<Slider>().value = Time.timeSinceLevelLoad / levelTime;
        
        if(Time.timeSinceLevelLoad >= levelTime)
        {
            timesUp = true;
            Debug.Log("Times Up Game Over");
        }
    }
}
