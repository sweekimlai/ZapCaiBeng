using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Error : MonoBehaviour
{
    // Start is called before the first frame update
    int errorCount;
    void Start()
    {
        errorCount = 0;
        setupErrorCount();
    }

    private void setupErrorCount()
    {
        for(int i = 0; i < GameSession.gameSession.errorCount; i++)
        {
            AddError();
        }
    }
    
    public void AddError()
    {
        errorCount += 1;
        Image[] errorImages = transform.GetComponentsInChildren<Image>();
        foreach (Image image in errorImages)
        {
            if(image.color.a != 1f)
            {
                image.color = new Color(1f, 1f, 1f, 1f);
                break;
            }            
        }

        if(errorCount >= 3)
        {
            ErrorGameOver();
        }
    }

    private void ErrorGameOver()
    {
        Debug.Log("Three Errors Game Over!");
    }
}
