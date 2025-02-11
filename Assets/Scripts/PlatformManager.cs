using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour

{
    public GameObject platformPrefab;  // R�f�rence au prefab de la plateforme
    public int numberOfPlatforms = 10;  // Nombre de plateformes � g�n�rer
    public float platformSpacing = 15f;  // Espacement entre chaque plateforme
    public GameObject trapPrefab;  // R�f�rence au prefab du pi�ge
    public GameObject movingTrapPrefab;  // R�f�rence au prefab du pi�ge mobile

    void Start()
    {
        GeneratePlatforms();
    }

    void GeneratePlatforms()
    {
        Vector3 spawnPosition = Vector3.zero;

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

            // Ajouter un pi�ge statique
            if (Random.value > 0.5f)
            {
                Vector3 trapPosition = newPlatform.transform.position + new Vector3(0, 1.5f, 0);
                Instantiate(trapPrefab, trapPosition, Quaternion.identity);
            }

            // Ajouter un pi�ge dynamique
            if (Random.value > 0.3f)
            {
                Vector3 movingTrapPosition = newPlatform.transform.position + new Vector3(2, 1.5f, 0);
                GameObject movingTrap = Instantiate(movingTrapPrefab, movingTrapPosition, Quaternion.identity);

                MovingTrap trapScript = movingTrap.GetComponent<MovingTrap>();
                trapScript.pointA = movingTrapPosition;
                trapScript.pointB = movingTrapPosition + new Vector3(0, 0, 5);  // Mouvement sur 5 unit�s en Z
            }

            spawnPosition.z += platformSpacing;
        }
    }
}


