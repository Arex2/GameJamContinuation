using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private GameObject shield;

    int dashCost = 125;
    int doubleJumpCost = 125;
    int shieldCost = 125; 
    int damageCost = 55; 
    int latteCost = 400; //super latte!
    int bombCost = 5;


    private void DisableButton(Button button)
    {
        button.interactable = false;
    }

    public void BuyDash(Button button)
    {
        player.GetComponent<PlayerHealth>().MaxHealth -= dashCost;
        UpdatePlayerMaxHealthText();
        player.GetComponent<PlayerController>().hasAbilityDash = true;
        DisableButton(button);
    }

    public void BuyDoubleJump(Button button)
    {
        player.GetComponent<PlayerHealth>().MaxHealth -= doubleJumpCost;
        UpdatePlayerMaxHealthText();
        player.GetComponent<PlayerController>().hasAbilityDoubleJump = true;
        DisableButton(button);
    }

    public void BuyShield(Button button)
    {
        player.GetComponent<PlayerHealth>().MaxHealth -= shieldCost;
        UpdatePlayerMaxHealthText();
        //player.GetComponent<PlayerController>().hasAbilityShield = true;
        shield.SetActive(true);
        DisableButton(button);
    }

    public void BuyDamage(Button button)
    {
        player.GetComponent<PlayerHealth>().MaxHealth -= damageCost;
        UpdatePlayerMaxHealthText();
        Projectile.damage ++;
        //player.GetComponent<PlayerController>().hasAbilityShield = true;
        //DisableButton(button);
    }

    public void BuyLatte(Button button)
    {
        player.GetComponent<PlayerHealth>().MaxHealth -= latteCost;
        UpdatePlayerMaxHealthText();
        //player.GetComponent<PlayerController>().hasAbilityShield = true;
        DisableButton(button);
    }

    public void BuyBomb(Button button)
    {
        player.GetComponent<PlayerHealth>().MaxHealth -= bombCost;
        UpdatePlayerMaxHealthText();
        //player.GetComponent<PlayerController>().hasAbilityShield = true;
        //DisableButton(button);
    }

    private void UpdatePlayerMaxHealthText()
    {
        player.GetComponent<PlayerHealth>().UpdateMaxHealthText();
    }
}
