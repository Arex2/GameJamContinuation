using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaterEnemyTargetSeeking : EnemyController
{
    [SerializeField] private GameObject player;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;
    private bool followingPlayer;
    private float distance;

    private void Start()
    {
        followingPlayer = false;
        sprend = gameObject.GetComponent<SpriteRenderer>();
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default");
    }

    public override void Update()
    {
        if (canMoveCountdownTimer > 0)
        {
            canMoveCountdownTimer -= Time.deltaTime;
        }
        else
        {
            canMove = true;
            GetComponent<CapsuleCollider2D>().enabled = true;
        }

        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < 7)
        {
            followingPlayer = true;
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            GetComponent<Animator>().SetTrigger("Vicinity");

            if (transform.position.x > player.transform.position.x)
            {
                sprend.flipX = true;
            }
            if (transform.position.x < player.transform.position.x)
            {
                sprend.flipX = false;
            }
        }
        else
        {
            followingPlayer = false;
            GetComponent<Animator>().SetTrigger("NoVicinity");
        }
    }

    public override void FixedUpdate()
    {
        if (moveSpeed < 0)
        {
            sprend.flipX = true;
        }
        if (moveSpeed > 0)
        {
            sprend.flipX = false;
        }

        if (!canMove || followingPlayer)
            return;

        transform.Translate(new Vector2(moveSpeed, 0) * Time.deltaTime);
    }

    public override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
        /*
        if (other.gameObject.CompareTag("Projectile"))
        {
            base.OnCollisionEnter2D(other);
            WhiteSprite();
            Invoke("NormalSprite", 0.2f);
        }
        */
        if (other.gameObject.CompareTag("Player"))
        {
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

    public override void EnemyTakeDamage(int damage)
    {
        base.EnemyTakeDamage(damage);
        WhiteSprite();
        Invoke("NormalSprite", 0.2f);

    }

    void WhiteSprite()
    {
        sprend.material.shader = shaderGUItext;
        sprend.color = Color.white;
    }

    void NormalSprite()
    {
        sprend.material.shader = shaderSpritesDefault;
        sprend.color = Color.white;
    }
}
