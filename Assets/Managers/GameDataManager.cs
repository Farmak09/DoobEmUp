using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
    InGame,
    PauseMenu,
    SettingsMenu,
    MainMenu
}

public class GameDataManager : MonoBehaviour
{
    private GameState state = GameState.InGame;


    public GameState CurrentState() { return state; }
    public void ChangeState(GameState newState) 
    {
        state = newState;

        //TODO warn player controller of pase state here
    }

}
