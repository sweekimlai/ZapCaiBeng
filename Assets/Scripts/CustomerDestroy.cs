using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDestroy : MonoBehaviour
{
    [SerializeField] CustomerCommands CustomerCommands;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
