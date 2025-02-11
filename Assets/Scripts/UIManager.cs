using UnityEngine;
using TMPro;
using System.Data;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public GameObject InventoryPanel;
    public GameObject MenuPanel;
    public GameObject EndPanel;
    public GameObject TrapsPanel;
    public GameObject StartButton;
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI numberTraps;

    public MapGenerator mapGenerator;

    public string winner;
    private bool gameStarted;

    private PlayerSpawner playerSpawner;
    private CameraMover cameraMover;
    private PlayerManager playerManager;
    private PlayerInputManager playerInputManager;


    public void ToggleInventory(bool isActive) => InventoryPanel.SetActive(isActive);
    public void ToggleTraps(bool isActive) => TrapsPanel.SetActive(isActive);
    public void ToggleMenu(bool isActive) => MenuPanel.SetActive(isActive);
    public void ToggleEnd(bool isActive) => EndPanel.SetActive(isActive);
    public void ToggleStart(bool isActive) => StartButton.SetActive(isActive);

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }

    void Start()
    {
        playerSpawner = FindObjectOfType<PlayerSpawner>();
        cameraMover = FindObjectOfType<CameraMover>();
        mapGenerator = FindObjectOfType<MapGenerator>();
        playerManager = FindObjectOfType<PlayerManager>();

        textComponent.text = "New Text Here";  // Modify the text
        textComponent.color = Color.red;       // Modify the color

        ToggleInventory(false);
        ToggleTraps(false);
        ToggleMenu(true);
        ToggleEnd(false);
        ToggleStart(false);
        gameStarted = false;

        mapGenerator.Restart();
        playerInputManager = playerManager.GetComponent<PlayerInputManager>();
        playerInputManager.joinBehavior = PlayerJoinBehavior.JoinPlayersManually;
    }

    public void StartSpawnPhase()
    {
        ToggleInventory(false);
        ToggleTraps(false);
        ToggleMenu(false);
        ToggleEnd(false);
        ToggleStart(true);
        cameraMover.ResetCameraPosition();
        cameraMover.movementActivated = false;
        gameStarted = false;
        playerInputManager.joinBehavior = PlayerJoinBehavior.JoinPlayersWhenButtonIsPressed;
    }
    public void StartTrapsPhase()
    {
        ToggleInventory(true);
        ToggleTraps(true);
        ToggleMenu(false);
        ToggleEnd(false);
        ToggleStart(false);
        cameraMover.ResetCameraPosition();
        cameraMover.movementActivated = true;
        playerInputManager.joinBehavior = PlayerJoinBehavior.JoinPlayersManually;
    }

    public void StartGame()
    {
        ToggleInventory(false);
        ToggleTraps(false);
        ToggleMenu(false);
        ToggleEnd(false);
        cameraMover.movementActivated = false;
        gameStarted = true;
    }
    void EndGame(string win)
    {
        gameStarted = false;
        winner = win;
        ToggleInventory(false);
        ToggleTraps(false);
        ToggleMenu(true);
        ToggleEnd(true);
        if (winner == "runners")
        {
            textComponent.text = "RUNNERS WON !";  // Modify the text
            textComponent.color = Color.green;       // Modify the color
        }
        else if (winner == "trapper")
        {
            textComponent.text = "TRAPPER WON !";  // Modify the text
            textComponent.color = Color.blue;       // Modify the color
        }
        else
        {
            textComponent.text = winner;  // Modify the text
            textComponent.color = Color.black;       // Modify the color
        }
    }

    private void Update()
    {
        if (numberTraps.text == "0")
        {
            StartGame();
            //map generator create map without blocked
            numberTraps.text = "10";
            mapGenerator.RemoveBlocked();
        }

        if (playerManager.nbPlayers == 0 && gameStarted)
        {
            EndGame("trapper");
            mapGenerator.Restart();
        }
    }



    private void OnEnable() => EndCollider.OnBlockEnter += HandleBlockEnter;
    private void OnDisable() => EndCollider.OnBlockEnter -= HandleBlockEnter;

    private void HandleBlockEnter(Collider other) => EndGame("runners");
}
