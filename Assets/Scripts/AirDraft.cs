using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDraft : MonoBehaviour
{
    private Rigidbody2D rb;
    public float force = 50;

    private void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            rb = collision.GetComponent<Rigidbody2D>();
            rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rb.AddForce(transform.up * force);
        }
    }

}
