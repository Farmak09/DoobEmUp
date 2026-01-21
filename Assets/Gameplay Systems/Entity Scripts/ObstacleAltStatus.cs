using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ObstacleAltStatus
{
    public void Update();
}

public class Ignited : ObstacleAltStatus
{
    private Obstacle owner;
    private float duration = 5f;
    private int damageTicks = 5;

    public Ignited(Obstacle owner) 
    { 
        this.owner = owner;
    }

    public void Update()
    {
        duration -= Time.deltaTime;

        if (duration <= 0f)
        {
            owner.RemoveCondition(this);
            return;
        }

        if(duration < damageTicks)
        {
            owner.IgnitionDamage();
            damageTicks--;
        }
    }
}