using UnityEngine;

[System.Serializable]
public class BoundedFloat
{
    public float min;
    public float basic;
    public float max;

    public float Clamp(float value)
    {
        if (value > max) value = max;
        else if (value < min) value = min;

        return value;
    }
}

[CreateAssetMenu(fileName = "StaticPlayerStats", menuName = "ScriptableObjects/Dooby")]
public class StaticPlayerStats : ScriptableObject
{
    //Health Stats
    [SerializeField] private float baseHP;
    public float GetBaseHP() { return baseHP; }


    //Movement Stats
    [SerializeField] private float topSpeed;
    public float GetTopSpeed() { return topSpeed; }

    [SerializeField] private float baseAcceleration;
    public float GetBaseAcceleration() { return baseAcceleration; }


    //Attack Stats
    [SerializeField] private BoundedFloat damage;
    public float GetDamage() { return damage.basic; }

    public float ClampDamage(float newValue)
    {
        return damage.Clamp(newValue);
    }


    [SerializeField] private BoundedFloat cadence;
    public float GetCadence() { return cadence.basic; }

    public float ClampCadence(float newValue)
    {
        return cadence.Clamp(newValue);
    }


    [SerializeField] private BoundedFloat bulletSpeed;
    public float GetBulletSpeed() { return bulletSpeed.basic; }

    public float ClampBulletSpeed(float newValue)
    {
        return bulletSpeed.Clamp(newValue);
    }
}

[System.Serializable]
public class PlayerStats
{
    public StaticPlayerStats statics;

    public bool selected = false;

    private float maxHP;
    public float currentHP { get; private set; }

    public WeaponStats weapon = new();

    private float speed = 0f;


    public void ModifyHP(float value)
    {
        currentHP += value;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }

    public void ModifyDamage(float percentage) { weapon.damage = statics.ClampDamage(weapon.damage * percentage); }
    public void ModifyCadence(float percentage) { weapon.cadence = statics.ClampCadence(weapon.cadence * percentage); }
    public void ModifyBulletSpeed(float percentage) { weapon.bulletSpeed = statics.ClampBulletSpeed(weapon.bulletSpeed * percentage); }



    public float GetSpeed(float target)
    {
        if (Mathf.Abs(target) - Time.deltaTime * Mathf.Abs(speed) < Global.MOVEMENT_DEADZONE)
        {
            speed = 0f;
            return 0f;
        }

        int direction = target < 0 ? -1 : 1;

        float ret = statics.GetTopSpeed() * direction;

        if (Mathf.Abs(speed) < statics.GetTopSpeed())
        {
            ret = speed;
            speed += Time.deltaTime * statics.GetBaseAcceleration() * direction;
        }
        return ret;
    }


    public void InitializeStats()
    {
        maxHP = statics.GetBaseHP();
        weapon.damage = statics.GetDamage();
        weapon.cadence = statics.GetCadence();
        weapon.bulletSpeed = statics.GetBulletSpeed();
    }
}

public class WeaponStats
{
    public float damage;
    public float cadence;
    public float bulletSpeed;
}