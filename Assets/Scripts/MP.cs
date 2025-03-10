using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 3.0f;
    private Rigidbody2D rigby;
    private Vector2 moveDirect;
    private Animator animator;

    void Start()
    {
        rigby = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

   
    void Update()
    {
        moveDirect = new Vector2(Input.GetAxis("Horizontal"), 0).normalized;
        animator.SetFloat("Speed", Mathf.Abs(moveDirect.magnitude * movementSpeed));

        bool flipped = moveDirect.x < 0;
        this.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f ));
    }

    private void FixedUpdate()
    {
        rigby.velocity = moveDirect * movementSpeed;

        if(moveDirect != Vector2.zero)
        {
            var xMoveD = moveDirect.x * movementSpeed * Time.deltaTime;
            this.transform.Translate(new Vector3(xMoveD, 0), Space.World);
        }
    }
}
