using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using Utilities.StateMachine;

public class EnemyGranadier : EnemyBase
{
    [SerializeField] private float attackDamage;
    [SerializeField] private float explosionRadius;
    [SerializeField] private EnemyGranade granadePrefab;
    [SerializeField] private float granadeCooldown = 3;
    [SerializeField] private PlayerDetectionTrigglerSphere avoidanceDetection;
    [SerializeField] private float avoidanceRange;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float timeToTarget = 3f;

    public bool IsOnAvoidanceRange { get; private set; }

    private float granadeTimer;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyGranadierStateMachine(this);
        stateMachine.ChangeState(State.IDLE);

        avoidanceDetection.OnPlayerEnterRange += OnAvoidanceRangeEnter;
        avoidanceDetection.OnPlayerExitRange += OnAvoidanceRangeExit;

        avoidanceDetection.Setup(avoidanceRange);
    }

    private void OnAvoidanceRangeEnter(PlayerView view)
    {
        IsOnAvoidanceRange = true;
    }

    private void OnAvoidanceRangeExit(PlayerView view)
    {
        IsOnAvoidanceRange = false;
    }

    private void Update()
    {
        if (granadeTimer > 0)
        {
            granadeTimer -= Time.deltaTime;
        }

        stateMachine.Update(Time.deltaTime);
    }

    public bool CanThrowGranade()
    {
        return granadeTimer <= 0;
    }




    public void ThrowGranade()
    {
        granadeTimer = granadeCooldown;

        EnemyGranade granade = Instantiate(granadePrefab, spawnPoint.position, Quaternion.identity);
        granade.Setup(attackDamage, explosionRadius, target.GetGranadeAimAt());
        granade.Rb.linearVelocity = PhisichsUtilities.CalculateThrowVelocity(spawnPoint.position, target.GetGranadeAimAt().position, timeToTarget);
    }
}

public class EnemyGranadierStateMachine : GenericStateMachine<EnemyBase>
{
    public EnemyGranadierStateMachine(EnemyBase owner) : base  (owner)
    {
        States.Add(State.IDLE, new EnemyIdleState());
        States.Add(State.CHASING, new EnemyShooterChasingState());
        States.Add(State.ATTACKING, new EnemyGranadierThrowingState());
        States.Add(State.SHOOTING, new EnemyGranadierThrowingState());
        States.Add(State.AVOIDING, new EnemyShooterAvoidanceState());
        States.Add(State.DEATH, new EnemyDeathState());
        SetOwner();
    }
}

