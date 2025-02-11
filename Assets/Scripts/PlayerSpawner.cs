using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Vector3 Position { get; private set; }
    public GameObject PlayerObject { get; private set; }

    public void SpawnPlayer()
    {
        Position = new Vector3(1f, 2f, 1f);
        // Load the prefab from the Resources folder
        GameObject player = Resources.Load<GameObject>("Player"); 

        PlayerObject = GameObject.Instantiate(player, Position, Quaternion.identity);
    }


    private void Update()
    {
        if (PlayerObject == null && Input.GetKeyDown(KeyCode.M))
        {
            Position = new Vector3(1f, 2f, 1f);
            // Load the prefab from the Resources folder
            GameObject player = Resources.Load<GameObject>("Player");

            PlayerObject = GameObject.Instantiate(player, Position, Quaternion.identity);
        }
    }
}
