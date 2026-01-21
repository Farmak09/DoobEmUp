using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "ObstacleStats", menuName = "ScriptableObjects/Obstacle")]
public class ObstacleStats : ScriptableObject
{
    [SerializeField]
    private HealthVariables health;
    [SerializeField]
    private float damageAbsortion;

    public float fireDamage;

    public void OnSpawn()
    {
        health.ModifyHP(float.MaxValue);
    }

    public virtual void Hit(float bulletDamage, out bool isLethal, bool bypassArmor = false)
    {
        float finalDamage = -bulletDamage + (bypassArmor ? 0 : damageAbsortion);
        Debug.Log(finalDamage);

        if (finalDamage > 0f) finalDamage = 0f;

        health.ModifyHP(finalDamage);


        if (health.currentHP <= 0f)
            isLethal = true;
        else
            isLethal = false;
    }
}
