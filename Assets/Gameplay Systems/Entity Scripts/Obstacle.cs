using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : GameplayElement
{
    [SerializeField]
    private ObstacleStats stats;
    private List<ObstacleAltStatus> conditions = new();

    public override void Awake()
    {
        base.Awake();
        stats.OnSpawn();
    }
    public override void GameUpdate()
    {
        if (!stats.IsAlive()) return;

        if (conditions.Count > 0)
        {
            conditions.ForEach(x => x.Update());
        }
        Move();
    }

    private void Move()
    {
        transform.position += stats.movementSpeed * Time.deltaTime * Vector3.back;
    }

    public void OnHit(float damage, out bool isLethal)
    {
        stats.Hit(damage, out isLethal);
    }

    public void Ignite()
    {
        conditions.Add(new Ignited(this));
    }

    public void RemoveCondition(ObstacleAltStatus condition)
    {
        conditions.Remove(condition);
    }

    public virtual void IgnitionDamage()
    {
        OnHit(stats.fireDamage, out _);
    }
}
