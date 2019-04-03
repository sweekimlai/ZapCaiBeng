using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{    
    [SerializeField] float speed = 10.0f;
    [SerializeField] GameObject speechBubble;
    [SerializeField] GameObject foodList;

    bool startMoving = false;
    bool served = false;

    public void StartMoving()
    {
        startMoving = true;
    }

    public void StopMoving()
    {
        startMoving = false;
    }

    public bool IsServed()
    {
        return served;
    }

    public void SetServeStatus(bool status)
    {
        served = status;
    }

    private void Update()
    {
        if(startMoving)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }
}
