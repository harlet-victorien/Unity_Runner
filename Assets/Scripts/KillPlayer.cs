using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Vérifie si l'objet touché est le joueur
        if (collision.collider.CompareTag("Player"))
        {
            // Appelle une fonction pour tuer le joueur
            Kill(collision.collider.gameObject);
        }
    }

    void Kill(GameObject player)
    {
        // Affiche un message ou ajoute ta propre logique de mort
        Debug.Log(player.name + " died !");
        
        // Si tu as un script de gestion de la vie, tu pourrais appeler une fonction ici
        // Exemple : player.GetComponent<PlayerHealth>().Die();

        // Détruit le joueur ou fait apparaître une animation de mort
        Destroy(player);

        // Alternativement, tu pourrais recharger la scène ou montrer un écran "Game Over"
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
