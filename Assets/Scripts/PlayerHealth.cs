using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public bool loseHealth;
    private int maxHealth = 1000;
    private int currentHealth = 1000;
    private float startTime = 0.05f; //time between 1hp draining
    private float timer;

    [SerializeField] private Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        loseHealth = true;
        EventController.onDeath += Test;
        EventController.onHPClick += AddHealth;
    }
    private void Test()
    {
    }

    // Update is called once per frame
    void Update() //VARA I FIXED UPDATE?
    {
        if (loseHealth)
        {
            //lose health
            Timer();
        }

        if(currentHealth <= 0)
        {
            //player die
            EventController.RaiseOnDeath();
        }
    }

    private void Timer()
    {
        timer -= Time.deltaTime;
        if(timer <= 0.0f)
        {
            MinusHealth();
            timer = startTime;
        }
    }

    public void AddHealth()
    {
        if (currentHealth >= maxHealth)
            return;
        currentHealth+= 10;
        UpdateHealthBar();
    }

    void MinusHealth()
    {
        currentHealth--;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = currentHealth;

        /*
        if (currentHealth >= 2)
        {
            fillColor.color = Color.white;
        }
        else
        {
            fillColor.color = redHealth;
        }
        */
    }


}
