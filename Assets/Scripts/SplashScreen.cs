using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] float splashScreenTime = 1.0f;

    void Start()
    {
        StartCoroutine(WaitTimeBeforeStart());
    }

    IEnumerator WaitTimeBeforeStart()
    {
        yield return new WaitForSeconds(splashScreenTime);
        SceneManager.LoadScene("LevelLoader");
    }
}
