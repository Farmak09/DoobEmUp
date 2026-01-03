using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElement : GameplayElement
{
    protected InputManager inputManager;

    [SerializeField]
    protected PlayerStats stats;


    public override void Awake()
    {
        base.Awake();
        inputManager = GetComponent<InputManager>();
        stats.SetToDefault();
    }
}
