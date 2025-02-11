using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Modules {
    public Dictionary<int, string[,,]> longs;
    public Dictionary<int, string[,,]> transitions;
    public Modules()
    {

        // Longs
        longs = new Dictionary<int, string[,,]>();
        
        //mod 0
        string[,,] mod = new string[7, 3, 5];
        for (int z = 0; z < 5; z++)
        {
            for (int y = 0; y < 3; y++)
            {
                mod[3, y, z] = "wallInv";
            }
        }
        for (int z = 0; z < 5; z++)
        {
            mod[1, 0, z] = "wall";
            mod[5, 0, z] = "wall";
        }
        longs.Add(0, mod);

        //mod 1
        // start and end
        mod = new string[7, 3, 5];
        mod[0, 0, 0] = "wall";
        mod[6, 0, 0] = "wall";
        mod[0, 0, 4] = "wall";
        mod[6, 0, 4] = "wall";
        // middle block
        mod[2, 1, 1] = "wall";
        mod[3, 1, 1] = "wall";
        mod[4, 1, 1] = "wall";
        longs.Add(1, mod);








        // Transitions
        transitions = new Dictionary<int, string[,,]>();

        //trans 0
        string[,,] trans = new string[7, 3, 3];
        for (int x = 0; x < 7; x++)
        {
            for(int z = 0; z < 3; z++)
            {
                trans[x, 0, z] = "wall";
            }
        }
        transitions.Add(0, trans);

        trans = new string[7, 3, 3];
        for (int x = 0;x < 7; x++)
        {
            trans[x, 0, 0] = "wall";
            trans[x, 0, 2] = "wall";
        }
        trans[0, 0, 1] = "wall";
        trans[6, 0, 1] = "wall";
        transitions.Add(1, trans);
    }
}