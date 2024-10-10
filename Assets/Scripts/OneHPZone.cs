using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            DisableCookieClicker();

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
            EnableCookieClicker();
        }
    }

    private void DisableCookieClicker()
    {
        GameObject.Find("/CookieClicker/Canvas/Panel").GetComponent<Image>().enabled = false;
        GameObject.Find("/CookieClicker/Canvas/Button").GetComponent<Button>().enabled = false;
        GameObject.Find("/CookieClicker/Canvas/Button").GetComponent<Image>().enabled = false;
    }

    private void EnableCookieClicker()
    {
        GameObject.Find("/CookieClicker/Canvas/Panel").GetComponent<Image>().enabled = true;
        GameObject.Find("/CookieClicker/Canvas/Button").GetComponent<Button>().enabled = true;
        GameObject.Find("/CookieClicker/Canvas/Button").GetComponent<Image>().enabled = true;
    }
}
