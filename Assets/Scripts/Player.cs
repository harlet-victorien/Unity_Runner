using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public float vitesse = 5f;
    public float jumpForce = 5f;
    private Rigidbody rb;
    private Collider col;
    private bool isGrounded;
    private bool jumpPressed;

    // Reference to the child object responsible for rotation
    public Transform rotationCenter;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        // Freeze rotation to prevent the player from tipping over
        rb.freezeRotation = true;

        // Ensure the rotation center is assigned
        if (rotationCenter == null)
        {
            Debug.LogError("Rotation center child object is not assigned.");
        }
    }

    void Update()
    {
        // Capture jump input in Update()
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = true;
        }
    }

    void FixedUpdate()
    {
        // Update isGrounded status at the beginning
        float raycastDistance = 0.2f;
        Vector3 rayOrigin = new Vector3(transform.position.x, col.bounds.min.y + 0.1f, transform.position.z);
        isGrounded = Physics.Raycast(rayOrigin, Vector3.down, raycastDistance);

        // For debugging: Visualize the raycast
        Debug.DrawRay(rayOrigin, Vector3.down * raycastDistance, Color.red);

        // Movement input
        float mouvementHorizontal = 0f;
        float mouvementVertical = 0f;

        if (Input.GetKey(KeyCode.Z))
            mouvementVertical += 1;

        if (Input.GetKey(KeyCode.S))
            mouvementVertical -= 1;

        if (Input.GetKey(KeyCode.Q))
            mouvementHorizontal -= 1;

        if (Input.GetKey(KeyCode.D))
            mouvementHorizontal += 1;

        Vector3 movementInput = new Vector3(mouvementHorizontal, 0, mouvementVertical).normalized;
        Debug.Log(movementInput);
        // Calculate the desired velocity
        Vector3 desiredVelocity = movementInput * vitesse;
        desiredVelocity.y = rb.velocity.y; // Preserve the current vertical velocity

        // Apply the velocity to the Rigidbody
        rb.velocity = desiredVelocity;

        // Rotate the child object to face the velocity direction if it's above a small threshold
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
    }
}
