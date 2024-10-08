using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportingDoors : MonoBehaviour
{
    [SerializeField]
    private GameObject otherDoor;
    private Collider2D col;
    private bool inRange = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inRange)
        {
            Teleport(col);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            col = collision;
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
    }

    private void Teleport(Collider2D collision)
    {
        collision.transform.position = otherDoor.transform.position;
        collision.GetComponent<Rigidbody2D>().velocity = Vector3.zero; //motverkar jump addforce grejen
    }
}
