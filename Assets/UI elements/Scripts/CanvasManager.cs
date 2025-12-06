using UnityEngine;
using UnityEngine.InputSystem;



public class CanvasManager : MonoBehaviour
{
    private InputManager inputManager;


    [SerializeField] private GameObject pauseMenu;


    private void Awake()
    {
        inputManager = GetComponent<InputManager>();

    }
    private void Start()
    {
        inputManager.pause.started += PauseGame;

    }
    private void PauseGame(InputAction.CallbackContext context)
    {
        switch (GameDataManager.instance.CurrentState())
        {
            case GameState.InGame:
                Pause();
                GameDataManager.instance.ChangeState(GameState.Paused);
                break;
            case GameState.Paused:
                Unpause();
                GameDataManager.instance.ChangeState(GameState.InGame);
                break;
            case GameState.MainMenu:
                break;
            default:
                break;
        }
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        pauseMenu.GetComponent<CanvasActivationTransition>().TurnOff(false);

    }
    private void Unpause()
    {
        pauseMenu.GetComponent<CanvasActivationTransition>().TurnOff(true);

    }
}
