﻿using System;
using System.Collections;
using UnityEngine;

public class BulletView : MonoBehaviour
{
    [SerializeField] private Rigidbody myRigidbody;
    [SerializeField] private LayerMask obstacleLayer;
    
    private BulletController controller;

    public void SetController(BulletController controller)
    {
        this.controller = controller;
    }

    public Rigidbody GetRigidbody()
    {
        return myRigidbody;
    }

    public void StartLifeTime(float lifeTime)
    {
        StartCoroutine(LifeTime(lifeTime));
    }

    private IEnumerator LifeTime(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (obstacleLayer == (obstacleLayer | (1 << other.gameObject.layer)))
        {
            Destroy(gameObject);
            return;
        }

        if(other.TryGetComponent(out IDamageble damageble))
        {
            damageble.TakeDamage(controller.GetDamage());
            Destroy(gameObject);
        }
    }
}
