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
    int numberOfOrder = 3;

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
        /* Return all food child gameobjects under foodOrderGroup as list */
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
        /* Destroy speechBubble and foodOrderGroup gameobject
        along with all its children gameobjects */
        Destroy(currentSpeechBubble.gameObject);
        Destroy(foodOrderGroup.gameObject);
    }
    
    public void ShowFoodOrder()
    {
        /* Every Customer gameobject has a child gameobject indicating
        the position of the speech bubble. SpeechBubble object will 
        spawn at thie location. The SpeechBubble has three child
        gameobjects indicating the locations of the spawned food 
        gameobjects. At the moment the number of foods, three, is
        hardcoded */

        foodOrderGroup = new GameObject("foodOrderGroup");
        currentSpeechBubble = Instantiate(speechBubble, transform.GetChild(0).position, Quaternion.identity) as GameObject;

        List<Food> randomFood = foodList.GetComponent<FoodList>().GetFood(numberOfOrder);
        int childIndex = 0;
        foreach (Food food in randomFood)
        {
            Food newFood = Instantiate(food,currentSpeechBubble.transform.GetChild(childIndex).position, 
                Quaternion.identity) as Food;
            newFood.transform.localScale = new Vector3(foodIconScale, foodIconScale, 0.0f);
            newFood.transform.parent = foodOrderGroup.transform;
            childIndex += 1;
        }
    }
}
