using System.Collections.Generic;
using UnityEngine;

public class EnemyManager
{
    public static EnemyManager Instance { get; private set; }

    public int EnemyDeadCount { get; private set; }

    private EventService eventService;

    public List<EnemyBase> Enemies { get; private set; } = new List<EnemyBase>();

    public EnemyManager()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("EnemyManager already exists.");
        }
    }

    public void Init(EventService eventService)
    {
        this.eventService = eventService;
        this.eventService.OnEnemyDeath.AddListener(OnEnemyDeath);
    }

    public void SubscribeEnemy(EnemyBase enemy)
    {
        Enemies.Add(enemy);
    }

    private void OnEnemyDeath(EnemyBase enemy)
    {
        EnemyDeadCount++;

        if(EnemyDeadCount >= Enemies.Count)
        {
            eventService.OnAllEnemiesDead.InvokeEvent();
        }
    }

    ~EnemyManager()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }
}
