using UnityEngine;

public class EnemyGranade : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private Transform targetLocationPrefab;

    private float damage;
    private float explosionRadius;
    private Transform target;
    public Rigidbody Rb { get => rb; }
    private Transform targetLocation;


    public void Setup(float damage, float explosionRadius, Transform target)
    {
        this.damage = damage;
        this.explosionRadius = explosionRadius;
        this.target = target;
        targetLocation = Instantiate(targetLocationPrefab, target.position, Quaternion.identity);
        targetLocation.transform.localScale = Vector3.one * explosionRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collisionLayer == (collisionLayer | (1 << other.gameObject.layer)))
        {
            Explode();
        }
    }

    private void Explode()
    {
        if(targetLocation != null)
        {
            Destroy(targetLocation.gameObject);
        }

        Instantiate(explosionParticles, transform.position, Quaternion.identity);
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in colliders)
        {
            if (collisionLayer == (collisionLayer | (1 << collider.gameObject.layer)))
            {
                collider.GetComponent<IDamageble>()?.TakeDamage(damage);
            }
        }
    }
}

