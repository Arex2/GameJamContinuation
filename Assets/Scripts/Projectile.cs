using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    ParticleSystem _particleSystem;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //find enemy
        if(collision.gameObject.CompareTag("Enemy"))
        {
            //collision.gameObject.GetComponent<EnemyMovement>.TakeDamage(1);
        }
        DestroyProjectile();
    }

    void DestroyProjectile()
    {
        Instantiate(_particleSystem, transform.position, gameObject.transform.rotation);
        Destroy(this.gameObject);
    }
}
