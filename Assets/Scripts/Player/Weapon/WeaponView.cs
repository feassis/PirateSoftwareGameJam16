using Input;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    private WeaponController controller;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();
        playerInput.Player.Shoot.performed += ctx => OnShootPerformed();
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
}
