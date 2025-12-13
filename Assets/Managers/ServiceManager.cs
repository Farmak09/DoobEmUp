using UnityEngine;
using UnityEngine.SceneManagement;

public class ServiceManager : MonoBehaviour
{
    public GameDataManager gameData = new();

    void Awake()
    {
        if (gameData == null) gameData = new();

        DontDestroyOnLoad(this.gameObject); 

        SceneManager.LoadScene("Canvases", LoadSceneMode.Additive);
    }
}

