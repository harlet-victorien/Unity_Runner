using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float vitesse = 5f;
    public float jumpForce = 5f;
    private Rigidbody rb;
    private Collider col;
    private bool isGrounded;
    private bool jumpPressed;

    // Reference to the child object responsible for rotation
    public Transform rotationCenter;

    // Input values
    private Vector2 movementInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        rb.freezeRotation = true;

        if (rotationCenter == null)
        {
            Debug.LogError("Rotation center child object is not assigned.");
        }
    }

    // Input System callback for movement
    public void OnWASD(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    // Input System callback for jump
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpPressed = true;
        }
    }

    void FixedUpdate()
    {
        // Update isGrounded status
        float raycastDistance = 0.2f;
        Vector3 rayOrigin = new Vector3(transform.position.x, col.bounds.min.y + 0.1f, transform.position.z);
        isGrounded = Physics.Raycast(rayOrigin, Vector3.down, raycastDistance);

        Debug.DrawRay(rayOrigin, Vector3.down * raycastDistance, Color.red);

        // Movement input processing
        Vector3 movement = new Vector3(movementInput.x, 0, movementInput.y).normalized;
        Vector3 desiredVelocity = movement * vitesse;
        desiredVelocity.y = rb.velocity.y; // Preserve vertical velocity

        // Apply velocity to the Rigidbody
        rb.velocity = desiredVelocity;
        // Rotate child object to face velocity direction
        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (horizontalVelocity.magnitude > 0.1f && rotationCenter != null)
        {
            Quaternion targetRotation = Quaternion.LookRotation(horizontalVelocity);
            rotationCenter.rotation = Quaternion.Slerp(rotationCenter.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // Jump
        if (isGrounded && jumpPressed)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Reset jumpPressed
        jumpPressed = false;
        //movementInput = new Vector2 { x = 0, y = 0 };


    }
}
