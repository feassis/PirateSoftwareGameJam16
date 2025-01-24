using System;
using UnityEngine;

public class EnemyWeaponController : WeaponController
{
    public EnemyWeaponController(WeaponView view, WeaponModel model, Transform gunParent, EventService eventService) : base(view, model, gunParent, eventService)
    {


    }

    public override void Updade()
    {
        if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }

        
    }

    public override void Shoot()
    {
        if (shootTimer > 0)
        {
            return;
        }

        BulletModel bulletModel = new BulletModel(model.Damage, model.BulletSpeed, model.BulletLifeTime);
        BulletController bulletController = new BulletController(model.BulletPrefab, bulletModel, view.GetShootPointTransform(), view.GetForwardDirection());

        shootTimer = GetWeaponCoolDown();
    }

    public bool CanShoot()
    {
        return shootTimer <= 0;
    }

    private EnemyWeaponView GetEnemyWeaponView()
    {
        return view as EnemyWeaponView;
    }

    public void LookAt(Transform transform)
    {
        GetEnemyWeaponView().RotateToTarget(transform);
    }
}