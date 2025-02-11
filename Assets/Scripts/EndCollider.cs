using UnityEngine;

public class EndCollider : MonoBehaviour
{
    public delegate void EndEnterEvent(Collider other);
    public static event EndEnterEvent OnBlockEnter;

    private UIManager uiManager;  // Make this private and assign in Awake or Start

    private void Start()
    {
        // Automatically find and assign the UIManager component
        uiManager = FindObjectOfType<UIManager>();

        if (uiManager == null)
        {
            Debug.LogWarning("UIManager not found in the scene.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (uiManager != null)
            {
                uiManager.winner = "runners";
            }

            // Notify subscribers that the block's collider has been entered
            OnBlockEnter?.Invoke(other);
        }
    }
}
