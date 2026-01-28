using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StaticObstacleStats", menuName = "ScriptableObjects/Obstacle")]
public class StaticObstacleStats : ScriptableObject
{
    [SerializeField] private ObstacleType type;
    public ObstacleType GetObstacleType() { return type; }


    [SerializeField] private float maxHP;
    public float GetMaxHP() { return maxHP; }


    [SerializeField] private float movementSpeed;
    public float GetSpeed() { return movementSpeed; }


    [SerializeField] private float damageAbsortion;
    public float GetAbsortion() { return damageAbsortion; }


    [SerializeField] private float fireWeakness;
    public float GetWeakness(Weaknesses type)
    {
        switch (type)
        {
            case Weaknesses.fire:
                return fireWeakness;
            default:
                return 0f;
        }
    }
}
[System.Serializable]
public class ObstacleStats
{
    [SerializeField]
    private float currentHP = float.MaxValue;

    public void OnSpawn(StaticObstacleStats stats)
    {
        ModifyHP(stats, float.MaxValue);
    }

    public void ModifyHP(StaticObstacleStats stats, float value)
    {
        currentHP += value;
        if (currentHP > stats.GetMaxHP()) currentHP = stats.GetMaxHP();
    }
    public virtual void Hit(StaticObstacleStats stats, float bulletDamage, out bool isLethal, bool bypassArmor = false)
    {
        float finalDamage = -bulletDamage + (bypassArmor ? 0 : stats.GetAbsortion());

        if (finalDamage > 0f) finalDamage = 0f;

        ModifyHP(stats, finalDamage);


        if (currentHP <= 0f)
            isLethal = true;
        else
            isLethal = false;
    }
    public bool IsAlive()
    {
        return currentHP >= 0f;
    }
}

public enum Weaknesses
{
    fire
}

public enum ObstacleType
{
    test,
    test2
}
