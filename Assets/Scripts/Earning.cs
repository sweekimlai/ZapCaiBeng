using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Earning : MonoBehaviour
{
    [SerializeField] float increment = 5f;
    Text earningText;
    bool animate = false;
    float earningValue = 0f;
    float timeToWait = 0.1f;
    float intervel = 0.1f;


    void Start()
    {
        earningText = GetComponent<Text>();
        earningText.text = earningValue.ToString();
    }

    public void AddEarning()
    {
        earningValue += increment;
        GameSession.gameSession.Earnings += increment;
        animate = true;
    }

    private void Update()
    {
        if(animate)
        {
            earningIncrement();
        }
    }

    private void earningIncrement()
    {
        intervel -= Time.deltaTime;
        if(intervel <= 0f)
        {
            float currentEarning = int.Parse(earningText.text);
            if (currentEarning < earningValue)
            {
                currentEarning += 1f;                
                earningText.text = currentEarning.ToString();
                intervel = 0.1f;
            }
            else
            {
                animate = false;                
            }
        }
    }
}
