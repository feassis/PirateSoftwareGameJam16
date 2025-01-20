using System;
using UnityEngine;

public class PlayerController
{
    private PlayerView view;
    private PlayerModel model;

    private Vector2 lookInput;
    private Vector2 moveInput;
    private float xCameraRotation = 10f;
    private bool isDashing = false;
    private float dashCooldownTimer = 0f;
    private float dashTimer = 0f;
    private float jumpCooldownTimer = 0f;
    private float jumpTimer = 0f;
    private bool isJumping = false;

    private WeaponController weaponController;
    private EventService eventService;

    public PlayerController(PlayerView view, PlayerModel model, Transform spawnPoint, EventService eventService)
    {
        this.eventService = eventService;
        this.view = GameObject.Instantiate<PlayerView>(view, spawnPoint.position, spawnPoint.rotation);
        this.view.SetController(this);
        this.model = model;
        this.model.SetController(this);
        InstantiateWeapon();
    }

    ~PlayerController()
    {
        eventService.OnWeaponDestroyed.RemoveListener(OnWeaponController);
    }

    private void OnWeaponController(WeaponController controller)
    {
        eventService.OnWeaponDestroyed.RemoveListener(OnWeaponController);

        InstantiateWeapon();
    }

    public void Look()
    {
        CharacterController characterController = view.GetCharacterController();
        characterController.transform.Rotate(Vector3.up * lookInput.x * model.LookSensitivityX * Time.deltaTime);
        xCameraRotation -= lookInput.y * model.LookSensitivityY * Time.deltaTime;
        xCameraRotation = Mathf.Clamp(xCameraRotation, model.VerticalLookLowerBounds, model.VerticalLookUperBounds);

        view.GetCameraTransform().localRotation = Quaternion.Euler(xCameraRotation, 0f, 0f);
    }

    private void InstantiateWeapon()
    {
        eventService.OnWeaponDestroyed.AddListener(OnWeaponController);
        WeaponModel weaponModel = new WeaponModel(model.WeaponSO);
        weaponController = new WeaponController(model.WeaponSO.WeaponPrefab, weaponModel, view.GetWeaponHolder(), eventService);
    }

    private float GetSpeed()
    {
        return isDashing ? model.DashSpeed : model.MovementSpeed;
    }

    public void Move()
    {
        CharacterController characterController = view.GetCharacterController();
        Vector3 move = view.transform.right * moveInput.x + view.transform.forward * moveInput.y;

        if (isDashing && move == Vector3.zero)
        {
            move = view.transform.forward.normalized;
        }

        move *= GetSpeed() * Time.deltaTime;


        if (characterController.isGrounded && characterController.velocity.y < 0)
        {
            move.y = -2f; // Small downward force to keep the character grounded
        }


        if (!characterController.isGrounded && !isJumping)
        {
            move.y += (model.Gravity + characterController.velocity.y) * Time.deltaTime;
        }

        if (isJumping)
        {
            move.y = model.JumpVelocity * Time.deltaTime;
        }

        move.y = Mathf.Clamp(move.y, model.MaxFallVelocity, model.MaxUpVelocity);
        characterController.Move(move);
    }

    public void Jump()
    {
        CharacterController characterController = view.GetCharacterController();
        if (characterController.isGrounded && jumpCooldownTimer <= 0)
        {
            isJumping = true;
            jumpCooldownTimer = model.JumpCooldown;
            jumpTimer = model.JumpUpwardsDuration;
        }
    }

    public void Dash()
    {
        if (dashCooldownTimer > 0 || isDashing)
        {
            return;
        }

        isDashing = true;
        dashCooldownTimer = model.DashCooldown;
        dashTimer = model.DashDuration;
    }

    public void MoveInputStop(Vector2 vector2)
    {
        moveInput = Vector2.zero;
    }

    public void MoveInput(Vector2 vector2)
    {
        moveInput = vector2.normalized;
    }

    public void LookInput(Vector2 vector2)
    {
        lookInput = vector2.normalized;
    }


    public void HandleJumpLogic()
    {
        if (jumpCooldownTimer > 0)
        {
            jumpCooldownTimer -= Time.deltaTime;
        }

        if (isJumping)
        {
            jumpTimer -= Time.deltaTime;

            if (jumpTimer <= 0)
            {
                isJumping = false;
            }
        }
    }

    public void HandleDashLogic()
    {
        if (dashCooldownTimer > 0)
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
}
