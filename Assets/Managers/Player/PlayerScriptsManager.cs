using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScriptsManager : GameplayElement
{
    public InputManager inputManager;
    private List<PlayerElement> playerElements = new();

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

    public PlayerElement FindScriptInList(TypeOfPlayerScripts type)
    {
        return playerElements.Find(x => x.type == type);
    }

    public void AddPlayerElement(PlayerElement newElement)
    {
        Debug.Log(newElement.type);

        if (playerElements.FindAll(x => x.type == newElement.type).Count == 0)
            playerElements.Add(newElement);
        else
            Debug.Log("tried to add a player script twice");
    }
}

public enum TypeOfPlayerScripts
{
    Movement,
    Weapon,
    Animation,
    Stats
}

public class PlayerElement : MonoBehaviour
{
    public PlayerScriptsManager player;
    public TypeOfPlayerScripts type;

    public virtual void Awake()
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