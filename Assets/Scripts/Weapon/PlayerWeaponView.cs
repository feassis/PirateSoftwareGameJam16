using Input;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponView : WeaponView
{
    
    [SerializeField] protected Image overheatBar;
    [SerializeField] protected GameObject overheatUI;
    [SerializeField] protected List<OverheatColorPercentage> overheatColors = new List<OverheatColorPercentage>();
    
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


    public override void ThrowViewLogic()
    {
        transform.parent = null;
        playerInput.Disable();
        transform.Rotate(0, 90, 0);
    }

    public override void ToggleWeaponUI(bool isActive)
    {
        overheatUI.gameObject.SetActive(isActive);
    }


    public override void UpdateOverheatUI(float overheatPercentage)
    {
        overheatBar.fillAmount = overheatPercentage;
        for (int i = 0; i < overheatColors.Count; i++)
        {
            if (overheatColors[i].Percentage >= overheatPercentage)
            {
                overheatBar.color = overheatColors[i].Color;
                break;
            }
        }
    }
}
