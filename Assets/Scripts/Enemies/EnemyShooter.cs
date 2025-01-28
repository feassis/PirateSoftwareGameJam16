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

    private void OnAvoidanceRangeEnter(PlayerView view)
    {
        IsOnAvoidanceRange = true;
    }

    private void OnAvoidanceRangeExit(PlayerView view)
    {
        IsOnAvoidanceRange = false;
    }

    protected override void Start()
    {
        base.Start();
        InstantiateWeapon();
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
