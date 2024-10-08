using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private GameObject startPosition;
    public int damageDealt = 1;
    private Rigidbody2D rigbod;

    private void Start()
    {
        rigbod = GetComponent<Rigidbody2D>();
    }

    private void SpikesReturn()
    {
        transform.position = startPosition.transform.position;
        rigbod.gravityScale = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rigbod.gravityScale = 7;
            Invoke("SpikesReturn", 4f);
        }
    }
}
