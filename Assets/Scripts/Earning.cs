using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Earning : MonoBehaviour
{
    [SerializeField] int increment = 5;
    Text earningText;
    bool animate = false;
    int earningValue = 0;
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
            int currentEarning = int.Parse(earningText.text);
            if (currentEarning < earningValue)
            {
                currentEarning += 1;
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
