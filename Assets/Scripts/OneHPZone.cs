using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneHPZone : MonoBehaviour
{
    int originalMaxHealth, originalCurrentHealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            originalMaxHealth = collision.GetComponent<PlayerHealth>().MaxHealth;
            originalCurrentHealth = collision.GetComponent<PlayerHealth>().CurrentHealth;
            collision.GetComponent<PlayerHealth>().MaxHealth = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().MaxHealth = originalMaxHealth;
            //need to raise current health to the previous one
            while(collision.GetComponent<PlayerHealth>().CurrentHealth < originalCurrentHealth)
            {
                collision.GetComponent<PlayerHealth>().AddHealth();
            }
        }
    }
}
