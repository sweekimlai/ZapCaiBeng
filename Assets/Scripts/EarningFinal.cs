using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EarningFinal : MonoBehaviour
{
    Text earningFinalText;

    void Start()
    {
        earningFinalText = GetComponent<Text>();
        earningFinalText.text = string.Format("You Earned ${0}", GameSession.gameSession.Earnings);
    }

}
