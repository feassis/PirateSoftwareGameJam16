using UnityEngine;

public class EventService
{
    public static EventService Instance { get; private set; }

    public EventController<WeaponController> OnWeaponDestroyed;
    public EventController<WeaponController> OnWeaponThrowed;
    public EventController<WeaponController> OnWeaponOverheat;
    public EventController<EnemyBase> OnEnemyDeath;
    public EventController<PlayerController> OnPlayerDeath;
    public EventController OnAllEnemiesDead;

    public EventService()
    {
        OnWeaponDestroyed = new EventController<WeaponController>();
        OnWeaponThrowed = new EventController<WeaponController>();
        OnEnemyDeath = new EventController<EnemyBase>();
        OnPlayerDeath = new EventController<PlayerController>();
        OnAllEnemiesDead = new EventController();
        OnWeaponOverheat = new EventController<WeaponController>();
    }

    public void Init()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("EventService already exists.");
        }
    }
}
