using UnityEngine;

public class  WeaponModel
{
    public float Damage;
    public float FireRate;
    public float BulletSpeed;
    public float BulletLifeTime;
    public BulletView BulletPrefab;
    public float WeaponThrowVelocity;
    public float OverheatIncreasePerShot;
    public float OverheatDecreasePerSeconds;
    public float OverheatLimit;
    public float MinBlastRadious;
    public float MaxBlastRadious;
    public float MinBlastDamage;
    public float MaxBlastDamage;
    public float TimeToExplode;
    public LayerMask ExplosionTarget;
    public ParticleSystem ExplosionParticles;

    private WeaponController controller;

    public WeaponModel(WeaponSO weaponSO)
    {
        Damage = weaponSO.Damage;
        FireRate = weaponSO.FireRate;
        BulletSpeed = weaponSO.BulletSpeed;
        BulletLifeTime = weaponSO.BulletLifeTime;
        BulletPrefab = weaponSO.BulletPrefab;
        WeaponThrowVelocity = weaponSO.WeaponThrowVelocity;
        OverheatIncreasePerShot = weaponSO.OverheatIncreasePerShot;
        OverheatDecreasePerSeconds = weaponSO.OverheatDecreasePerSeconds;
        OverheatLimit = weaponSO.OverheatLimit;
        MinBlastRadious = weaponSO.MinBlastRadious;
        MaxBlastRadious = weaponSO.MaxBlastRadious;
        MinBlastDamage = weaponSO.MinBlastDamage;
        MaxBlastDamage = weaponSO.MaxBlastDamage;
        TimeToExplode = weaponSO.TimeToExplode;
        ExplosionTarget = weaponSO.ExplosionTarget;
        ExplosionParticles = weaponSO.ExplosionParticles;
    }

    public void SetController(WeaponController controller)
    {
        this.controller = controller;
    }
}
