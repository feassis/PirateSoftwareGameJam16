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
    }

    public void SetController(WeaponController controller)
    {
        this.controller = controller;
    }
}
