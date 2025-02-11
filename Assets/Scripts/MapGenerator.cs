using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class MapGenerator : MonoBehaviour
{
    public int MapWidth = 50;
    public int MapHeight = 5; // Set to at least 5 to accommodate pillars
    public int MapDepth = 50;
    public Transform mapParentObject;
    public TextMeshProUGUI mapName;
    
    private string backgroundName;
    private Map map;
    private GameObject background;


    private void Start()
    {
        // Initialize the map with dimensions
        map = new Map();

        backgroundName = "Nature";
        mapName.text = backgroundName;
    }

    public void ChangeBackground(string name)
    {
        backgroundName = name;
        mapName.text = backgroundName;
    }
    public void CreateMap(bool blocked)
    {
        

        map.DeleteMap(mapParentObject);

        if (background != null)
        {
            // Destroy Background
            GameObject.Destroy(background);
        }

        // Load the background prefab from the Resources folder
        GameObject backgroundPrefab = Resources.Load<GameObject>("Backgrounds/" + backgroundName);
        if (backgroundPrefab == null)
        {
            Debug.LogWarning(name);
            Debug.LogWarning("Backgrounds/Nature");
        }

        // Instantiate the background and set it as a child of mapParentObject
        background = GameObject.Instantiate(backgroundPrefab, mapParentObject);

        // Optionally, reset the position of the background if needed
        background.transform.position = background.transform.localPosition; // Or set a custom offset if needed
        
        
        // Initialize map data
        map.InitializeMapData(blocked);

        // Generate the map based on mapData and set it under the mapParentObject
        map.GenerateMap(mapParentObject);
    }


    public void ModifyBlockAt(Vector3 position, string newCube)
    {
        map.ModifyBlockAt(position, newCube, mapParentObject);
    }

    public void RemoveBlocked()
    {
        map.RemoveBlocked(mapParentObject);
    }

    public void Restart()
    {
        // Destroy Map
        map.DeleteMap(mapParentObject);

        // Destroy Background
        GameObject.Destroy(background);


        // Load the background prefab from the Resources folder
        background = Resources.Load<GameObject>("Backgrounds/Nature");


        // Instantiate the background and set it as a child of mapParentObject
        background = GameObject.Instantiate(background, mapParentObject);

        // Optionally, reset the position of the background if needed
        background.transform.position = background.transform.localPosition; // Or set a custom offset if needed
    }
    private void OnEnable()
    {
        EndCollider.OnBlockEnter += HandleBlockEnter;
    }

    private void OnDisable()
    {
        EndCollider.OnBlockEnter -= HandleBlockEnter;
    }

    private void HandleBlockEnter(Collider other)
    {

        // Destroy Map
        map.DeleteMap(mapParentObject);

        // Destroy Background
        GameObject.Destroy(background);


        // Load the background prefab from the Resources folder
        background = Resources.Load<GameObject>("Backgrounds/Nature");


        // Instantiate the background and set it as a child of mapParentObject
        background = GameObject.Instantiate(background, mapParentObject);

        // Optionally, reset the position of the background if needed
        background.transform.position = background.transform.localPosition; // Or set a custom offset if needed
    }
}
