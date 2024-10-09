using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    int dashCost;
    int doubleJumpCost;
    int shieldCost; 
    int damageCost; 
    int latteCost; //super latte!
    int bombCost;


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
        DisableButton(button);
    }

    public void BuyDamage(Button button)
    {
        player.GetComponent<PlayerHealth>().MaxHealth -= damageCost;
        UpdatePlayerMaxHealthText();
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
