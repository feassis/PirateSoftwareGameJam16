using UnityEngine;

public class EventService
{
    public EventController<WeaponController> OnWeaponDestroyed;

    public EventService()
    {
        OnWeaponDestroyed = new EventController<WeaponController>();
    }
}
