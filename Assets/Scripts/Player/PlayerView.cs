using Input;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerView : MonoBehaviour, IDamageble
{
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private Transform cameraTransform;

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
        playerInput.Player.Jump.performed += ctx => Jump();
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


    private void Jump()
    {
        controller.Jump();
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
        throw new System.NotImplementedException();
    }

    public void Heal(float heal)
    {
        throw new System.NotImplementedException();
    }
}
