using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float moveSpeed = 10f; // Speed at which the camera moves
    public float borderThreshold = 10f; // Distance from the edge of the screen to trigger the movement

    public bool isFlipped = false;
    public bool movementActivated = true;

    private GameObject[] players;
    private GameObject player;

    void Update()
    {
        // Update the player references each frame
        players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 0)
        {
            // Find the player with the lowest z position
            player = players[0];
            float lowestZ = player.transform.position.z;

            foreach (GameObject p in players)
            {
                if (p.transform.position.z < lowestZ)
                {
                    player = p;
                    lowestZ = p.transform.position.z;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            FlipCamera();
        }

        Vector3 camPosition = transform.position;
        Vector2 mousePosition = Input.mousePosition;

        // Get screen dimensions
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        if (isFlipped)
        {
            if (movementActivated)
            {
                // Move camera left when the mouse is near the left or top border of the screen
                if (mousePosition.x >= screenWidth - borderThreshold || mousePosition.y >= screenHeight - borderThreshold)
                {
                    camPosition.z += moveSpeed * Time.deltaTime; // Move left
                }
                // Move camera right when the mouse is near the right or bottom border of the screen
                else if (mousePosition.x <= borderThreshold || mousePosition.y <= borderThreshold)
                {
                    camPosition.z -= moveSpeed * Time.deltaTime; // Move right
                }
            }
        }
        else
        {
            if (movementActivated)
            {
                // Move camera left when the mouse is near the left or top border of the screen
                if (mousePosition.x <= borderThreshold || mousePosition.y >= screenHeight - borderThreshold)
                {
                    camPosition.z += moveSpeed * Time.deltaTime; // Move left
                }
                // Move camera right when the mouse is near the right or bottom border of the screen
                else if (mousePosition.x >= screenWidth - borderThreshold || mousePosition.y <= borderThreshold)
                {
                    camPosition.z -= moveSpeed * Time.deltaTime; // Move right
                }
            }
        }

        if (!movementActivated && player != null)
        {
            camPosition.z = player.transform.position.z - 5;
        }

        // Apply the new position
        transform.position = camPosition;
    }

    public void FlipCamera()
    {
        isFlipped = !isFlipped; // Toggle the flipped state

        Vector3 camPosition = transform.position;
        Vector3 camRotation = transform.eulerAngles;

        if (isFlipped)
        {
            camPosition.x = 8f;
            camRotation.y = -37.4f;
        }
        else
        {
            camPosition.x = 0f;
            camRotation.y = 37.4f;
        }

        // Apply the new position and rotation
        transform.position = camPosition;
        transform.eulerAngles = camRotation;
    }

    public void ResetCameraPosition()
    {
        Vector3 camPosition = transform.position;
        Debug.Log(camPosition);
        if (isFlipped)
        {
            camPosition.x = 8f;
        }
        else
        {
            camPosition.x = 0f;
        }
        camPosition.z = -2f;
        transform.position = camPosition;
        Debug.Log(transform.position);
    }
}
