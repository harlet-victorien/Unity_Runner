// Map.cs
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public string[,,] mapData { get; private set; }

    private readonly int width;
    private readonly int height;
    private readonly int depth;

    private readonly int numberModules;

    public GameObject wallPrefab;

    public Map()
    {
        this.width = 9;
        this.height = 4;

        this.numberModules = 3;

        this.depth = 6 + 5 * numberModules + 3 * (numberModules - 1);

        // Initialize mapData with default values (0: no cube)
        mapData = new string[width, height, depth];
    }

    public void InitializeMapData(bool blocked)
    {
        // Clear mapData
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                for (int z = 0; z < depth; z++)
                    mapData[x, y, z] = null;

        // Create a flat platform
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                mapData[x, 0, z] = "wall"; 
            }
        }

        // Walls
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                mapData[x, y, 0] = "wallInv";
                mapData[x, y, depth - 1] = "wallInv";
            }
        }
        for (int z = 0; z < depth; z++)
        {
            for (int y = 0; y < height; y++)
            {
                mapData[0, y, z] = "wallInv";
                mapData[width - 1, y, z] = "wallInv";
            }
        }


        // Starting and Ending Zones
        int StartingZoneEnd = 3;
        int EndZoneStart = depth - 3;

        for (int x = 1; x < width - 1; x++)
        {
            for (int z = StartingZoneEnd; z < EndZoneStart; z++)
            {
                mapData[x, 0, z] = null;
            }
        }
        for (int x = 1; x < width - 1; x++)
        {
            for (int y = 1; y < height; y++)
            {
                for (int z = EndZoneStart; z < depth - 1; z++)
                {
                    mapData[x, y, z] = "EndZone";
                }
            }
        }
        if (blocked)
        {
            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height; y++)
                {
                    mapData[x, y, StartingZoneEnd - 1] = "wallInv";
                }
                mapData[x, height - 1, StartingZoneEnd - 2] = "wallInv";
            }
        }

        // Modules
        Modules modules = new Modules();


        for (int i = 0; i < numberModules; i++)
        {
            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    for (int z = 0; z < 5; z++)
                    {
                        mapData[x + 1, y, z + 3 + 8 * i] = modules.longs[i % 2][x, y, z];
                    }
                }
            }
            if (i < numberModules - 1)
            {
                for (int x = 0; x < 7; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        for (int z = 0; z < 3; z++)
                        {
                            mapData[x + 1, y, z + 8 + 8 * i] = modules.transitions[i % 2][x, y, z];
                        }
                    }
                }
            }
        }
    }


    public void GenerateMap(Transform parent)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    string id = mapData[x, y, z];
                    if (id != null) // If id is not 0, create a cube
                    {
                        Vector3 position = new Vector3(x, y, z);
                        Cube cube = new Cube(position, parent, id);

                    }
                }
            }
        }
    }

    public void ModifyBlockAt(Vector3 position, string newBlockId, Transform parent)
    {
        int x = Mathf.FloorToInt(position.x);
        int y = Mathf.FloorToInt(position.y);
        int z = Mathf.FloorToInt(position.z);

        // Check if the coordinates are within bounds.
        if (x >= 0 && x < width && y >= 0 && y < height && z >= 0 && z < depth)
        {
            // Update mapData
            mapData[x, y, z] = newBlockId;

            // Find the block at the specified position in the scene.
            foreach (Transform child in parent)
            {
                if (child.position == new Vector3(x, y, z))
                {

                    // If newBlockId is null, destroy the block (delete it from the scene)
                    if (newBlockId != null)
                    {
                        GameObject.Destroy(child.gameObject);
                    }
                }
            }

            // If the new block ID is not null, create a new block at this position.
            if (newBlockId != null)
            {
                Cube cube = new Cube(position, parent, newBlockId);
            }
        }
    }

    public void DeleteMap(Transform parent)
    {
        foreach (Transform child in parent)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void RemoveBlocked(Transform parent)
    {
        for (int x = 1; x < width - 1; x++)
        {
            for (int y = 1; y < height; y++)
            {
                foreach (Transform child in parent)
                {
                    if (child.position == new Vector3(x, y, 2))
                    {
                        GameObject.Destroy(child.gameObject);
                    }
                }
            }
            foreach (Transform child in parent)
            {
                if (child.position == new Vector3(x, height - 1, 1))
                {
                    GameObject.Destroy(child.gameObject);
                }
            }
        }
    }
}
