public class  WeaponModel
{
    public float Damage;
    public float FireRate;
    public float BulletSpeed;
    public float BulletLifeTime;
    public BulletView BulletPrefab;
    public float WeaponThrowVelocity;

    private WeaponController controller;

    public WeaponModel(float damage, float fireRate, float bulletSpeed, float bulletLifeTime, float weaponThrowVelocity, BulletView bulletPrefab)
    {
        Damage = damage;
        FireRate = fireRate;
        BulletSpeed = bulletSpeed;
        BulletLifeTime = bulletLifeTime;
        BulletPrefab = bulletPrefab;
        WeaponThrowVelocity = weaponThrowVelocity;
    }

    public void SetController(WeaponController controller)
    {
        this.controller = controller;
    }
}
