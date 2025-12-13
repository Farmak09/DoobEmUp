using UnityEngine;
using UnityEngine.InputSystem;



public class CanvasManager : MonoBehaviour
{
    private InputManager inputManager;
    private ServiceManager services;


    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;


    private void Awake()
    {
        inputManager = GetComponent<InputManager>();


        services = GameObject.FindGameObjectWithTag("ServiceProvider").GetComponent<ServiceManager>();
    }
    private void Start()
    {
        inputManager.pause.started += PauseGame;
    }

    private void PauseGame(InputAction.CallbackContext context)
    {
        switch (services.gameData.CurrentState())
        {
            case GameState.InGame:
                LoadPause(true);
                services.gameData.ChangeState(GameState.PauseMenu);
                break;
            case GameState.PauseMenu:
                LoadPause(false);
                services.gameData.ChangeState(GameState.InGame);
                break;
            case GameState.SettingsMenu:
                LoadPause(true);
                LoadSettings(false);
                services.gameData.ChangeState(GameState.PauseMenu);
                break;            
            case GameState.MainMenu:
                break;
            default:
                break;
        }
    }

    public void LoadPause(bool value)
    {
        if (value) pauseMenu.SetActive(true);
        pauseMenu.GetComponent<CanvasActivationTransition>().TurnOff(!value);
    }

    public void LoadSettings(bool value)
    {
        if (value)
        {
            settingsMenu.SetActive(true);
            services.gameData.ChangeState(GameState.SettingsMenu);
            LoadPause(false);
        }
        settingsMenu.GetComponent<CanvasActivationTransition>().TurnOff(!value);
    }
}
