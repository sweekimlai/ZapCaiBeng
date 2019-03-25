using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{    
    [SerializeField] float speed = 10.0f;
    [SerializeField] GameObject speechBubble;

    bool startMoving = false;
    GameObject currentSpeechBubble;
    GameObject foodOrder;
    float speechOffset = 2.0f;
    float speechBubbleMargin = 0.1f;
    float foodPosGap = 1.2f;
    float foodIconScale = 0.4f;

    private void Start()
    {
        foodOrder = FindObjectOfType<CustomerCommands>().GetFoodOrder();
    }

    public void StartMoving()
    {
        startMoving = true;
    }

    public void StopMoving()
    {
        startMoving = false;
    }

    private void Update()
    {
        if(startMoving)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    public void DestroyFoodOrder()
    {
        /*
            Destroy speechBubble and all child objects under foodOrder gameobject
        */
        Destroy(currentSpeechBubble.gameObject);
        foodOrder.GetComponent<FoodOrder>().DestroyAllFoodOrder();
    }

    public void ShowFoodOrder()
    {
        /*
            Find top left corner location of current customer 
            then offset the speech bubble from it.
            Add food icons based on the speechbubble location
            then offset from it.
        */
        float xpos = GetComponent<SpriteRenderer>().bounds.min.x - speechOffset;
        float ypos = GetComponent<SpriteRenderer>().bounds.max.y;
        currentSpeechBubble = Instantiate(speechBubble, new Vector2(xpos, ypos), Quaternion.identity) as GameObject;        

        List<Food> randomFood = FindObjectOfType<FoodList>().GetFood(3);
        float foodPosX = currentSpeechBubble.GetComponent<SpriteRenderer>().bounds.min.x + speechBubbleMargin;
        foreach (Food food in randomFood)
        {
            Food newFood = Instantiate(food, new Vector2(foodPosX, ypos - speechBubbleMargin), Quaternion.identity) as Food;            
            newFood.transform.localScale = new Vector3(foodIconScale, foodIconScale, 0.0f);
            newFood.transform.parent = foodOrder.transform;
            foodPosX += foodPosGap;
        }
    }
}
