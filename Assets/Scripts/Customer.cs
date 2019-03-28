using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{    
    [SerializeField] float speed = 10.0f;
    [SerializeField] GameObject speechBubble;
    [SerializeField] GameObject foodList;

    bool startMoving = false;
    GameObject currentSpeechBubble;
    GameObject foodOrderGroup;
    float foodIconScale = 0.4f;

    private void Start()
    {
        foodOrderGroup = new GameObject("foodOrderGroup");
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

    public List<GameObject> GetCurrentOrder()
    {
        /*
            Return all food child gameobjects as list
        */
        if (!foodOrderGroup) { return null; }

        List<GameObject> currentOrder = new List<GameObject>();
        for (int i = 0; i < foodOrderGroup.transform.childCount; i++)
        {
            currentOrder.Add(foodOrderGroup.transform.GetChild(i).gameObject);
        }
        return currentOrder;
    }

    public void DestroyFoodOrder()
    {
        /*
            Destroy speechBubble and all child objects under foodOrder gameobject
        */
        Destroy(currentSpeechBubble.gameObject);
        Destroy(foodOrderGroup.gameObject);
    }

    public void ShowFoodOrder()
    {
        /*
            Find top left corner location of current customer 
            then offset the speech bubble from it.
            Add food icons based on the speechbubble location
            then offset from it.
        */        
        currentSpeechBubble = Instantiate(speechBubble, new Vector2(
            transform.GetChild(0).position.x, transform.GetChild(0).position.y), 
            Quaternion.identity) as GameObject;

        List<Food> randomFood = foodList.GetComponent<FoodList>().GetFood(3);
        int childIndex = 0;
        foreach (Food food in randomFood)
        {
            Food newFood = Instantiate(food, new Vector2(
                currentSpeechBubble.transform.GetChild(childIndex).position.x, 
                currentSpeechBubble.transform.GetChild(childIndex).position.y), 
                Quaternion.identity) as Food;
            newFood.transform.localScale = new Vector3(foodIconScale, foodIconScale, 0.0f);
            newFood.transform.parent = foodOrderGroup.transform;
            childIndex += 1;
        }
    }
}
