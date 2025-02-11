using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int nbPlayers;
    private GameObject[] players;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Update the player references each frame
        players = GameObject.FindGameObjectsWithTag("Player");
        if (nbPlayers < players.Length)
        {
            nbPlayers = players.Length;
            Transform child = players[players.Length - 1].transform.Find("body");
            if (nbPlayers == 1)
            {
                Debug.Log("oui 1");
                GameObject.Destroy(child.Find("cow").gameObject);
                GameObject.Destroy(child.Find("llama").gameObject);
            }
            else if (nbPlayers == 2)
            {
                GameObject.Destroy(child.Find("pig").gameObject);
                GameObject.Destroy(child.Find("llama").gameObject);
            }
            else if (nbPlayers == 3)
            {
                GameObject.Destroy(child.Find("cow").gameObject);
                GameObject.Destroy(child.Find("pig").gameObject);
            }

        }
        else if (nbPlayers != players.Length)
        {
            nbPlayers = players.Length;
        }
    }
}
