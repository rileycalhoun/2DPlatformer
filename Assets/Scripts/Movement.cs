using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private float horizontal;
    private bool isFacingRight = true;

    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpingPower = 8f;
    [SerializeField] private float groundCheckRange = 0.1f;

    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        // Check which direction the player should be moving
        horizontal = Input.GetAxisRaw("Horizontal");

        // Check if the 'w' or Space buttons are pressed down and if the user s on the ground
        bool isButtonDown = Input.GetButtonDown("Jump");
        if(isButtonDown && isGrounded())
        {
            // Make the user jump
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpingPower);
        }

        // Check if the 'w' or Space button is not pressed down and if the user is moving upwards
        bool isButtonUp = Input.GetButtonUp("Jump");
        if (isButtonUp && rigidBody.velocity.y > 0f)
        {
            // Make the user slowly lose momentum upwards
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * 0.5f);
        }

        // Flip the character's sprite if they are moving in the opposite direction of which they face
        flip();
    }

    // Fixed update is called at intervals
    void FixedUpdate()
    {
        // Make the user move in the direction they are indicating
        rigidBody.velocity = new Vector2(horizontal * speed, rigidBody.velocity.y);
    }

    // Check if the user is on the ground
    private bool isGrounded()
    {
        // Check if the Ground Check object is in proximity to the ground
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRange, groundLayer);
    }

    private void flip()
    {
        // Check if the user is facing in the same direction that they're moving
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            // Flip the character model if not
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

}
