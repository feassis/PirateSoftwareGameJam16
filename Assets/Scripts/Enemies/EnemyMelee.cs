using UnityEngine;
using Utilities.StateMachine;

public class EnemyMelee : EnemyBase
{
    [SerializeField] protected float attackDamage = 10;
    [SerializeField] protected PlayerDetectionTrigglerGeneric swordDetection;

    public PlayerDetectionTrigglerGeneric SwordTrigguer { get => swordDetection;}

    public float AttackDamage { get => attackDamage; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyMeleeStateMachine(this);
        stateMachine.ChangeState(State.IDLE);
    }
}
