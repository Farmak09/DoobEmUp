using UnityEngine;
using System.IO;

[System.Serializable]
public class DefaultStats
{
    public float health;
    public float damage;
    public float cadence;
}
[System.Serializable]
public class MovementVariables
{
    [SerializeField]
    private float maxSpeed;
    private float speed = 0f;

    [SerializeField]
    private float accel;

    public void ResetVariables(DefaultStats stats)
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
    private float maxHP;
    private float currentHP;
    public void ResetVariables(DefaultStats stats)
    {
        maxHP = stats.health;
        currentHP = stats.health;
    }

}

[System.Serializable]
public class ShootingVariables
{
    [SerializeField]
    private float minDamage;
    [SerializeField]
    private float minAttackSpeed;


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


    public void ResetVariables(DefaultStats stats)
    {
        _damage = stats.damage;
        _cadence = stats.cadence;
    }
}

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/Dooby")]
public class PlayerStats : ScriptableObject
{
    [SerializeField]
    private MovementVariables movement;
    [SerializeField]
    private HealthVariables health;
    [SerializeField]
    private ShootingVariables weapon;


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

    public float BulletDamage()
    {
        return weapon.Damage;
    }
    public float BulletCadence()
    {
        return weapon.Cadence;
    }
    public void SetToDefault()
    {
        DefaultStats dStats = JsonUtility.FromJson<DefaultStats>(new StreamReader(Global.DEFAULT_STATS_PATH).ReadToEnd());

        movement.ResetVariables(dStats);
        health.ResetVariables(dStats);
        weapon.ResetVariables(dStats);
    }
}
