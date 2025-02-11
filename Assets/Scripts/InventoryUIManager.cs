using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    public BlockSelector blockSelector; // Reference to your BlockSelector script

    // This method will be called when a trap button is clicked
    public void SelectTrap(string trapName)
    {
        blockSelector.SetSelectedTrap(trapName);
    }
}
