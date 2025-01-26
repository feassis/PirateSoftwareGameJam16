public class BulletModel
{
    public float Damage;
    public float Speed;
    public float LifeTime;

    private BulletController controller;

    public BulletModel(float damage, float speed, float lifeTime)
    {
        Damage = damage;
        Speed = speed;
        LifeTime = lifeTime;
    }

    public void SetController(BulletController controller)
    {
        this.controller = controller;
    }
}