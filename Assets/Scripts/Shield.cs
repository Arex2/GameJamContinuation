using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 45f;
    [SerializeField] private Transform player;
    [SerializeField] private float activeTimer = 10f;
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
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
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
