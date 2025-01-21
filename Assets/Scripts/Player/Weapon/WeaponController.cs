using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class WeaponController
{
    private WeaponView view;
    private WeaponModel model;

    private float shootTimer = 0f;
    private EventService eventService;
    private float overheatAmount = 0f;

    public WeaponController(WeaponView view, WeaponModel model, Transform gunParent, EventService eventService)
    {
        this.view = GameObject.Instantiate(view, gunParent);
        this.model = model;
        this.view.SetController(this);
        this.model.SetController(this);
        this.eventService = eventService;
        view.UpdateOverheatUI(0f);
    }

    public void Updade()
    {
        if(shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }

        if(overheatAmount > 0)
        {
            overheatAmount = Mathf.Max(0, overheatAmount - model.OverheatDecreasePerSeconds * Time.deltaTime);
            view.UpdateOverheatUI(Mathf.Min(overheatAmount / model.OverheatLimit, 1f));
        }
    }

    public void Shoot()
    {
        if(shootTimer > 0)
        {
            return;
        }

        overheatAmount += model.OverheatIncreasePerShot;
        view.UpdateOverheatUI(Mathf.Min(overheatAmount / model.OverheatLimit, 1f));
        BulletModel bulletModel = new BulletModel(model.Damage, model.BulletSpeed, model.BulletLifeTime);
        BulletController bulletController = new BulletController(model.BulletPrefab, bulletModel, view.GetShootPointTransform(), view.GetForwardDirection());

        shootTimer =  GetWeaponCoolDown();

        if(overheatAmount >= model.OverheatLimit)
        {
            BlowUp();
        }
    }

    public void Throw()
    {
        Vector3 direction = view.GetForwardDirection();

        view.ThrowViewLogic();
        Rigidbody rigidbody = view.GetRigidbody();
        rigidbody.isKinematic = false;
        rigidbody.linearVelocity = direction * model.WeaponThrowVelocity;

        if(model.TimeToExplode > 0)
        {
            view.StartCoroutine(GranadeRoutine());
        }
    }

    private IEnumerator GranadeRoutine()
    {
        yield return new WaitForSeconds(model.TimeToExplode);
        BlowUp();
    }

    private float GetWeaponCoolDown()
    {
        return 1 / model.FireRate;
    }

    public void BlowUp()
    {
        float radious = model.MinBlastRadious + (model.MaxBlastRadious - model.MinBlastRadious) * Mathf.Clamp(overheatAmount / model.OverheatLimit, 0,1);

        Collider[] colliders = Physics.OverlapSphere(view.transform.position, radious, model.ExplosionTarget);
        DebugExtension.DebugWireSphere(view.transform.position, Color.red, radious, 1f);
        GameObject.Instantiate(model.ExplosionParticles.gameObject, view.transform.position, Quaternion.identity);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.TryGetComponent<IDamageble>(out IDamageble damageble))
            {
                float damage = model.MinBlastDamage + (model.MaxBlastDamage - model.MinBlastDamage) * Mathf.Clamp(overheatAmount / model.OverheatLimit, 0, 1);
                damageble.TakeDamage(damage);
            }
        }

        eventService.OnWeaponDestroyed.InvokeEvent(this);
        GameObject.Destroy(view.gameObject);
    }
}
