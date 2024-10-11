using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 1f;
    [SerializeField] private float bounce = 100f;
    [SerializeField] private float knockbackForce = 200f;
    [SerializeField] private float knockbackUpwardForce = 100f;
    [SerializeField] private float canMoveTimer = 0.5f;
    [SerializeField] private int startingHealth = 10;
    private float canMoveCountdownTimer;
    public int currentHealth = 10;
    public int damageGiven = 1;
    public bool canMove = true;
    public SpriteRenderer sprend;

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

    public virtual void FixedUpdate()
    {
        if (!canMove)
            return;

        transform.Translate(new Vector2(moveSpeed, 0) * Time.deltaTime);

        if (moveSpeed < 0)
        {
            sprend.flipX = true;
        }
        if (moveSpeed > 0)
        {
            sprend.flipX = false;
        }
    }

    public virtual void OnCollisionEnter2D(Collision2D other)
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
            EnemyHurt();
            other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(other.gameObject.GetComponent<Rigidbody2D>().velocity.x, 0);
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, bounce));

            if (currentHealth <= 0)
            {
                Invoke("EnemyDeactivate", 0.2f);
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
                Invoke("EnemyDeactivate", 0.2f);
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

    private void EnemyHurt()
    {
        canMoveCountdownTimer = canMoveTimer;
        currentHealth -= 1;
        GetComponent<Animator>().SetTrigger("Hit");
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        canMove = false;
    }

    private void EnemyDeactivate()
    {
        gameObject.SetActive(false);
    }
}
