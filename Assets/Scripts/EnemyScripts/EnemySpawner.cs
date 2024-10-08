using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    private void SpawnEnemy()
    {
        Instantiate(enemy, transform.position, enemy.transform.localRotation);
        enemy.GetComponent<EnemyController>().canMove = false;
        enemy.GetComponent<EnemyController>().GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.GetComponent<PlayerMovement>().enemiesDefeated >= 15)
        //{
            //return;
        //}
        //else
        {
            if (other.gameObject.CompareTag("Player"))
            {
                SpawnEnemy();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            enemy.gameObject.GetComponent<EnemyController>().canMove = true;

        }
    }
}
