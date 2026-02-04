using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : GameplayElement
{
    BulletAttributeManager attributeManager = new();
    [SerializeField]
    private Vector3 speed = Vector3.forward;
    private WeaponStats stats;
    public void InitializeProjectile(WeaponStats newStats, List<ProjectileAttributes> attributes)
    {
        stats = newStats;
        SetUpAttributes(attributes);
    }


    public void ModifyProjectileSpeed(Vector3 newSpeed)
    {
        speed = newSpeed;
    }



    private void SetUpAttributes(List<ProjectileAttributes> attributes)
    {
        foreach(ProjectileAttributes attribute in attributes)
        {
            switch (attribute)
            {
                case ProjectileAttributes.flammable:
                    attributeManager.ActivateAtribute(new Flammable(attributeManager));
                    break;
                case ProjectileAttributes.test_2:
                    attributeManager.ActivateAtribute(new Test2Att(attributeManager));
                    break;
            }
        }
    }

    public override void GameUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position += Time.deltaTime * speed * stats.bulletSpeed;
    }


    private void OnTriggerEnter(Collider other)
    {
        attributeManager.ObstacleHit(other, stats.damage);
        Vanish();
    }
}

public class BulletAttributeManager
{
    public List<IAttribute> attributes = new();

    public void ActivateAtribute(IAttribute attribute)
    {
        if (!attributes.Contains(attribute))
        {
            attributes.Add(attribute);

            attribute.OnSpawn();
        }
    }

    public void ObstacleHit(Collider obstacle, float damage)
    {
        ApplyDamageToEnemy(obstacle, damage);
        ApplyOnHitAttributeEffects(obstacle);
    }

    private void ApplyDamageToEnemy(Collider hitObstacle, float damage)
    {
        Obstacle obstacle = hitObstacle.gameObject.GetComponent<Obstacle>();

        obstacle.OnHit(damage, out bool isLethal);
        if(isLethal)
        {
            ApplyOnDeathAttributeEffects();
        }
    }

    private void ApplyOnHitAttributeEffects(Collider obstacle)
    {
        if (attributes.Count > 0)
            attributes.ForEach(x => x.OnObstacleCollision(obstacle));
    }

    private void ApplyOnDeathAttributeEffects()
    {
        if (attributes.Count > 0)
            attributes.ForEach(x => x.OnObstacleKill());
    }
}

