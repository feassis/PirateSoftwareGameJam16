using System;
using UnityEngine;
using UnityEngine.Rendering;
using Utilities.StateMachine;

public class EnemyShooter : EnemyBase
{
    [SerializeField] private Transform weaponTransform;
    [SerializeField] private PlayerDetectionTrigglerSphere avoidanceDetection;
    [SerializeField] private float avoidanceRange;
    [SerializeField] private float attackDamage;
    [SerializeField] private WeaponSO weaponSO;
    [SerializeField] private WeaponView weaponView;

    private EnemyWeaponController enemyWeaponController;

    public EnemyWeaponController EnemyWeaponController { get => enemyWeaponController; }

    public bool IsOnAvoidanceRange { get; private set; }

    public float AttackDamage { get => attackDamage; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyShooterStateMachine(this);
        stateMachine.ChangeState(State.IDLE);

        avoidanceDetection.OnPlayerEnterRange += OnAvoidanceRangeEnter;
        avoidanceDetection.OnPlayerExitRange += OnAvoidanceRangeExit;

        avoidanceDetection.Setup(avoidanceRange);
    }

    private void Start()
    {
        InstantiateWeapon();
    }

    private void OnAvoidanceRangeEnter(PlayerView view)
    {
        IsOnAvoidanceRange = true;
    }

    private void OnAvoidanceRangeExit(PlayerView view)
    {
        IsOnAvoidanceRange = false;
    }

    private void InstantiateWeapon()
    {
        WeaponModel model = new WeaponModel(weaponSO);
        enemyWeaponController = new EnemyWeaponController(weaponView, model, weaponTransform, GameManager.Instance.EventService);
    }

    public override State GetStateAfterPlayerDetection()
    {
        return State.CHASING;
    }

    public void PointGunToPlayer()
    {
        if(target == null)
        {
            return;
        }

        enemyWeaponController.LookAt(target.GetAimAtPosition());
    }

    public override State GetAttackState()
    {
        return State.SHOOTING;
    }

    private void Update()
    {
        stateMachine.Update(Time.deltaTime);
    }
}


public class EnemyShooterStateMachine : GenericStateMachine<EnemyBase>
{
    public EnemyShooterStateMachine(EnemyBase Owner) : base(Owner)
    {
        this.States.Add(State.IDLE, new EnemyIdleState());
        this.States.Add(State.CHASING, new EnemyChasingState());
        this.States.Add(State.SHOOTING, new EnemyShootingState());
        this.States.Add(State.AVOIDING, new EnemyShooterAvoidanceState());
        this.States.Add(State.ATTACKING, new EnemyShootingState());
        this.States.Add(State.DEATH, new EnemyDeathState());
        SetOwner();
    }
}

public class EnemyShootingState : IState
{
    private EnemyBase owner;

    public MonoBehaviour Owner { get => owner; set => owner = (EnemyBase)value;}

    public void OnStateEnter()
    {
        owner.PlayAnimation(EnemyAnimations.Idle);
    }

    public void OnStateExit()
    {
        
    }

    public void Update(float TimeDeltaTime)
    {
        owner.FacePlayer();
        ((EnemyShooter)owner).PointGunToPlayer();

        if(((EnemyShooter)owner).EnemyWeaponController.CanShoot())
        {
            owner.PlayAnimation(EnemyAnimations.Shoot);
            ((EnemyShooter)owner).EnemyWeaponController.Shoot();
            return;
        }

        if(owner.target == null)
        {
            owner.stateMachine.ChangeState(State.IDLE);
            return;
        }

        if(((EnemyShooter)owner).IsOnAvoidanceRange)
        {
            owner.stateMachine.ChangeState(State.AVOIDING);
            return;
        }
    }
}
