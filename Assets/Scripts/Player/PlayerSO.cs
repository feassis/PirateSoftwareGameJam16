using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "ScriptableObjects/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    public PlayerView PlayerViewPrefab;

    [Header("Movement Settings")]
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

    [Header("Camera Settings")]
    public float LookSensitivityX = 100f;
    public float LookSensitivityY = 3f;
    public float VerticalLookUperBounds = 90f;
    public float VerticalLookLowerBounds = -90f;

    [Header("Weapon Settings")]
    public WeaponSO WeaponSO;
}