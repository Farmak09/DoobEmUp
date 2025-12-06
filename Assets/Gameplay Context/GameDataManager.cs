using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
    InGame,
    Paused,
    MainMenu
}

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager instance;

    private GameState state;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

            state = GameState.InGame;
        }
    }

    public GameState CurrentState() { return state; }
    public void ChangeState(GameState newState) 
    {
        state = newState;

        //TODO warn player controller of pase state here
    }

}
