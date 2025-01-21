using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    [SerializeField] private float healAmount = 10f;
    [SerializeField] private LayerMask possibleTargets;

    private void OnTriggerEnter(Collider other)
    {
        if(possibleTargets == (possibleTargets | (1 << other.gameObject.layer)))
        {
            other.GetComponent<IDamageble>().Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
