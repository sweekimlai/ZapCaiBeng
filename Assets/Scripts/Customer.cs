using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{    
    [SerializeField] float speed = 10.0f;
    [SerializeField] GameObject speechBubble;
    [SerializeField] GameObject foodList;

    float moveUpSpeed = 3.0f;

    Animator customerAnimator;

    public enum status {WAIT,SERVE,LEAVE};

    public bool StartMoving { get; set; }

    public float MoveTargetLocation { get; set; }

    public status CustomerStatus { get; set; }
   
    public CustomerCommands CustomerCommands { get; set; }

    public CustomerList CustomerList { get; set; }

    private void Start()
    {
        StartMoving = false;
        CustomerStatus = status.WAIT;
        MoveTargetLocation = 0.0f;
        customerAnimator = transform.Find("Body").gameObject.GetComponent<Animator>();
    }

    private void Update()
    {       
        /* If StartMoving is enabled, move left till target location is reached
         then check the staus. If is Serve, meaning that is the current customer.
         Will need to display the food order in speech bubble. If is Wait, 
         meaning the customer still in queue and just moved up in queu. Passing this 
         customer object to figure out which customer queue behind.
         */
        if(StartMoving)
        {
            if(transform.position.x > MoveTargetLocation)
            {                
                if (CustomerStatus == status.WAIT)
                {
                    transform.Translate(Vector2.left * moveUpSpeed * Time.deltaTime);
                }
                else
                {
                    transform.Translate(Vector2.left * speed * Time.deltaTime);
                }
                customerAnimator.SetBool("StartWalk", true);
            }
            else
            {
                StartMoving = false;
                customerAnimator.SetBool("StartWalk", false);
                if (CustomerStatus == status.SERVE)
                {
                    CustomerCommands.ShowFoodOrder();
                }
                else if (CustomerStatus == status.WAIT)
                {                    
                    CustomerList.MoveCustomerUpInQueue(this);
                }
            }
        }
    }
}
