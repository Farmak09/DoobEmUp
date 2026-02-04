using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScriptsManager : GameplayElement
{
    public InputManager inputManager;
    List<PlayerElement> playerElements = new();

    public PlayerStats stats;


    public override void Awake()
    {
        base.Awake();
        inputManager = GetComponent<InputManager>();
        stats.InitializeStats();

    }

    private void Start()
    {
    }

    public override void GameUpdate()
    {
        playerElements.ForEach(x => x.PlayerUpdate());
    }



    public void AddPlayerElement(PlayerElement newElement)
    {
        playerElements.Add(newElement);
    }
}

public class PlayerElement : MonoBehaviour
{
    public PlayerScriptsManager player;

    private void Awake()
    {
        player = GetComponent<PlayerScriptsManager>();
        AddSelfToScriptsList();
    }

    private void AddSelfToScriptsList()
    {
        player.AddPlayerElement(this);
    }

    public virtual void PlayerUpdate()
    {

    }
}