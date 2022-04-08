using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Destroy the game object with which a collision occurred (the Enemy, not the Collector)
            Destroy(collision.gameObject);
        }
    }
}
