using UnityEngine;
using UnityEngine.SceneManagement;

public class ServiceManager : MonoBehaviour
{
    public GameDataManager gameData;
    public GameplayManager gameplay;

    void Awake()
    {
        SceneManager.LoadScene("Canvases", LoadSceneMode.Additive);

        DontDestroyOnLoad(this.gameObject);

        InitializeServices();
    }

    public void InitializeServices()
    {
        if (gameData == null) gameData = new();
        if (gameplay == null) gameplay = new();
    }
}

