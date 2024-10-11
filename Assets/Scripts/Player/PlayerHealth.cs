using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static EventController;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    public bool loseHealth;
    private int trueHealth = 3;
    private int maxHealth = 1000;
    private int currentHealth = 1000;
    private float startTime = 0.05f; //time between 1hp draining
    private float timer;

    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text maxHealthText;

    bool hasBeenRaised;//so Death wont be raised during multiple frames

    //FOR TRUE HEALTH
    public Image[] hearts;
    private Sprite heartFill;

    private void UpdateTrueHealthUI()
    {
        for(int i = 0; i< hearts.Length; i++)
        {
            if(i<trueHealth)
            {
                hearts[i].color = new Color32(125, 69, 42, 255);
                Debug.Log("R truH: " + trueHealth + "  i: " + i);
            }
            else
            {
                hearts[i].color = new Color32(216, 121, 43, 255);
                Debug.Log("G truH: " + trueHealth + "  i: " + i);
            }
        }
        
    }

    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
        set 
        {
            int change = maxHealth - value;
            SpawnMaxHealthChangeText(change);
            maxHealth = value;
            UpdateHealthSlider();
            UpdateHealthBar();
            UpdateHealthText();
            UpdateMaxHealthText();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        loseHealth = true;
        EventController.onDeath += Death;
        EventController.onHPClick += AddHealth;
        EventController.onRespawn += ResetPlayerHealth;
    }

    // Update is called once per frame
    void Update() //VARA I FIXED UPDATE?
    {
        if (loseHealth && currentHealth > 0)
        {
            //lose health
            Timer();
        }

        if (currentHealth <= 0)
        {
            //player die
            if(hasBeenRaised == false)
            {
                EventController.RaiseOnDeath(); //SHOULD ONLY HAPPEN ONCE
                hasBeenRaised = true;
            }
        }

        if (currentHealth > maxHealth) //should not be able to get over max health
        {
            currentHealth = maxHealth;
            UpdateHealthBar();
            UpdateHealthText();
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
        {
            currentHealth = maxHealth;
            return;
        }
        currentHealth+= 10;
        UpdateHealthBar();
        UpdateHealthText();
    }

    void MinusHealth()
    {
        currentHealth--;
        UpdateHealthBar();
        UpdateHealthText();
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

    private void UpdateHealthText()
    {
        healthText.text = currentHealth.ToString();
    }

    public void UpdateMaxHealthText()
    {
        maxHealthText.text = maxHealth.ToString();
    }

    private void UpdateHealthSlider()
    {
        healthSlider.maxValue = maxHealth;
    }

    private void SpawnMaxHealthChangeText(int change)
    {
        var offset = new Vector3(-400,200,0);
        TMP_Text tempTextBox = Instantiate(Resources.Load<TMP_Text>("PopupHealthText"), transform.position + offset, transform.rotation);//, Input.mousePosition - (offset / 2), transform.rotation) as TMP_Text; //Camera.main.ScreenToWorldPoint(Input.mousePosition)
        //Parent to the panel 
        tempTextBox.transform.SetParent(GameObject.Find("Canvas").transform, false);

        //change text
        if (change > 0)
        {
            tempTextBox.text = "-" + change + "max"; //parameter f�r int amount changed
        }
        else
            tempTextBox.text = "+" + System.Math.Abs(change) + "max"; //parameter f�r int amount changed


        //tempTextBox.color = Color.red;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            TakeDamage(other.gameObject.GetComponent<Fire>().damageDealt);
            //Invoke("FireDamage", 0.5f);
        }
    }

    private void FireDamage()
    {
        currentHealth -= GetComponent<Fire>().damageDealt;
        MinusHealth();

        if (GetComponent<PlayerHealth>().currentHealth <= 0)
        {
            Death();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthBar();
        UpdateHealthText();

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        trueHealth--;
        if (trueHealth < 0)
        {
            //GAMEOVER
            //switch scene to main menu
            SceneManager.LoadScene(0);
        }
        UpdateTrueHealthUI();
    }

    private void ResetPlayerHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        UpdateHealthText();
        hasBeenRaised = false;
    }

}
