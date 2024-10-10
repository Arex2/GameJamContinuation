using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 45f;
    [SerializeField] private Transform player;
    [SerializeField] private float activeTimer = 10f;
    [SerializeField] private ParticleSystem shieldParticles;
    [SerializeField] private TrailRenderer shieldTrail;
    private float activeCountdownTimer;
    public bool playerHasShield = false;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        this.transform.RotateAround(player.position, Vector3.forward, rotationSpeed * Time.deltaTime);

        if (activeCountdownTimer > 0)
        {
            activeCountdownTimer -= Time.deltaTime;
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            shieldParticles.Stop();
            shieldTrail.enabled = false;
            //gameObject.SetActive(false);
        }
        else
        {
            GetComponent<CapsuleCollider2D>().enabled = true;
            GetComponent<SpriteRenderer>().enabled = true;
            shieldParticles.Play();
            shieldTrail.enabled = true;
            //gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            activeCountdownTimer = activeTimer;
        }
    }
}
