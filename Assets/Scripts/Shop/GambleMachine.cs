using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GambleMachine : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    public void BuyRandomColor() //GAMBLING YES
    {
        if (player == null)
            return;
        //player.GetComponent<PlayerMovement>().coinCount -= gambleCost;
        //UpdatePlayerCoinText();
        player.GetComponent<SpriteRenderer>().color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
    }
}
