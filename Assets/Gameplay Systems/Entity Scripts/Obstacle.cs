using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : GameplayElement
{
    [SerializeField]
    private ObstacleStats stats = new();
    [SerializeField]
    private StaticObstacleStats staticStats;
    private List<ObstacleAflictions> conditions = new();

    private void Start()
    {
        stats.OnSpawn(staticStats);
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

    protected virtual void Move()
    {
        transform.position += staticStats.GetSpeed() * Time.deltaTime * Vector3.back;

        if (CheckForGoal())
            StealCookie();
    }

    protected bool CheckForGoal()
    {
        return transform.position.z < Global.COOKIE_LINE;
    }

    private void StealCookie()
    {
        GameObject.FindGameObjectWithTag("ServiceProvider").GetComponent<ServiceManager>().gameplay.LoseCookie(staticStats.GetObstacleType());
        Vanish();
    }

    public void OnHit(float damage, out bool isLethal)
    {
        stats.Hit(staticStats, damage, out isLethal);
        if (isLethal)
            Vanish();
    }

    public void Ignite()
    {
        conditions.Add(new Ignited(this));
    }

    public void RemoveCondition(ObstacleAflictions condition)
    {
        conditions.Remove(condition);
    }

    public virtual void IgnitionDamage()
    {
        OnHit(staticStats.GetWeakness(Weaknesses.fire), out _);
    }
}
