using UnityEngine;

public class Cube
{
    public Vector3 Position { get; private set; }
    public GameObject CubeObject { get; private set; }

    public Cube(Vector3 position, Transform parent, string prefab)
    {
        Position = position;

        // Load the wall prefab from the Resources folder
        GameObject wallPrefab = Resources.Load<GameObject>(prefab); // Replace "WallPrefab" with your prefab's name (without the extension)

        if (wallPrefab != null)
        {
            // Instantiate the wall prefab at the specified position
            CubeObject = GameObject.Instantiate(wallPrefab, Position, Quaternion.identity, parent);
        }
        else
        {
            Debug.LogError("Wall prefab could not be found in Resources!");
        }
    }
}