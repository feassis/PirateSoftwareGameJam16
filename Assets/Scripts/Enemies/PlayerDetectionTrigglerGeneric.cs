using System;
using UnityEngine;

public class PlayerDetectionTrigglerGeneric : MonoBehaviour
{
    public event Action<PlayerView> OnPlayerEnterRange;
    public event Action<PlayerView> OnPlayerExitRange;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerView playerView))
        {
            OnPlayerEnterRange?.Invoke(playerView);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerView playerView))
        {
            OnPlayerExitRange?.Invoke(playerView);
        }
    }
}
