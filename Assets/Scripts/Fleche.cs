using UnityEngine;

public class Fleche : MonoBehaviour
{
    // Cette méthode est appelée lorsque la flèche entre en collision avec un autre objet
    void OnCollisionEnter(Collision collision)
    {
        // Vérifie si l'objet touché a le tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Appelle une méthode pour tuer le joueur
            TuerJoueur(collision.gameObject);
        }
    }

    // Méthode pour gérer la logique de mort du joueur
    void TuerJoueur(GameObject joueur)
    {
        // Logique de mort du joueur
        Debug.Log("Le joueur est mort !");
        
        // Exemple : désactiver le joueur au lieu de le détruire
        // joueur.SetActive(false);

        // Si tu souhaites le détruire
        Destroy(joueur);  // Supprime le joueur pour simuler sa mort (remplace par ta propre logique)

        // Optionnel : détruire la flèche après avoir touché le joueur
        Destroy(gameObject);
    }
}
