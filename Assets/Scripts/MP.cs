using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP : MonoBehaviour
{
    [Header("Movement Settings")]
    [Range(0f, 50f)]
    public float runSpeed = 2f;
    public float movementSmoothing = 0.05f;
    public float Vis_xVel;
    public float Vis_yVel;

    [Header("Jump Settings")]
    public float jumpForce = 400f;
    public float fallMult;
    public float jumpMult;
    private Vector2 vecGravity;
    public float jumpTime;

    // Ground settings
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool wasGroundedLastFrame = false;

    private Coroutine stepCoroutine;
    private bool isWalking = false;


    [Header("Animation Settings")]
    public Animator animator;

    private Rigidbody2D rigidbody2D;
    private float horizontalMove = 0f;
    private Vector3 currentVelocity;
    private float compensationSpeed = 10f;
    private bool facingRight = true;
    private bool jumpPressed = false;
    private bool isGrounded = false;
    private bool isJumping;
    float jumpCounter;

    AudioManager audioManager;

    [Header("Attack Settings")]
    public Transform Aim;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        vecGravity = new Vector2(0,Physics2D.gravity.y);
    }

    private void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal") * runSpeed;

        // Detectar si camina en el suelo
        bool currentlyWalking = Mathf.Abs(horizontalMove) > 0.1f && isGrounded;

        if (currentlyWalking && !isWalking)
        {
            isWalking = true;
            stepCoroutine = StartCoroutine(PlayFootsteps());
        }
        else if (!currentlyWalking && isWalking)
        {
            isWalking = false;
            if (stepCoroutine != null)
            {
                StopCoroutine(stepCoroutine);
            }
        }


        animator.SetFloat("xVelocity", Mathf.Abs(rigidbody2D.velocity.x));
        animator.SetFloat("yVelocity", rigidbody2D.velocity.y);


        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("MovementBool", true);
        }
        else
        {
            animator.SetBool("MovementBool", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = true;
        }
    }

    private void FixedUpdate()
    {
        // Crea un collider para checar si está pisando el GROUND LAYER
        isGrounded = false;
        animator.SetBool("isGrounded", false);

        Collider2D[] groundColliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundLayer);
        for (int i = 0; i < groundColliders.Length; i++)
        {
            if (groundColliders[i].gameObject != this.gameObject)
            {
                isGrounded = true;
                animator.SetBool("isGrounded", true);
                animator.SetBool("isJumping", false);

                // DETECTA ATERIZAJE
                if (!wasGroundedLastFrame)
                {
                    // Reproducir sonido de caída
                    audioManager.PlaySFX(audioManager.Fall);
                }

            }
        }

        wasGroundedLastFrame = isGrounded;

        // Mueve dereha o izquierda
        Move(horizontalMove * Time.fixedDeltaTime);

        //Flipea los sprites
        if (horizontalMove > 0f && !facingRight)
        {

            Flip();

        }
        else if (horizontalMove < 0f && facingRight)
        {
            Flip();
        }
    }

    private void Move(float _move)
    {
        Vector3 targetVelocity = new Vector2(_move * compensationSpeed, rigidbody2D.velocity.y);
        rigidbody2D.velocity = Vector3.SmoothDamp(rigidbody2D.velocity, targetVelocity, ref currentVelocity, movementSmoothing);

        if( isGrounded )
        {
            isGrounded = true;
            animator.SetBool("isGrounded", true);
            animator.SetBool("isJumping", false);
        }

        if (jumpPressed && isGrounded)
        {
            audioManager.PlaySFX(audioManager.Jump);
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            //rigidbody2D.velocity = new Vector2(0f, jumpForce);
            jumpPressed = false;
            isJumping = true;
            isGrounded = false;
            jumpCounter = 0;
            animator.SetBool("isGrounded", false);
            animator.SetBool("isJumping", true);
        }

        if (rigidbody2D.velocity.y > 0 && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime)
            {
                isJumping = false;
                animator.SetBool("isJumping", false);
            }
            rigidbody2D.velocity += vecGravity * jumpMult * Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            animator.SetBool("isJumping", false);
        }

        if(rigidbody2D.velocity.y < 0)
        {
            rigidbody2D.velocity -= vecGravity * fallMult * Time.deltaTime;
        }

    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 targetScale = transform.localScale;
        targetScale.x *= -1;
        transform.localScale = targetScale;
    }

    private IEnumerator PlayFootsteps()
    {
        while (true)
        {
            audioManager.PlaySFX(audioManager.footsteps, 0.9f, 1.1f); // Pitch aleatorio entre 0.9 y 1.1
            yield return new WaitForSeconds(0.4f); // Tiempo entre pasos
        }
    }


}
