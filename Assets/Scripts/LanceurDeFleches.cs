using UnityEngine;
using System.Collections;

public class LanceurDeFleches : MonoBehaviour
{
    public GameObject prefabFleche;  // Prefab de la flèche
    public Transform pointDeTir;     // Point de départ de la flèche
    public float vitesseFleche = 20f;   // Vitesse de la flèche
    public KeyCode keyTir = KeyCode.U; // Touche pour activer le tir
    public float intervalleDeTir = 2f;  // Intervalle entre chaque tir
    private float prochainTir;
    private bool piegeActive = false;  // Variable pour savoir si le piège est activé

    void Update()
    {
        // Activer ou désactiver le piège avec la touche définie
        if (Input.GetKeyDown(keyTir))
        {
            piegeActive = !piegeActive;
        }

        // Vérifier si le piège est actif et que le délai pour tirer est écoulé
        if (piegeActive && Time.time >= prochainTir)
        {
            TirerFleche();
            prochainTir = Time.time + intervalleDeTir;
        }
    }

    // Fonction pour tirer la flèche
    void TirerFleche()
    {
        // Instancier la flèche en appliquant une rotation de -90 degrés autour de l'axe Y
        GameObject fleche = Instantiate(prefabFleche, pointDeTir.position, pointDeTir.rotation * Quaternion.Euler(-90, 90, 0));
        
        Rigidbody rb = fleche.GetComponent<Rigidbody>();
        
        // Appliquer une force à la flèche pour la propulser dans la direction du point de tir
        rb.velocity = - pointDeTir.forward * vitesseFleche;

        // Détruire la flèche après 5 secondes pour éviter une accumulation d'objets
        Destroy(fleche, 5f);
    }
}
