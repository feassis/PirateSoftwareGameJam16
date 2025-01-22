using System;
using UnityEngine;

public class PlayerDetectionTrigglerSphere : MonoBehaviour
{
    public event Action<PlayerView> OnPlayerEnterRange;
    public event Action<PlayerView> OnPlayerExitRange;

    private float attackRange;

    public void Setup(float attackRange)
    {
        this.attackRange = attackRange;
        GetComponent<SphereCollider>().radius = attackRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerView playerView))
        {
            OnPlayerEnterRange?.Invoke(playerView);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out PlayerView playerView))
        {
            OnPlayerExitRange?.Invoke(playerView);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
