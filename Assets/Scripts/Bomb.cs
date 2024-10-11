using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    ParticleSystem _particleSystem;
    [SerializeField] AudioClip clip;
    Rigidbody2D rb;
    int directHitDmg = 20;
    int explosionDmg = 15;
    float explosionRadius = 2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1.5f * rb.velocity.normalized.x));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //find enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().EnemyTakeDamage(directHitDmg);
            //DO even more damage on direct hit
        }
        AudioSource.PlayClipAtPoint(clip, transform.position);
        Explode(transform.position, explosionRadius);
        DestroyProjectile();
    }

    void DestroyProjectile()
    {
        var rotationAtImpact = this.transform.rotation;
        Instantiate(_particleSystem, transform.position, transform.rotation);// Quaternion.Inverse(rotationAtImpact));
        Destroy(this.gameObject);
    }

    void Explode(Vector3 explosionPoint, float explosionRadius)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(explosionPoint, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                //Do damage
                //hitCollider.GetComponent<EnemyController>().doDamage(10);
                hitCollider.gameObject.GetComponent<EnemyController>().EnemyTakeDamage(explosionDmg);
            }
        }
    }
}
