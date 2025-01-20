using System;
using System.Collections;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageble
{
    [SerializeField] private float MaxHealth = 100;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material damageMaterial;
    [SerializeField] private Collider myCollider;
    
    private Material defaultMaterial;

    private float currentHealth;

    private void Awake()
    {
        defaultMaterial = meshRenderer.material;
        currentHealth = MaxHealth;
    }

    public void Heal(float heal)
    {
        currentHealth = Mathf.Clamp(currentHealth + heal, 0, MaxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, MaxHealth);
        

        if(currentHealth == 0)
        {
            Die();
        }
        else
        {
            TakeDamageVisuals();
        }
    }

    protected virtual void TakeDamageVisuals()
    {
        StartCoroutine(DamageVisuals());
    }

    protected virtual IEnumerator DamageVisuals()
    {
        meshRenderer.material = damageMaterial;
        yield return new WaitForSeconds(0.1f);
        meshRenderer.material = defaultMaterial;
    }

    protected virtual void Die()
    {
        meshRenderer.material = damageMaterial;
        meshRenderer.transform.localPosition = new Vector3(0, 0.5f, 0);
        meshRenderer.transform.localRotation = Quaternion.Euler(90, 0, 0);
        myCollider.enabled = false;
    }
}
