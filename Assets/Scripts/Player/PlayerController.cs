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
    [SerializeField] private float jumpForce = 800f;
    [SerializeField] private float dashForce = 12000f;
    [SerializeField] private Transform footL, footR;
    [SerializeField] private LayerMask whatIsGround;
    private float horizontalValue;
    private float rayDistance = 0.25f;
    private bool canMove;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector3 spawnPos;

    private Animator anim;

    [SerializeField] private ParticleSystem chargeParticles;
    [SerializeField] private ParticleSystem chargeDoneParticles;
    private bool canShoot, canThrowBomb;
    private float timer, timer2;
    private float cooldown = 0.25f;
    private float chargeTime = 0.75f;
    private int projectileForce = 1000;
    private int bombCount = 0;

    [SerializeField] private ParticleSystem dashParticles;
    [SerializeField] private TrailRenderer dashTrail;
    public bool hasAbilityDoubleJump, hasAbilityDash, hasAbilityShield;
    private bool hasDoubleJumped, hasDashed;

    [SerializeField]
    private TMP_Text bombText;

    [SerializeField]
    public Sprite[] projectileSprites;

    public int BombCount => bombCount;

    // Start is called before the first frame update
    void Start()
    {
        //bombCount = 10;
        //hasAbilityDash = true;
        //hasAbilityDoubleJump = true;
        dashParticles.Stop();
        dashTrail.enabled = false;

        EventController.onDeath += Death;

        canMove = true;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        timer = cooldown;
        timer2 = chargeTime;

        chargeParticles.Stop();
        chargeDoneParticles.Stop();

        lowHealthImage.SetActive(false);

        spawnPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalValue = Input.GetAxis("Horizontal");


        if(Input.GetButton("Fire1") || Input.GetButton("Fire2"))
        {
            //FLIP TO FACE MOUSE
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 aimPoint = (Vector3)(Input.mousePosition - screenPoint);

            if (aimPoint.x > transform.position.x)
            {

                FlipSprite(false);
            }
            else if (aimPoint.x < transform.position.x)
            {
                FlipSprite(true);
            }
        }
        else
        {
            //FLIP TO FACE MOVE DIRECTION
            if (horizontalValue < 0f)
            {
                FlipSprite(true);
            }
            if (horizontalValue > 0f)
            {
                FlipSprite(false);
            }

        }

        




        if (Input.GetButtonDown("Jump") && CheckIfGrounded())
        {
            rb.velocity = Vector2.zero; // so add force isn't additive
            Jump();
        }
        else if(Input.GetButtonDown("Jump") && hasAbilityDoubleJump && !hasDoubleJumped)
        {
            Jump();
            hasDoubleJumped = true;
        }

        if(Input.GetButtonDown("Dash") && hasAbilityDash && !hasDashed)
        {
            Dash();
            hasDashed = true;
        }


        if(Input.GetButton("Fire2") && bombCount > 0)
        {
            //charge up timer for bomb throw
            timer2 -= Time.deltaTime;
            if (timer2 <= 0.0f)
            {
                canThrowBomb = true;
                timer2 = chargeTime;
                chargeParticles.Stop();
                chargeDoneParticles.Play();
            }
            if(!chargeParticles.isPlaying && !chargeDoneParticles.isPlaying)
            {
                chargeParticles.Play();
            }

        }

        if(!IsPointerOverGameObjectClickable())
        {
            if (Input.GetButtonDown("Fire1") && canShoot)
            {
                Debug.Log("Shoot 1");
                ShootProjectile();
                anim.SetTrigger("attack");
                canShoot = false;
            }
            if (Input.GetButtonUp("Fire2"))
            {
                chargeParticles.Stop();
                if (bombCount > 0 && canThrowBomb)
                {
                    Debug.Log("Shoot 2");
                    ShootBomb();
                    anim.SetTrigger("attack");
                    bombCount--;
                    UpdateBombText();
                    canThrowBomb = false;
                    chargeDoneParticles.Stop();

                }
                timer2 = chargeTime;
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

        //cooldown timer for basic attack
        if (canShoot == false)
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                canShoot = true;
                timer = cooldown;
            }
        }



        //ANIMATIONS
        anim.SetFloat("MoveSpeed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("VerticalSpeed", rb.velocity.y);
        anim.SetBool("IsGrounded", CheckIfGrounded());
        //anim.SetBool("IsCrouching", isCrouched);


    }

    private void FixedUpdate()//"g�r p� ett j�mnt intervall 60 g�nger i sekunden"
    {
        if (!canMove)
        {
            return;
        }
        rb.velocity = new Vector2(horizontalValue * moveSpeed * Time.deltaTime, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("BombPickup"))
        {
            AddBomb();
            Destroy(collision.gameObject);
            UpdateBombText();
        }
    }

    private void FlipSprite(bool direction)
    {
        spriteRenderer.flipX = direction;
    }

    private void Jump()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0, jumpForce));
    }

    private void Dash()
    {
        dashTrail.enabled = true;
        dashParticles.Play();
        Invoke("StopParticleSystem", 0.2f);
        rb.AddForce(new Vector2(dashForce * horizontalValue, rb.velocity.y));
        //rb.velocity = new Vector2(dashForce * horizontalValue, rb.velocity.y);
    }
    private void StopParticleSystem()
    {
        dashParticles.Stop();
        dashTrail.enabled = false;
    }

    private bool CheckIfGrounded()
    {
        RaycastHit2D hitL = Physics2D.Raycast(footL.position, Vector2.down, rayDistance, whatIsGround);
        RaycastHit2D hitR = Physics2D.Raycast(footR.position, Vector2.down, rayDistance, whatIsGround);

        if (hitL.collider != null && hitL.collider.CompareTag("Ground") || hitR.collider != null && hitR.collider.CompareTag("Ground"))
        {
            hasDoubleJumped = false;
            hasDashed = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Death()
    {
        anim.SetBool("respawn", false);
        anim.SetTrigger("death");
        Invoke("Respawn", 1f);
    }

    private void Respawn()
    {
        //should wait a second and then
        //do respawn things
        anim.SetBool("respawn", true);
        transform.position = spawnPos;
        EventController.RaiseOnRespawn();
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
        var mousepos = GetMousePosition();
        var lookAngle = Mathf.Atan2(mousepos.y, mousepos.x) * Mathf.Rad2Deg;
        GameObject projectile = Instantiate(Resources.Load<GameObject>("Bomb"), transform.position, Quaternion.Euler(0, 0, lookAngle));
        //projectile.GetComponent<SpriteRenderer>().sprite = projectileSprites[Random.Range(0, projectileSprites.Length)];
        projectile.GetComponent<Rigidbody2D>().AddForce(GetMousePosition() * projectileForce);
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

    private void AddBomb()
    {
        bombCount++;
    }

    private void UpdateBombText()
    {
        bombText.text = bombCount.ToString();
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
