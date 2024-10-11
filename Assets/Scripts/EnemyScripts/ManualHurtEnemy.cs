using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualHurtEnemy : EnemyController
{
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;

    void Start()
    {
        sprend = gameObject.GetComponent<SpriteRenderer>();
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default"); // or whatever sprite shader is being used
    }

    public override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);

        if (other.gameObject.CompareTag("Projectile"))
        {
            base.OnCollisionEnter2D(other);
            WhiteSprite();
            Invoke("NormalSprite", 0.2f);
        }
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
