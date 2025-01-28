using System;
using System.Collections;
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
    private bool isJumpingButtonDown = false;

    private WeaponController weaponController;
    private EventService eventService;

    private float currentHealth;
    private float currentArmor;
    private float armorCooldownTimer = 0f;

    public PlayerController(PlayerView view, PlayerModel model, Transform spawnPoint, EventService eventService)
    {
        this.eventService = eventService;
        this.view = GameObject.Instantiate<PlayerView>(view, spawnPoint.position, spawnPoint.rotation);
        this.view.SetController(this);
        this.model = model;
        this.model.SetController(this);
        InstantiateWeapon();

        currentHealth = model.MaxHealth;
        currentArmor = model.MaxArmor;
        view.UpdateHealthBar(currentHealth, model.MaxHealth);
        view.UpdateArmorBar(currentArmor, model.MaxArmor);
    }

    ~PlayerController()
    {
        eventService.OnWeaponThrowed.RemoveListener(OnWeaponController);
        eventService.OnWeaponOverheat.RemoveListener(OnWeaponController);
    }

    private void OnWeaponController(WeaponController controller)
    {
        eventService.OnWeaponThrowed.RemoveListener(OnWeaponController);
        eventService.OnWeaponOverheat.RemoveListener(OnWeaponController);

        view.StartCoroutine(RespawnWeaponCoolDown());
    }

    private IEnumerator RespawnWeaponCoolDown()
    {
        yield return new WaitForSeconds(model.throwRespaenCoolDown);
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
        eventService.OnWeaponThrowed.AddListener(OnWeaponController);
        eventService.OnWeaponOverheat.AddListener(OnWeaponController);
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
            if(characterController.velocity.y > 0)
            {
                move.y += model.GravityDuringJumpUpWards * Time.deltaTime;
            }
            else
            {
                move.y += model.Gravity * Time.deltaTime;
            }
        }

        if (isJumping)
        {
            move.y = model.JumpVelocity * Time.deltaTime;
        }

        move.y = Mathf.Clamp(move.y, model.MaxFallVelocity, model.MaxUpVelocity);
        characterController.Move(move);
    }

    public void JumpInputStart()
    {
        isJumpingButtonDown = true;
    }

    public void JumpInputStop()
    {
        isJumpingButtonDown = false;
        isJumping = false;
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


        if(jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;

            if(jumpTimer <= 0)
            {
                isJumping = false;
            }
        }

        CharacterController characterController = view.GetCharacterController();

        if(isJumpingButtonDown && jumpCooldownTimer <= 0 && characterController.isGrounded && !isJumping)
        {
            isJumping = true;
            jumpCooldownTimer = model.JumpCooldown;
            jumpTimer = model.JumpMaxInputDuration;
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

    public void TakeDamage(float damage)
    {
        armorCooldownTimer = model.ArmorCooldown;

        if (currentArmor > 0)
        {
            currentArmor -= damage;
            if (currentArmor < 0)
            {
                currentHealth += currentArmor;
                currentArmor = 0;
            }
        }
        else
        {
            currentHealth -= damage;
        }

        view.UpdateHealthBar(currentHealth, model.MaxHealth);
        view.UpdateArmorBar(currentArmor, model.MaxArmor);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float heal)
    {
        currentHealth = Mathf.Clamp(currentHealth + heal, 0, model.MaxHealth);
        view.UpdateHealthBar(currentHealth, model.MaxHealth);
    }

    private void Die()
    {
        weaponController.ToggleWeaponUI(false);
        view.ToggleOverHeatBar(false);
        eventService.OnPlayerDeath.InvokeEvent(this);
    }

    public void HandleArmorRegeneration()
    {
        if(armorCooldownTimer > 0)
        {
            armorCooldownTimer -= Time.deltaTime;
        }
        else
        {
            currentArmor = Mathf.Clamp(currentArmor + model.ArmorRegenRate * Time.deltaTime, 0, model.MaxArmor);
        }
        view.UpdateArmorBar(currentArmor, model.MaxArmor);
    }
}
