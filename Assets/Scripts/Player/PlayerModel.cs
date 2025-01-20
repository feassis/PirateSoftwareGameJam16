using UnityEngine;

public class PlayerModel
{
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

    public float Damage = 5f;
    public float FireRate = 2f;
    public float BulletSpeed = 30f;
    public float BulletLifeTime = 3f;
    public float WeaponThrowVelocity = 20f;
    public WeaponView WeaponPrefab;
    public BulletView BulletPrefab;

    private PlayerController playerController;

    public PlayerModel(PlayerSO playerSO)
    {
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

        Damage = playerSO.Damage;
        FireRate = playerSO.FireRate;
        BulletSpeed = playerSO.BulletSpeed;
        BulletLifeTime = playerSO.BulletLifeTime;
        WeaponThrowVelocity = playerSO.WeaponThrowVelocity;
        WeaponPrefab = playerSO.WeaponPrefab;
        BulletPrefab = playerSO.BulletPrefab;
    }

    public void SetController(PlayerController playerController)
    {
        this.playerController = playerController;
    }
}
