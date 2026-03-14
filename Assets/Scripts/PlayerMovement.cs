using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Transform orientation;
    public float moveSpeed;

    public IngredientScript ingredientScript;

    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    
    Rigidbody rb;
    public float groundDrag;
    public float bounceStrength;
    public float bounceCooldown;
    public float CanMoveCooldown;

    private bool canBounce = true;
    private bool canMove = true;

    public float dashForce = 25f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 1.5f;
    private bool isDashing = false;
    private bool canDash = true;
    public Slider dashSlider;
    private float currentDashTimer;

    public Camera playerCamera;


    public float playerHeight;
    public LayerMask Ground;
    bool grounded;

    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);
        MyInput();
        if (isDashing)
        {
            rb.linearDamping = 0.5f;
        }
        else
        {
            rb.linearDamping = grounded ? groundDrag : 0;
        }

        if (isDashing == true)
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, 65f, Time.deltaTime * 10f);
        }
        else
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, 60f, Time.deltaTime * 10f);
        }

        SpeedControl();
        UpateDashSlider();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            Dash();
        }
    }

    private void Dash()
    {
        canDash = false;
        isDashing = true;
        currentDashTimer = 0f;

        Vector3 dashDirection = orientation.forward;
        rb.AddForce(dashDirection * dashForce, ForceMode.VelocityChange);
        ingredientScript.UpdatePositionSpeed(100f);

        StartCoroutine(StopDash());
        StartCoroutine(DashCooldown());
    }

    private void UpateDashSlider()
    {
        if (canDash)
        {
            dashSlider.value = 1f;
        }
        else
        {
            currentDashTimer += Time.deltaTime;
            dashSlider.value = currentDashTimer / dashCooldown;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void MovePlayer()
    {
        if (!canMove) return;
        if (isDashing) return;
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (moveDirection.magnitude > 0)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        if (isDashing) return;
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!canBounce) return;
        if (((1 << collision.gameObject.layer) & Ground) == 0)
        {
            StartCoroutine(BounceCooldown());
            StartCoroutine(CanMove());
            Vector3 bounceDirection = collision.contacts[0].normal;
            
            bounceDirection.y = 0;
            rb.AddForce(bounceDirection.normalized * bounceStrength, ForceMode.Impulse);
        }
    }
    private IEnumerator BounceCooldown()
    {
        canBounce = false;
        yield return new WaitForSeconds(bounceCooldown);
        canBounce = true;
    }

    private IEnumerator CanMove()
    {
        canMove = false;
        yield return new WaitForSeconds(CanMoveCooldown);
        canMove = true;
    }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private IEnumerator StopDash()
    {
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        ingredientScript.UpdatePositionSpeed(0);
    }
}
