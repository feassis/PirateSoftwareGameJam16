using Input;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerView : MonoBehaviour, IDamageble
{
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image armorBar;

    private CharacterController characterController;

    private PlayerInput playerInput;

    public PlayerController controller;

    private void Awake()
    {
        SetupInput();
        characterController = GetComponent<CharacterController>();
    }

    private void SetupInput()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();
        playerInput.Player.Look.performed += ctx => LookInput(ctx.ReadValue<Vector2>());
        playerInput.Player.Look.canceled += ctx => LookInput(ctx.ReadValue<Vector2>());
        playerInput.Player.Move.performed += ctx => MoveInput(ctx.ReadValue<Vector2>());
        playerInput.Player.Move.canceled += ctx => MoveInputStop(ctx.ReadValue<Vector2>());
        playerInput.Player.Dash.performed += ctx => Dash();
        playerInput.Player.Jump.performed += ctx => JumpStart();
        playerInput.Player.Jump.canceled += ctx => JumpEnd();
    }

    public Transform GetWeaponHolder()
    {
        return weaponHolder;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        if (playerInput == null)
        {
            return;
        }

        playerInput.Enable();
    }

    private void OnDisable()
    {
        if (playerInput == null)
        {
            return;
        }

        playerInput.Disable();
    }


    private void JumpStart()
    {
        controller.JumpInputStart();
    }

    private void JumpEnd()
    {
        controller.JumpInputStop();
    }

    private void Dash()
    {
        controller.Dash();
    }

    private void MoveInputStop(Vector2 vector2)
    {
        controller.MoveInputStop(vector2);
    }

    private void MoveInput(Vector2 vector2)
    {
        controller.MoveInput(vector2);
    }

    private void LookInput(Vector2 vector2)
    {
        controller.LookInput(vector2);
    }

    
    private void Update()
    {
        controller.HandleDashLogic();
        controller.HandleJumpLogic();
        controller.Look();
        controller.Move();
        controller.HandleArmorRegeneration();
    }

    public CharacterController GetCharacterController()
    {
        return characterController;
    }

    public void SetController(PlayerController playerController)
    {
        this.controller = playerController;
    }

    public Transform GetCameraTransform()
    {
        return cameraTransform;
    }

    public void TakeDamage(float damage)
    {
        controller.TakeDamage(damage);
    }

    public void Heal(float heal)
    {
        controller.Heal(heal);
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void UpdateArmorBar(float currentArmor, float maxArmor)
    {
        armorBar.fillAmount = currentArmor / maxArmor;
    }
}
