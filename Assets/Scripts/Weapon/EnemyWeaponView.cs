using UnityEngine;

public class EnemyWeaponView : WeaponView
{
    [SerializeField] private float facingAngle;

    public bool IsFacingTarget(Transform target)
    {
        return PhisichsUtilities.IsAngleLessThan(transform.forward, target.position - transform.position, facingAngle);
    }

    public void RotateToTarget(Transform target)
    {
        if (target == null)
        {
            return;
        }
        transform.rotation = Quaternion.LookRotation(target.position - transform.position);
    }

    private void Update()
    {
        ((EnemyWeaponController)controller).Updade();
    }
}