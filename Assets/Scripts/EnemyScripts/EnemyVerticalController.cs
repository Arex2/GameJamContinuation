using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVerticalController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float bounce = 100f;
    [SerializeField] public float knockbackForce = 200f;
    [SerializeField] public float knockbackUpwardForce = 100f;
    [SerializeField] public int startingHealth = 1;
    [SerializeField] private float canMoveTimer = 0.5f;
    private float canMoveCountdownTimer;
    public int currentHealth = 0;
    public int damageGiven = 1;
    private bool canMove = true;
    private SpriteRenderer sprend;

    void Start()
    {
        sprend = GetComponent<SpriteRenderer>();
        currentHealth = startingHealth;
    }

    private void Update()
    {
        if (canMoveCountdownTimer > 0)
        {
            canMoveCountdownTimer -= Time.deltaTime;
        }
        else
        {
            canMove = true;
            GetComponent<CapsuleCollider2D>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = true;
            //GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }

    void FixedUpdate()
    {
        if (!canMove)
            return;

        transform.Translate(new Vector2(0, moveSpeed) * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D otherObject)
    {
        if (otherObject.gameObject.CompareTag("EnemyBlock"))
        {
            moveSpeed = -moveSpeed;
        }

        if (otherObject.gameObject.CompareTag("Ground"))
        {
            moveSpeed = -moveSpeed;
        }

        if (otherObject.gameObject.CompareTag("Player"))
        {
            moveSpeed = -moveSpeed;

            if (otherObject.transform.position.x > transform.position.x)
            {
                otherObject.gameObject.GetComponent<PlayerController>().GetKnockedBack(knockbackForce, knockbackUpwardForce);
            }
            else
            {
                otherObject.gameObject.GetComponent<PlayerController>().GetKnockedBack(-knockbackForce, knockbackUpwardForce);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.CompareTag("Player"))
        {
            canMoveCountdownTimer = canMoveTimer;
            currentHealth -= 1;
            otherObject.GetComponent<Rigidbody2D>().velocity = new Vector2(otherObject.GetComponent<Rigidbody2D>().velocity.x, 0);
            otherObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, bounce));
            GetComponent<Animator>().SetTrigger("Hit");
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            //GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            canMove = false;

            if (currentHealth <= 0)
            {
                //Destroy(gameObject, 0.3f);
            }
        }
    }
}
