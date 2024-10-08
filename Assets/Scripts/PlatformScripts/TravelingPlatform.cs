using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelingPlatform : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float moveSpeed = 2.0f;
    private Animator animat;

    private void Start()
    {
        animat = GetComponent<Animator>();
        animat.enabled = false;
    }

    private void Update()
    {
        if (transform.position == target.transform.position)
        {
            animat.enabled = true;
            Destroy(gameObject, 3f);
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.transform.position.y > transform.position.y)
        {
            other.transform.SetParent(transform);
        }

        if (other.gameObject.CompareTag("Enemy") && other.transform.position.y > transform.position.y)
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.transform.SetParent(null);
        }
    }
}
