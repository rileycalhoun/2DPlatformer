using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private float horizontal;
    public float speed = 4f;
    public float jumpingPower = 8f;
    public float groundCheckRange = 0.1f;

    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        bool isButtonDown = Input.GetButtonDown("Jump");
        if(isButtonDown && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        bool isButtonUp = Input.GetButtonUp("Jump");
        if (isButtonUp && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        flip();
    }

    // Fixed update is called at intervals
    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRange, groundLayer);
    }

    private void flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

}
