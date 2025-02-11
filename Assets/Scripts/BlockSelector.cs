using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems; // Add this namespace

public class BlockSelector : MonoBehaviour
{
    public Camera mainCamera;                // Assign in Inspector
    public MapGenerator MapGenerator;        // Reference to your MapGenerator script
    public string[] availableTraps;          // List of all available traps
    public string[] onBlocks;                // Traps to be placed on top of blocks

    private string selectedTrap;             // Currently selected trap

    private int numberTraps;
    public TextMeshProUGUI trapsUI;

    private void Start()
    {
        // Initialize with the first available trap or default
        if (availableTraps != null && availableTraps.Length > 0)
        {
            selectedTrap = availableTraps[0];
        }

        // Initialize onBlocks if not set in the Inspector
        if (onBlocks == null || onBlocks.Length == 0)
        {
            onBlocks = new string[] { "Boulder", "Murlanceur" }; // Default traps placed on top
        }

        numberTraps = int.Parse(trapsUI.text);
    }

    public void SetSelectedTrap(string trapName)
    {
        if (availableTraps.Contains(trapName))
        {
            selectedTrap = trapName;
            Debug.Log("Selected Trap: " + selectedTrap);
        }
        else
        {
            Debug.LogWarning("Trap not found in available traps: " + trapName);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
        {
            // Check if the pointer is over a UI element
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                // Pointer is over a UI element, do not process click
                return;
            }

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object is on the "Cube" layer
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Cube"))
                {
                    numberTraps = int.Parse(trapsUI.text);
                    Vector3 position = hit.collider.transform.position;
                    Debug.Log("Hit an object on the 'Cube' layer.");
                    Debug.Log(trapsUI.text);
                    Debug.Log(numberTraps);
                    if (selectedTrap != null && numberTraps > 0)
                    {
                        // Check if the selected trap should be placed on top
                        if (onBlocks.Contains(selectedTrap))
                        {
                            // Adjust position to place the trap on top of the block
                            position = new Vector3(position.x, position.y + 1, position.z);
                            MapGenerator.ModifyBlockAt(position, selectedTrap);
                        }
                        else
                        {
                            // Replace the block at the clicked position
                            MapGenerator.ModifyBlockAt(position, selectedTrap);
                        }
                        numberTraps -= 1;
                        trapsUI.text = numberTraps.ToString();
                    }
                    else
                    {
                        Debug.LogWarning("No trap selected or not enough traps left!");
                    }
                }
            }
        }
    }
}
