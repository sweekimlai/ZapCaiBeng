using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayAgain : MonoBehaviour
{
    public void Replay()
    {
        GameSession.gameSession.ResetGameSession();
        SceneManager.LoadScene("LevelLoader");
    }
}
