using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce = 150f;
    private float horizontalValue;
    private bool canMove;
    private Rigidbody2D rb;
    [SerializeField] private Transform footL, footR;
    [SerializeField] private LayerMask whatIsGround;
    private float rayDistance = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalValue = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && CheckIfGrounded())
        {
            rb.velocity = Vector2.zero; // so add force isn't additive
            Jump();
        }
    }

    private void FixedUpdate()//"går på ett jämnt intervall 60 gånger i sekunden"
    {
        if (!canMove)
        {
            return;
        }
        rb.velocity = new Vector2(horizontalValue * moveSpeed * Time.deltaTime, rb.velocity.y);
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce));
    }

    private bool CheckIfGrounded()
    {
        RaycastHit2D hitL = Physics2D.Raycast(footL.position, Vector2.down, rayDistance, whatIsGround);
        RaycastHit2D hitR = Physics2D.Raycast(footR.position, Vector2.down, rayDistance, whatIsGround);

        if (hitL.collider != null && hitL.collider.CompareTag("Ground") || hitR.collider != null && hitR.collider.CompareTag("Ground"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
