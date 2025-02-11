using UnityEngine;

public class RollingBoulder : MonoBehaviour
{
    public float pushForce = 500f;           // The force applied to the boulder when pushing
    public float killVelocityThreshold = 3f; // The minimum velocity required to kill the player

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Get the Rigidbody component attached to the boulder
    }

    void Update()
    {
        // If the "C" key is pressed
        if (Input.GetKeyDown(KeyCode.O))
        {
            PushBoulder();  // Apply the force to the boulder
        }
    }

    void PushBoulder()
    {
        // Apply a force to the boulder in the backward direction
        rb.AddForce(Vector3.back * pushForce);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is the player
        if (collision.collider.CompareTag("Player"))
        {
            // Calculate the impact velocity between the boulder and the player
            float impactVelocity = collision.relativeVelocity.magnitude;

            // Debugging: Print the impact velocity
            Debug.Log("Impact Velocity: " + impactVelocity);

            // If the impact velocity is greater than or equal to the threshold, kill the player
            if (impactVelocity >= killVelocityThreshold)
            {
                KillPlayer(collision.collider.gameObject);
            }
            else
            {
                // The boulder is moving too slowly to kill the player
                Debug.Log("Boulder moving too slowly to kill the player.");
            }
        }
    }

    void KillPlayer(GameObject player)
    {
        // Display a death message or add your own death logic here
        Debug.Log(player.name + " died!");

        // If you have a health management script, you could call a function like this:
        // player.GetComponent<PlayerHealth>().Die();

        // Destroy the player GameObject or trigger a death animation
        Destroy(player);

        // Alternatively, you could reload the scene or show a "Game Over" screen:
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
