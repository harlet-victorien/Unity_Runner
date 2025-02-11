using UnityEngine;

public class VoidKill : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        
        // Appelle une fonction pour tuer le joueur
        Kill(collision.collider.gameObject);
        
    }

    void Kill(GameObject player)
    {
        // Affiche un message ou ajoute ta propre logique de mort
        Debug.Log(player.name + " died !");

        // Si tu as un script de gestion de la vie, tu pourrais appeler une fonction ici
        // Exemple : player.GetComponent<PlayerHealth>().Die();

        // D�truit le joueur ou fait appara�tre une animation de mort
        Destroy(player);

        // Alternativement, tu pourrais recharger la sc�ne ou montrer un �cran "Game Over"
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
