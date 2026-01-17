using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStats
{
    public ProjectileStats(float speed, float cadence, float damage) {this.speed = speed; this.cadence = cadence; this.damage = damage;}

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
                case ProjectileAttributes.test_1:
                    attributeManager.ActivateAtribute(new Test1Att(attributeManager));
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
}
public interface IAttribute
{
    public void OnSpawn();
    //public void OnUpdate();
    public void OnEnemyCollision(Collision hitEntity);
    public void OnEnemyKill();
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
}

public class Test1Att : IAttribute
{
    BulletAttributeManager owner;

    public Test1Att(BulletAttributeManager owner) { this.owner = owner; }

    public void OnSpawn()
    {
        Debug.Log("test 1 active");
    }
    //public void OnUpdate();
    public void OnEnemyCollision(Collision hitEntity) 
    { 

    }
    public void OnEnemyKill()
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
    public void OnEnemyCollision(Collision hitEntity)
    {

    }
    public void OnEnemyKill()
    {

    }
}