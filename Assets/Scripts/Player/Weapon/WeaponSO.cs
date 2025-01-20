using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon")]
public class WeaponSO : ScriptableObject
{
    public float Damage;
    public float FireRate;
    public float BulletSpeed;
    public float BulletLifeTime;
    public BulletView BulletPrefab;
    public WeaponView WeaponPrefab;
    public float WeaponThrowVelocity;
    public float OverheatIncreasePerShot;
    public float OverheatDecreasePerSeconds;
    public float OverheatLimit;
}
