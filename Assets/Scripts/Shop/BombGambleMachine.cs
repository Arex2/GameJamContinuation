using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BombGambleMachine : MonoBehaviour
{

    [SerializeField]
    private Transform player;
    [SerializeField]
    private GameObject bomb;
    [SerializeField]
    private GameObject nothingDrop;

    private int cost = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(CheckFreeStealState())
            {
                FreeDealMessage();
            }
        }
    }

    public void ButtonClick()
    {
        if (CheckFreeStealState())
        {
            FreeDeal();
        }
        else
        {
            Gamble();
            player.GetComponent<PlayerHealth>().MaxHealth -= cost;
        }
    }

    private bool CheckFreeStealState()
    {
        if (player.GetComponent<PlayerHealth>().MaxHealth < 100 && player.GetComponent<PlayerController>().BombCount == 0)
            return true;
        return false;    
    }

    private void Gamble()
    {
        int randomInt = Random.Range(1, 101);
        if(randomInt == 100) //1% chance
        {
            //drop 10 maxHealth?
        }
        else if (randomInt > 0 && randomInt <= 5) //5% chance
        {
            DropBomb(3);
        }
        else if(randomInt > 5 && randomInt <= 20)//15% chance
        {
            DropBomb(1);
        }
        else //drop nothing
        {
            //instantiate sad particle system
            DropNothing();
        }
    }

    private void DropBomb(int count)
    {
        for (int i = 0; i < count; i++)
        {
            //Instantiate(bomb);
            var b = Instantiate(bomb, new Vector3(transform.position.x, transform.position.y + 3, 0), Quaternion.Euler(0, 0, Random.Range(0, 360))); // Random.rotation);
            b.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(Vector3.up * 4);
        }
    }

    private void FreeDeal()
    {
        DropBomb(1);
    }

    private void DropNothing()
    {
        nothingDrop.GetComponent<Animator>().SetTrigger("PlayAnimation");
    }

    private void FreeDealMessage()
    {
        //-0ho for 1 bomb!
    }
}
