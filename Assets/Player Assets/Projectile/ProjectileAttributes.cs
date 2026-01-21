using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttribute
{
    public void OnSpawn();
    //public void OnUpdate();
    public void OnObstacleCollision(Collider hitObstacle);
    public void OnObstacleKill();
}


public class Flammable : IAttribute
{
    BulletAttributeManager owner;

    public Flammable(BulletAttributeManager owner) { this.owner = owner; }

    public void OnSpawn()
    {
        Debug.Log("flame on");
    }
    //public void OnUpdate();
    public void OnObstacleCollision(Collider hitObstacle)
    {

        if (hitObstacle.gameObject.GetComponent<Obstacle>() != null)
        {
            hitObstacle.gameObject.GetComponent<Obstacle>().Ignite();
        }
    }
    public void OnObstacleKill()
    {

    }
}
public class Test2Att : IAttribute
{
    BulletAttributeManager owner;

    public Test2Att(BulletAttributeManager owner) { this.owner = owner; }

    public void OnSpawn()
    {
        Debug.Log("test 2 active");
    }
    //public void OnUpdate();
    public void OnObstacleCollision(Collider hitObstacle)
    {

    }
    public void OnObstacleKill()
    {

    }
}