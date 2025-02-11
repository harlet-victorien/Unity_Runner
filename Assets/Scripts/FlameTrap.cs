using UnityEngine;

public class FireTrap : MonoBehaviour
{
    public bool isActive;          // The trap is active by default
    public ParticleSystem flames;         // The particle system for the flames
    public BoxCollider fireZoneCollider;  // The collider representing the fire zone
    public KeyCode activationKey = KeyCode.F;

    private void Start()
    {
        flames.Stop();if (!isActive)
        // Ensure the fire zone collider is set as a trigger
        if (fireZoneCollider != null)
        {
            fireZoneCollider.isTrigger = true;
        }
        else
        {
            Debug.LogError("Fire Zone Collider not assigned!");
        }
    }
    private void Update()
    {
        // Activer/Désactiver le piège avec la touche F
        if (Input.GetKeyDown(activationKey))
        {
            isActive = !isActive; // Inverser l'état du piège

            if (isActive)
            {
                flames.Play(); // Activer les flammes (les rendre visibles)

            }
            else
            {
                flames.Stop(); // Désactiver les flammes (les rendre invisibles)
            }

            Debug.Log("Piège activé : " + isActive);
        }
    }
    // Function called when something enters the trap's trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // If the trap is active and the player enters the fire zone
        if (isActive && other.CompareTag("Player"))
        {
            Debug.Log("Player entered the flames!");
            Destroy(other.gameObject); // Instantly kill the player
            Debug.Log("Player died instantly!");
        }
    }

    // Function called when something stays within the trap's trigger collider
    private void OnTriggerStay(Collider other)
    {
        // If the trap is active and the player is within the flames
        if (isActive && other.CompareTag("Player"))
        {
            Debug.Log("Player is within the flames!");
            Destroy(other.gameObject); // Instantly kill the player
            Debug.Log("Player died instantly!");
        }
    }
}
