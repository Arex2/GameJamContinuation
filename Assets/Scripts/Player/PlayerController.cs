using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static Unity.VisualScripting.Member;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject lowHealthImage;
    [SerializeField] private float moveSpeed = 800f;
    [SerializeField] private float jumpForce = 150f;
    [SerializeField] private Transform footL, footR;
    [SerializeField] private LayerMask whatIsGround;
    private float horizontalValue;
    private float rayDistance = 0.25f;
    private bool canMove;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private bool canShoot;
    private float timer;
    private float cooldown1 = 0.25f;
    private int projectileForce = 1000;

    [SerializeField]
    public Sprite[] projectileSprites;

    // Start is called before the first frame update
    void Start()
    {
        EventController.onDeath += Respawn;

        canMove = true;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        timer = cooldown1;
        lowHealthImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalValue = Input.GetAxis("Horizontal");

        if (horizontalValue < 0f)
        {
            FlipSprite(true);
        }
        if (horizontalValue > 0f)
        {
            FlipSprite(false);
        }

        if (Input.GetButtonDown("Jump") && CheckIfGrounded())
        {
            rb.velocity = Vector2.zero; // so add force isn't additive
            Jump();
        }

        if(!IsPointerOverGameObjectClickable())
        {
            if (Input.GetButtonDown("Fire1") && canShoot)
            {
                Debug.Log("Shoot 1");
                ShootProjectile();
                canShoot = false;
            }
            if (Input.GetButtonDown("Fire2"))
            {
                Debug.Log("Shoot 2");
                ShootBomb();
            }
        }


        if(GetComponent<PlayerHealth>().CurrentHealth > 800)
        {
            moveSpeed = 350f;
            lowHealthImage.SetActive(false);
        }

        if (GetComponent<PlayerHealth>().CurrentHealth >= 200 && GetComponent<PlayerHealth>().CurrentHealth <= 799)
        {
            moveSpeed = 500f;
            lowHealthImage.SetActive(false);
        }

        if (GetComponent<PlayerHealth>().CurrentHealth < 200)
        {
            moveSpeed = 700f;
            lowHealthImage.SetActive(true);
        }

        //cooldown timer
        if (canShoot == false)
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                canShoot = true;
                timer = cooldown1;
            }
        }
    }

    private void FixedUpdate()//"g�r p� ett j�mnt intervall 60 g�nger i sekunden"
    {
        if (!canMove)
        {
            return;
        }
        rb.velocity = new Vector2(horizontalValue * moveSpeed * Time.deltaTime, rb.velocity.y);
    }

    private void FlipSprite(bool direction)
    {
        spriteRenderer.flipX = direction;
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

    private void Respawn()
    {
        //do respawn things
    }

    private void ShootProjectile()
    {
        //Instantiate(Resources.Load<TMP_Text>("PopupText"), Input.mousePosition - (offset/2), transform.rotation) as TMP_Text; //Camera.main.ScreenToWorldPoint(Input.mousePosition)
        var mousepos = GetMousePosition();
        var lookAngle = Mathf.Atan2(mousepos.y, mousepos.x) * Mathf.Rad2Deg;
        GameObject projectile = Instantiate(Resources.Load<GameObject>("Projectile"), transform.position, Quaternion.Euler(0, 0, lookAngle));
        projectile.GetComponent<SpriteRenderer>().sprite = projectileSprites[Random.Range(0, projectileSprites.Length)];
        projectile.GetComponent<Rigidbody2D>().AddForce(GetMousePosition() * projectileForce);
    }

    private void ShootBomb()
    {

    }

    private Vector3 GetMousePosition()
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 aimPoint = (Vector3)(Input.mousePosition - screenPoint);
        aimPoint.Normalize();

        return aimPoint;
    }

    public void GetKnockedBack(float knockbackForce, float knockbackUpwardForce)
    {
        canMove = false;
        rb.AddForce(new Vector2(knockbackForce, knockbackUpwardForce));
        Invoke("CanMoveAgain", 0.25f);
    }

    private void CanMoveAgain()
    {
        canMove = true;
    }

    /*
    public bool RaycastAll(PointerEventData eventData, List<RaycastResult> raycastResults)
    {
        foreach (RaycastResult result in raycastResults)
        {
            if(result.ToString() == "Button")
            {
                return true;
            }
        }
        return false;
    }

    private bool IsMouseOverUI()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        return false;
    }
        */

    //FROM : https://stackoverflow.com/a/76350447
    private bool IsPointerOverGameObjectClickable()
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        if (raycastResults.Count > 0)
        {
            foreach (var go in raycastResults)
            {
                if (go.gameObject.CompareTag("Clickable"))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
