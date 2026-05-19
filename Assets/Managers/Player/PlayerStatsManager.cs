using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : PlayerElement
{
    public override void Awake()
    {
        type = TypeOfPlayerScripts.Stats;
        base.Awake();
    }
    public override void PlayerUpdate()
    {

    }
}
