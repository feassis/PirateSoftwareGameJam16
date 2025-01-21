using UnityEngine;

public class PlayerModel
{
    public float MaxHealth = 100f;
    public float MaxArmor = 20f;
    public float ArmorCooldown = 5f;
    public float ArmorRegenRate = 1f;

    public float MovementSpeed = 5f;
    public float DashSpeed = 15f;
    public float DashDuration = 0.3f;
    public float DashCooldown = 2f;
    public float MaxFallVelocity = -10f;
    public float MaxUpVelocity = 50f;
    public float JumpCooldown = 0.5f;
    public float JumpUpwardsDuration = 0.3f;
    public float JumpVelocity = 2f;
    public float Gravity = -5f;

    public float LookSensitivityX = 100f;
    public float LookSensitivityY = 3f;
    public float VerticalLookUperBounds = 90f;
    public float VerticalLookLowerBounds = -90f;

    public WeaponSO WeaponSO;

    private PlayerController playerController;

    public PlayerModel(PlayerSO playerSO)
    {
        MaxHealth = playerSO.MaxHealth;
        MaxArmor = playerSO.MaxArmor;
        ArmorCooldown = playerSO.ArmorCooldown;
        ArmorRegenRate = playerSO.ArmorRegenRate;

        MovementSpeed = playerSO.MovementSpeed;
        DashSpeed = playerSO.DashSpeed;
        DashDuration = playerSO.DashDuration;
        DashCooldown = playerSO.DashCooldown;
        MaxFallVelocity = playerSO.MaxFallVelocity;
        MaxUpVelocity = playerSO.MaxUpVelocity;
        JumpCooldown = playerSO.JumpCooldown;
        JumpUpwardsDuration = playerSO.JumpUpwardsDuration;
        JumpVelocity = playerSO.JumpVelocity;
        Gravity = playerSO.Gravity;

        LookSensitivityX = playerSO.LookSensitivityX;
        LookSensitivityY = playerSO.LookSensitivityY;
        VerticalLookUperBounds = playerSO.VerticalLookUperBounds;
        VerticalLookLowerBounds = playerSO.VerticalLookLowerBounds;

        WeaponSO = playerSO.WeaponSO;
    }

    public void SetController(PlayerController playerController)
    {
        this.playerController = playerController;
    }
}
