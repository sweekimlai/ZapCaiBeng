using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerList : MonoBehaviour
{
    [SerializeField] List<Customer> allCustomers;
    
    private List<Customer> ShuffleCustomers()
    {
        /* shuffle customers and return it as a list type */
        List<Customer> shuffledCustomers = new List<Customer>();
        for (int i = 0; i < allCustomers.Count; i++)
        {
            bool found = true;
            while(found)
            {
                found = false;
                int rnd = Random.Range(0, 10);
                Customer customer = allCustomers[rnd];
                if (!shuffledCustomers.Contains(customer))
                {
                    shuffledCustomers.Add(customer);
                }
                else
                {
                    found = true;
                }
            }
        }

        return shuffledCustomers;
    }

    public List<Customer> GetAllCustomers()
    {        
        return ShuffleCustomers();
    }
}
