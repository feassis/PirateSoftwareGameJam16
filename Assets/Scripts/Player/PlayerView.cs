using Input;
using System;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.InputSystem.XR;

[RequireComponent(typeof(CharacterController))]
public class PlayerView : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashDuration = 0.3f;
    [SerializeField] private float dashCooldown = 2f;
    [SerializeField] private float  maxFallVelocity = -10f;
    [SerializeField] private float  maxUpVelocity = 50f;
    [SerializeField] private float  jumpCooldown = 0.5f;
    [SerializeField] private float  jumpUpwardsDuration = 0.3f;
    [SerializeField] private float  jumpVelocity = 2f;
    [SerializeField] private float  gravity = -5f;
    [Header("Camera Settings")]
    [SerializeField] private float lookSensitivityX = 100f;
    [SerializeField] private float lookSensitivityY = 3f;
    [SerializeField] private float verticalLookUperBounds = 90f;
    [SerializeField] private float verticalLookLowerBounds = -90f;
    [SerializeField] private Transform cameraTransform;
    
    private CharacterController characterController;

    private PlayerInput playerInput;
    private Vector2 lookInput;
    private Vector2 moveInput;
    private float xCameraRotation = 10f;
    private bool isDashing = false;
    private float dashCooldownTimer = 0f;
    private float dashTimer = 0f;
    private float jumpCooldownTimer = 0f;
    private float jumpTimer = 0f;
    private bool isJumping = false;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();
        playerInput.Player.Look.performed += ctx => LookInput(ctx.ReadValue<Vector2>());
        playerInput.Player.Look.canceled += ctx => LookInput(ctx.ReadValue<Vector2>());
        playerInput.Player.Move.performed += ctx => MoveInput(ctx.ReadValue<Vector2>());
        playerInput.Player.Move.canceled += ctx => MoveInputStop(ctx.ReadValue<Vector2>());
        playerInput.Player.Dash.performed += ctx => Dash();
        playerInput.Player.Jump.performed += ctx => Jump();
        characterController = GetComponent<CharacterController>();
    }

    private void Jump()
    {
        if(characterController.isGrounded && jumpCooldownTimer <= 0)
        {
            isJumping = true;
            jumpCooldownTimer = jumpCooldown;
            jumpTimer = jumpUpwardsDuration;
        }
    }

    private void Dash()
    {
        if (dashCooldownTimer > 0 || isDashing)
        {
            return;
        }

        isDashing = true;
        dashCooldownTimer = dashCooldown;
        dashTimer = dashDuration;
    }

    private void MoveInputStop(Vector2 vector2)
    {
        moveInput = Vector2.zero;
    }

    private void MoveInput(Vector2 vector2)
    {
        moveInput = vector2.normalized;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LookInput(Vector2 vector2)
    {
        lookInput = vector2.normalized;
    }

    private void OnEnable()
    {
        if(playerInput == null)
        {
            return;
        }

        playerInput.Enable();
    }

    private void OnDisable()
    {
        if(playerInput == null)
        {
            return;
        }

        playerInput.Disable();
    }

    private void Update()
    {
        HandleDashLogic();
        HandleJumpLogic();
        Look();
        Move();
    }

    private void HandleJumpLogic()
    {
        if(jumpCooldownTimer > 0)
        {
            jumpCooldownTimer -= Time.deltaTime;
        }

        if (isJumping)
        {
            jumpTimer -= Time.deltaTime;

            if(jumpTimer <= 0)
            {
                isJumping = false;
            }
        }
    }

    private void HandleDashLogic()
    {
        if(dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                isDashing = false;
                dashTimer = 0f;
            }
        }
    }

    private float GetSpeed()
    {
        return isDashing ? dashSpeed : movementSpeed;
    }

    private void Move()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        if(isDashing && move == Vector3.zero)
        {
            move = transform.forward.normalized;
        }

        move *= GetSpeed() * Time.deltaTime;


        if (characterController.isGrounded && characterController.velocity.y < 0)
        {
            move.y = -2f; // Small downward force to keep the character grounded
        }
     

        if (!characterController.isGrounded && !isJumping)
        {
            move.y += (gravity + characterController.velocity.y) * Time.deltaTime;
        }

        if(isJumping)
        {
            move.y = jumpVelocity * Time.deltaTime;
        }

        move.y = Mathf.Clamp(move.y, maxFallVelocity, maxUpVelocity);
        characterController.Move(move);
    }

    private void Look()
    {
        characterController.transform.Rotate(Vector3.up * lookInput.x * lookSensitivityX * Time.deltaTime);
        xCameraRotation -= lookInput.y * lookSensitivityY * Time.deltaTime;
        xCameraRotation = Mathf.Clamp(xCameraRotation, verticalLookLowerBounds, verticalLookUperBounds);

        cameraTransform.localRotation = Quaternion.Euler(xCameraRotation, 0f, 0f);
    }
}
