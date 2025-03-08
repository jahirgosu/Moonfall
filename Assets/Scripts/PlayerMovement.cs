using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public float jumpHeight = 5f;
    public bool isGround = true;
    public Animator animator;

    private float movement;
    private bool isFacingRight = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        movement = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            Jump();
            isGround = false;
        }

    }

    private void FixedUpdate()
    {
        //transform.position += new Vector3(movement, 0f, 0f) * Time.fixedDeltaTime * moveSpeed;
        rb.velocity = new Vector2(movement * moveSpeed, rb.velocity.y);
        animator.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
    }

    private void FlipSprite()
    {
        if(isFacingRight && movement <0f || !isFacingRight && movement > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }

}
