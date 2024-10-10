using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float bounce = 100f;
    [SerializeField] private float knockbackForce = 200f;
    [SerializeField] private float knockbackUpwardForce = 100f;
    [SerializeField] private float canMoveTimer = 0.5f;
    [SerializeField] private int startingHealth = 1;
    private float canMoveCountdownTimer;
    public int currentHealth = 0;
    public int damageGiven = 1;
    public bool canMove = true;
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
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }

    void FixedUpdate()
    {
        if (!canMove)
            return;

        transform.Translate(new Vector2(moveSpeed, 0) * Time.deltaTime);

        if (moveSpeed > 0)
        {
            sprend.flipX = true;
        }
        if (moveSpeed < 0)
        {
            sprend.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyBlock"))
        {
            moveSpeed = -moveSpeed;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            moveSpeed = -moveSpeed;
        }

        if (other.gameObject.CompareTag("Projectile"))
        {
            canMoveCountdownTimer = canMoveTimer;
            currentHealth -= 1;
            other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(other.gameObject.GetComponent<Rigidbody2D>().velocity.x, 0);
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, bounce));
            GetComponent<Animator>().SetTrigger("Hit");
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            canMove = false;

            if (currentHealth <= 0)
            {
                //gameObject.SetActive = false;
            }
        }

        if (other.gameObject.CompareTag("Shield"))
        {
            canMoveCountdownTimer = canMoveTimer;
            currentHealth -= 5;
            other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(other.gameObject.GetComponent<Rigidbody2D>().velocity.x, 0);
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, bounce));
            GetComponent<Animator>().SetTrigger("Hit");
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            canMove = false;

            if (currentHealth <= 0)
            {
                //gameObject.SetActive = false;
            }
        }

        if (other.gameObject.CompareTag("Player"))
        {
            moveSpeed = -moveSpeed;

            if (other.transform.position.x > transform.position.x)
            {
                other.gameObject.GetComponent<PlayerController>().GetKnockedBack(knockbackForce, knockbackUpwardForce);
            }
            else
            {
                other.gameObject.GetComponent<PlayerController>().GetKnockedBack(-knockbackForce, knockbackUpwardForce);
            }
        }
    }
}
