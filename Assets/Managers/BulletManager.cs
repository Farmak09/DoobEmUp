using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStats
{
    public ProjectileStats(float speed, float cadence, float Damage) {this.speed = speed; this.cadence = cadence; this.damage = damage;}

    public float speed { get; private set; }
    public float cadence { get; private set; }
    public float damage { get; private set; }
}

public class BulletManager : GameplayElement
{
    BulletAttributeManager attributeManager = new();
    [SerializeField]
    private Vector3 speed = Vector3.forward;
    private float damage = 0f;
    public void InitializeProjectile(ProjectileStats stats, List<ProjectileAttributes> attributes)
    {
        damage = stats.damage;
        ModifyProjectileSpeed(speed * stats.speed);
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
        transform.position += Time.deltaTime * speed;
    }


    private void OnTriggerEnter(Collider other)
    {
        attributeManager.ObstacleHit(other);
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

    public void ObstacleHit(Collider obstacle)
    {
        if (attributes.Count > 0)
            attributes.ForEach(x => x.OnObstacleCollision(obstacle));
    }
}

