using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    ParticleSystem _particleSystem;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1.5f * rb.velocity.normalized.x));
        Debug.Log(rb.velocity.normalized.x);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //find enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //collision.gameObject.GetComponent<EnemyMovement>.TakeDamage(3);
        }
        DestroyProjectile();
    }

    void DestroyProjectile()
    {
        var rotationAtImpact = this.transform.rotation;
        Instantiate(_particleSystem, transform.position, transform.rotation);// Quaternion.Inverse(rotationAtImpact));
        Destroy(this.gameObject);
    }

    private void ShootProjectile()//(Quaternion angle)
    {
        /*
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 aimPoint = (Vector3)(Input.mousePosition - screenPoint);
        //Vector3 mousPos = aimPoint.Normalize();
        //Instantiate(Resources.Load<TMP_Text>("PopupText"), Input.mousePosition - (offset/2), transform.rotation) as TMP_Text; //Camera.main.ScreenToWorldPoint(Input.mousePosition)
        var mousepos = aimPoint.normalized;
        var lookAngle = Mathf.Atan2(mousepos.y, mousepos.x) * Mathf.Rad2Deg;
        
        GameObject projectile = Instantiate(Resources.Load<GameObject>("Projectile"), transform.position, angle);
        //projectile.GetComponent<SpriteRenderer>().sprite = projectileSprites[Random.Range(0, projectileSprites.Length)];
        projectile.GetComponent<Rigidbody2D>().AddForce(mousepos * 1000);// Vector2.right * 1000);// Vector3.forward * 100);// velocity = Vector2.right * 100;
        Debug.Log(projectile.GetComponent<Rigidbody2D>().totalForce);


        //NEW
        Vector2 startPoint = transform.position;
        float radius = 5f;
        float moveSpeed = 5f;
        float angleStep = 360f /6;
        float angle = 0f;

        for(int i= 0; i < 6 -1; i++)
        {
            float projectileDirXposition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYposition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * moveSpeed;

            var proj = Instantiate(Resources.Load<GameObject>("Projectile"), startPoint, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().gravityScale = 0.3f;
            proj.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);
            angle += angleStep;
        }

                */

    }
}
