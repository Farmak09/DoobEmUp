using UnityEngine;
using System.IO;

[System.Serializable]
public class DefaultPlayerStats
{
    public float health;
    public float damage;
    public float cadence;
    public float speed;
}
[System.Serializable]
public class MovementPlayerVariables
{
    [SerializeField]
    private float maxSpeed;
    private float speed = 0f;

    [SerializeField]
    private float accel;

    public void ResetVariables(DefaultPlayerStats stats)
    {
        speed = 0f;
    }

    public float GetSpeed(float target)
    {
        if (Mathf.Abs(target) - Time.deltaTime * Mathf.Abs(speed) < Global.MOVEMENT_DEADZONE)
        {
            speed = 0f;
            return 0f;
        }

        int direction = target < 0 ? -1 : 1;

        float ret = maxSpeed * direction;

        if (Mathf.Abs(speed) < maxSpeed)
        {
            ret = speed;
            speed += Time.deltaTime * accel * direction;
        }
        return ret;
    }
}

[System.Serializable]
public class HealthVariables
{
    [SerializeField]
    protected float maxHP;
    public float currentHP { get; protected set; }

    public void ModifyHP(float value)
    {
        currentHP += value;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }
}

[System.Serializable]
public class HealthPlayerVariables : HealthVariables
{

    public void ResetVariables(DefaultPlayerStats stats)
    {
        maxHP = stats.health;
        currentHP = stats.health;
    }

}

[System.Serializable]
public class ShootingPlayerVariables
{
    [SerializeField]
    private float minDamage;
    [SerializeField]
    private float minAttackSpeed;
    [SerializeField]
    private float minProjectileSpeed;

    public float damage { get; private set; }
    public float cadence { get; private set; }
    public float speed { get; private set; }

    public void ResetVariables(DefaultPlayerStats stats)
    {
        damage = stats.damage;
        cadence = stats.cadence;
        speed = stats.speed;
    }

    public void ModifyDamage(float percentage)
    {
        damage *= percentage;
        if (damage < minDamage)
        {
            damage = minDamage;
        }
    }
    public void ModifyCadence(float percentage)
    {
        cadence *= percentage;
        if (cadence > minAttackSpeed)
        {
            cadence = minAttackSpeed;
        }
    }
    public void ModifySpeed(float percentage)
    {
        speed *= percentage;
        if (speed > minProjectileSpeed)
        {
            speed = minProjectileSpeed;
        }
    }
}

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/Dooby")]
public class PlayerStats : ScriptableObject
{
    [SerializeField]
    private MovementPlayerVariables movement;
    [SerializeField]
    private HealthPlayerVariables health;
    [SerializeField]
    private ShootingPlayerVariables weapon;


    private bool _controlled = false;
    public bool Controlled
    {
        get { return _controlled; }
        set { _controlled = value; }
    }

    public float GetSpeed(float direction)
    {
        return movement.GetSpeed(direction);
    }

    public ProjectileStats BulletStats()
    {
        return new ProjectileStats(weapon.speed, weapon.cadence, weapon.damage);
    }

    public void SetToDefault()
    {
        DefaultPlayerStats dStats = JsonUtility.FromJson<DefaultPlayerStats>(new StreamReader(Global.DEFAULT_STATS_PATH).ReadToEnd());

        movement.ResetVariables(dStats);
        health.ResetVariables(dStats);
        weapon.ResetVariables(dStats);
    }
}
