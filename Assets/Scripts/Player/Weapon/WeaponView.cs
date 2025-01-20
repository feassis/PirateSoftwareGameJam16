using Input;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Rigidbody myRigidbody;
    [SerializeField] private LayerMask obstacleLayer;
    private WeaponController controller;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();
        playerInput.Player.Shoot.performed += ctx => OnShootPerformed();
        playerInput.Player.Throw.performed += ctx => OnThrowPerformed();
    }


    private void OnEnable()
    {
        if(playerInput != null)
        {
            playerInput.Enable();
        }
    }

    private void OnDisable()
    {
        if(playerInput != null)
        {
            playerInput.Disable();
        }
    }

    private void Update()
    {
        controller.Updade();
    }

    private void OnShootPerformed()
    {
        controller.Shoot();
    }

    private void OnThrowPerformed()
    {
        controller.Throw();
    }

    public void ThrowViewLogic()
    {
        transform.parent = null;
        playerInput.Disable();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (obstacleLayer == (obstacleLayer | (1 << collision.gameObject.layer)))
        {
            controller.DestroyWeapon();
        }
    }
}
