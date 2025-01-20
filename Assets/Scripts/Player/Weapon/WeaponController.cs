using UnityEngine;

public class WeaponController
{
    private WeaponView view;
    private WeaponModel model;

    private float shootTimer = 0f;
    private EventService eventService;

    public WeaponController(WeaponView view, WeaponModel model, Transform gunParent, EventService eventService)
    {
        this.view = GameObject.Instantiate(view, gunParent);
        this.model = model;
        this.view.SetController(this);
        this.model.SetController(this);
        this.eventService = eventService;
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

    public void Throw()
    {
        Vector3 direction = view.GetForwardDirection();

        view.ThrowViewLogic();
        Rigidbody rigidbody = view.GetRigidbody();
        rigidbody.isKinematic = false;
        rigidbody.linearVelocity = direction * model.WeaponThrowVelocity;
    }

    private float GetWeaponCoolDown()
    {
        return 1 / model.FireRate;
    }

    public void DestroyWeapon()
    {
        eventService.OnWeaponDestroyed.InvokeEvent(this);
        GameObject.Destroy(view.gameObject);
    }
}
