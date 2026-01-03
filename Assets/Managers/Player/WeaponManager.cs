using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileAttributes
{
    test_1,
    test_2
}

public class WeaponManager : PlayerElement
{
    private List<ProjectileAttributes> activeAttributes = new();

    [SerializeField]
    private float weaponCooldown = 0f;

    [SerializeField] private BulletManager projectile;

    public void AddAttributeToWeapon(ProjectileAttributes newAtt)
    {
        activeAttributes.Add(newAtt);
    }
    // Start is called before the first frame update
    void Start()
    {
        ResetCooldown();
    }
    public override void GameUpdate()
    {
        weaponCooldown -= Time.deltaTime;

        if(weaponCooldown <= 0f)
        {
            Shoot();
            ResetCooldown();
        }
    }

    private void ResetCooldown()
    {
        Debug.Log(stats.BulletCadence());

        weaponCooldown = stats.BulletCadence();
    }

    private void Shoot()
    {
        //TODO make speed a player stat and get the bullet speed from there

        Instantiate(projectile, this.transform.position, this.transform.rotation).InitializeProjectile(3.5f, activeAttributes);
    }
}
