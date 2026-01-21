using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileAttributes
{
    flammable,
    test_2
}

public class WeaponManager : PlayerElement
{
    private List<ProjectileAttributes> activeAttributes = new();

    [SerializeField]
    private float weaponCooldown = 0f;

    [SerializeField] private BulletManager projectile;

    private bool shoot = false;

    public void AddAttributeToWeapon(ProjectileAttributes newAtt)
    {
        activeAttributes.Add(newAtt);
    }


    void Start()
    {
        ResetCooldown();
        AddAttributeToWeapon(ProjectileAttributes.flammable);
    }

    public override void PlayerUpdate()
    {
        weaponCooldown -= Time.deltaTime;

        if(weaponCooldown <= 0f)
        {
            shoot = true;
            ResetCooldown();
        }
    }

    private void ResetCooldown()
    {
        weaponCooldown = player.stats.BulletStats().cadence;
    }

    private void Shoot()
    {
        Instantiate(projectile, this.transform.position, this.transform.rotation).InitializeProjectile(player.stats.BulletStats(), activeAttributes);
    }

    private void LateUpdate()
    {
        if (shoot)
        {
            Shoot();
            shoot = false;
        }
    }
}
