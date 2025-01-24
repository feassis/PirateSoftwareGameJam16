using UnityEngine;

public class BulletController
{
    private BulletView view;
    private BulletModel model;

    public BulletController(BulletView view, BulletModel model, Transform spawnPosition, Vector3 bulletDirection)
    {
        this.view = GameObject.Instantiate(view);
        this.view.transform.position = spawnPosition.position;
        this.view.transform.rotation = spawnPosition.rotation;
        this.view.GetRigidbody().linearVelocity = bulletDirection * model.Speed;
        this.view.StartLifeTime(model.LifeTime);
        this.model = model;
        this.view.SetController(this);
        this.model.SetController(this);
    }

    public float GetDamage()
    {
        return model.Damage;
    }
}
