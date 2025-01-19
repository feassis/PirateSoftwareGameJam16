using UnityEngine;

public class WeaponController
{
    private WeaponView view;
    private WeaponModel model;

    private float shootTimer = 0f;

    public WeaponController(WeaponView view, WeaponModel model, Transform gunParent)
    {
        this.view = GameObject.Instantiate(view, gunParent);
        this.model = model;
        this.view.SetController(this);
        this.model.SetController(this);
    }

    public void Updade()
    {
        if(shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }
    }

    public void Shoot()
    {
        if(shootTimer > 0)
        {
            return;
        }

        BulletModel bulletModel = new BulletModel(model.Damage, model.BulletSpeed, model.BulletLifeTime);
        BulletController bulletController = new BulletController(model.BulletPrefab, bulletModel, view.GetShootPointTransform(), view.GetForwardDirection());

        shootTimer =  GetWeaponCoolDown();
    }

    private float GetWeaponCoolDown()
    {
        return 1 / model.FireRate;
    }
}
