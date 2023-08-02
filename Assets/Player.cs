using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
    private bool isFrozen = true;
    [SerializeField] private float jumpGravityScale = 1.5f;
    [SerializeField] private float upwardRotation = 20f;
    [SerializeField] private float downwardRotation = -20f;
    [SerializeField] private float rotationSpeed = 3f; // Adjust this value to control the rotation speed

    private Vector3 startPosition; // Store the initial position of the player
    private Rigidbody2D rb;
    private float originalGravityScale;

    // Event to notify when the player passes by the right side of a pipe
    public event Action OnPassRightSidePipe;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravityScale = rb.gravityScale;

        startPosition = transform.position;
    }

    void Update()
    {
        // Check for jump input (e.g., spacebar or tap)
        if (Input.GetKeyDown(KeyCode.Space) && !isFrozen)
        {
            Jump();
        }

        // Apply smooth rotation based on Y velocity
        RotateBasedOnVelocity();
    }

    void Jump()
    {
        // Normalize the velocity to prevent force from accumulating
        rb.velocity = new Vector2(rb.velocity.x, 0f);

        // Apply an upward force to the Rigidbody2D to make the GameObject jump
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        // Increase gravity scale temporarily for a snappier fall after the jump
        rb.gravityScale = jumpGravityScale;
        StartCoroutine(ResetGravityScale());
    }

    IEnumerator ResetGravityScale()
    {
        // Wait for a short duration before resetting gravity scale
        yield return new WaitForSeconds(0.2f);

        // Reset gravity scale back to its original value
        rb.gravityScale = originalGravityScale;
    }

    void RotateBasedOnVelocity()
    {
        // Check if the bird is going up or down in the Y direction
        float yVelocity = rb.velocity.y;

        // Calculate the target rotation based on the bird's Y velocity
        float targetRotation = yVelocity > 0 ? upwardRotation : downwardRotation;

        // Smoothly rotate to the target rotation using Lerp
        float newZRotation = Mathf.LerpAngle(transform.rotation.eulerAngles.z, targetRotation,
            Time.deltaTime * rotationSpeed);

        // Apply the new rotation to the bird
        transform.rotation = Quaternion.Euler(0f, 0f, newZRotation);
    }

    void FixedUpdate()
    {
        // Clamp the Y position to a maximum value
        Vector3 clampedPosition = transform.position;
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -4.5f, 4.5f);
        transform.position = clampedPosition;
    }


    public bool IsCollidingWithWall()
    {
        // Get the bounds of the player's collider
        Bounds playerBounds = GetComponent<Collider2D>().bounds;

        // Define the layer mask for wall objects
        int wallMask = LayerMask.GetMask("Wall");

        // Get all colliders in the scene that belong to the wall layer
        Collider2D[] wallColliders = Physics2D.OverlapBoxAll(playerBounds.center, playerBounds.size, 0f, wallMask);

        // Check if the player has intersected with any wall objects
        return wallColliders.Length > 0;
    }


    public void ResetPlayer()
    {
        // Reset the player's position to the start position
        transform.position = startPosition;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.gravityScale = originalGravityScale;
        transform.rotation = Quaternion.identity;
    }

    public void MakeControllable()
    {
        GetComponent<Rigidbody2D>().gravityScale = 1f;
        isFrozen = false;
    }

    // Function to freeze the player in place without gravity
    public void Freeze()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        isFrozen = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Score"))
        {
            OnPassRightSidePipe?.Invoke();
        }
    }
}