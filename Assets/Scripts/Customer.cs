using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{    
    [SerializeField] float speed = 10.0f;
    [SerializeField] GameObject speechBubble;
    [SerializeField] GameObject foodList;

    public enum status {WAIT,SERVE,LEAVE};

    CustomerCommands custormerCommands;
    bool startMoving = false;
    float moveTargetLocation = 0.0f;
    status customerStatus = status.WAIT;

    public bool StartMoving { get; set; }

    public float MoveTargetLocation { get; set; }

    public status CustomerStatus { get; set; }
   
    public CustomerCommands CustomerCommands { get; set; }

    private void Update()
    {
        if(StartMoving)
        {
            if(transform.position.x > MoveTargetLocation)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);                
            }
            else
            {
                StartMoving = false;
                CustomerCommands.ShowFoodOrder();
            }
        }
    }
}
