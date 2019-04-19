using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerList : MonoBehaviour
{
    [SerializeField] Customer[] allCustomerArray = new Customer[10];

    private Customer[] ShuffeleCustomerArray(int customerCount)
    {
        Customer[] shuffledCustomerArray = new Customer[customerCount];
        for(int i = 0; i < customerCount; i++)
        {
            bool found = true;
            while(found)
            {
                found = false;
                int rnd = Random.Range(0, 10);
                Customer customer = allCustomerArray[rnd];
                if(!shuffledCustomerArray.Contains(customer))
                {
                    shuffledCustomerArray[i] = customer;
                }
                else
                {
                    found = true;
                }
            }
        }
        return shuffledCustomerArray;
    }

    public Customer[] GetAllCustomerArray(int customerCount)
    {
        return ShuffeleCustomerArray(customerCount);
    }
}
