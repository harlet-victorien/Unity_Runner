using System;
using UnityEngine;

public class FallingFloorTrap : MonoBehaviour
{
    private Rigidbody rb;
    private bool isTriggered = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // S'assurer que la gravité est désactivée au départ
    }

    void OnTriggerEnter(Collider other)
    {
        // Vérifie si le collider est celui du joueur
        if (other.CompareTag("Player") && !isTriggered)
        {
            rb.AddForce(Vector3.down * 300f, ForceMode.Acceleration);
            isTriggered = true;
            rb.useGravity = true; // Active la gravité pour faire tomber le sol
            //Invoke("DestroyFloor", 10f); // Détruit le sol après 10 secondes
        }
    }

    private void Update()
    {
        // Vérifie si le cube a touché le sol pour le détruire
        if (isTriggered && rb.IsSleeping() == false) // Vérifie si le Rigidbody est en mouvement
        {
            //DestroyFloor();
        }
    }

    private void DestroyFloor()
    {
        Destroy(gameObject); // Détruit le GameObject
    }
}
