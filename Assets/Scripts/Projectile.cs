using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    ParticleSystem _particleSystem;
    Rigidbody2D rb;
    //AudioSource audioSource;
    [SerializeField] AudioClip clip;

    static public int damage = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 3f * rb.velocity.normalized.x));
        //Debug.Log(rb.velocity.normalized.x);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //find enemy
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().EnemyTakeDamage(damage);
        }
        AudioSource.PlayClipAtPoint(clip, transform.position);
        DestroyProjectile();
    }

    void DestroyProjectile()
    {
        var rotationAtImpact = this.transform.rotation;
        Instantiate(_particleSystem, transform.position, transform.rotation);// Quaternion.Inverse(rotationAtImpact));
        Destroy(this.gameObject);
    }
}
