using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandboxHealthMachine : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    int healthIncrease = 100;
    public void BuyRandomColor() //GAMBLING YES
    {
        if (player == null)
            return;
        player.GetComponent<PlayerHealth>().MaxHealth += healthIncrease;
    }
}
