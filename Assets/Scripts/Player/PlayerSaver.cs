using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaver : MonoBehaviour
{
    //Code from this article: https://www.sitepoint.com/saving-data-between-scenes-in-unity/ (accessed 30-09-2024)
    //Makes sure this script is not destoryed and keeps the same information between scenes
    public static PlayerSaver Instance;

    //Variables to be saved
    public Color32 playerColor = new Color32(192, 192, 192, 255);
    public bool hasAbilityDoubleJump = false;
    public bool hasAbilityDash = false;
    public bool hasAbilityShield = false;
    public int currentHealth = 0;
    public int maxHealth = 0;
    public int trueHealth = 0;
    public int bombCount = 0;

    public bool switchingBetweenLevelScenes = false;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    //USE IN PLAYER SCRIPTS

    /*    //for saving important player values
    public void SavePlayer()
    {
        BetweenScenesValueSaver.Instance.playerColor = (Color32)GetComponent<SpriteRenderer>().color;
        BetweenScenesValueSaver.Instance.hasAbilityDoubleJump = hasAbilityDoubleJump;
        BetweenScenesValueSaver.Instance.hasAbilityDash = hasAbilityDash;
        BetweenScenesValueSaver.Instance.hasAbilityDash = hasAbilityShield;
        BetweenScenesValueSaver.Instance.currentHealth = currentHealth;
        BetweenScenesValueSaver.Instance.maxHealth = maxHealth;
        BetweenScenesValueSaver.Instance.trueHealth = trueHealth;
        BetweenScenesValueSaver.Instance.bombCount = bombCount;
    }

    //for loading player values
    private void LoadPlayer()
    {
        GetComponent<SpriteRenderer>().color = BetweenScenesValueSaver.Instance.playerColor;
        hasAbilityDoubleJump = BetweenScenesValueSaver.Instance.hasAbilityDoubleJump;
        hasAbilityDash = BetweenScenesValueSaver.Instance.hasAbilityDash;
            hasAbilityShield = BetweenScenesValueSaver.Instance.hasAbilityShield;
        currentHealth = BetweenScenesValueSaver.Instance.currentHealth;
        maxHealth = BetweenScenesValueSaver.Instance.maxHealth;
        trueHealth = BetweenScenesValueSaver.Instance.trueHealth;
        bombCount = BetweenScenesValueSaver.Instance.bombCount;

        if (BetweenScenesValueSaver.Instance.switchingBetweenLevelScenes && SceneManager.GetActiveScene().buildIndex == 3)
        {
            transform.position = new Vector3(42.5f, 6.5f, 0); //change to pos next to this
        }

    }
    */
}
