using UnityEngine;
using System.IO;



public class SaveData : MonoBehaviour
{
    public GameData dataFile;

    // Start is called before the first frame update
    void Start()
    {
        dataFile = JsonUtility.FromJson<GameData>(new StreamReader(Global.SAVE_DATA_PATH).ReadToEnd());
    }

    public void SaveState()
    {

    }

    public void LoadState()
    {

    }

    public void ChangeDifficulty(Difficulty newDif)
    {
        dataFile.difficulty = newDif;
    }
}

[System.Serializable]
public class GameData
{
    public int test;
    public Difficulty difficulty;
}
