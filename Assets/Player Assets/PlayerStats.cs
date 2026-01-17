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
public class HealthPlayerVariables
{
    [SerializeField]
    private float maxHP;
    private float currentHP;
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

    private float _damage;
    public float Damage
    {
        get { return _damage; }
    }
    public void ModifyDamage(float percentage)
    {
        _damage *= percentage;
        if (_damage < minDamage)
        {
            _damage = minDamage;
        }
    }


    private float _cadence;
    public float Cadence
    {
        get { return _cadence; }
    }
    public void ModifyCadence(float percentage)
    {
        _cadence *= percentage;
        if (_cadence > minAttackSpeed)
        {
            _cadence = minAttackSpeed;
        }
    }

    private float _speed;
    public float Speed
    {
        get { return _speed; }
    }
    public void ModifySpeed(float percentage)
    {
        _speed *= percentage;
        if (_speed > minProjectileSpeed)
        {
            _speed = minProjectileSpeed;
        }
    }
    public void ResetVariables(DefaultPlayerStats stats)
    {
        _damage = stats.damage;
        _cadence = stats.cadence;
        _speed = stats.speed;
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
        return new ProjectileStats(weapon.Speed, weapon.Cadence, weapon.Damage);
    }

    public void SetToDefault()
    {
        DefaultPlayerStats dStats = JsonUtility.FromJson<DefaultPlayerStats>(new StreamReader(Global.DEFAULT_STATS_PATH).ReadToEnd());

        movement.ResetVariables(dStats);
        health.ResetVariables(dStats);
        weapon.ResetVariables(dStats);
    }
}
