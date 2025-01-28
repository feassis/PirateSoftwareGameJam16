using System;
using UnityEngine;

public class WeaponView : MonoBehaviour
{
    [SerializeField] protected Transform shootPoint;
    [SerializeField] protected Rigidbody myRigidbody;
    [SerializeField] protected LayerMask obstacleLayer;
    protected WeaponController controller;

    public virtual void ThrowViewLogic()
    {
        transform.parent = null;
        transform.Rotate(0, 90, 0);
    }

    public Rigidbody GetRigidbody()
    {
        return myRigidbody;
    }

    public void SetController(WeaponController controller)
    {
        this.controller = controller;
    }

    public Vector3 GetForwardDirection()
    {
        return shootPoint.forward;
    }

    public Transform GetShootPointTransform()
    {
        return shootPoint;
    }

    public virtual void LookToTarget(Transform targetPosition)
    {
        
    }

    public virtual void UpdateOverheatUI(float overheatPercentage)
    {
        
    }

    public virtual void ToggleWeaponUI(bool isActive)
    {
        
    }
}
