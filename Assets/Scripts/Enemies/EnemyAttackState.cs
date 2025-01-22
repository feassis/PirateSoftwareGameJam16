using System;
using UnityEngine;
using Utilities.StateMachine;

public class EnemyAttackState : IState
{
    private EnemyBase owner;

    public MonoBehaviour Owner { get => owner; set => owner = (EnemyBase) value; }

    private float attackTimer = 0;

    public void OnStateEnter()
    {
        if(owner is EnemyMelee)
        {
            EnemyMelee melee = (EnemyMelee) owner;
            melee.SwordTrigguer.OnPlayerEnterRange += OnPlayerEnterRange;
        }
    }

    public void OnStateExit()
    {
        if(owner is EnemyMelee)
        {
            EnemyMelee melee = (EnemyMelee) owner;
            melee.SwordTrigguer.OnPlayerEnterRange -= OnPlayerEnterRange;
        }
    }

    private void OnPlayerEnterRange(PlayerView view)
    {
        view.TakeDamage(((EnemyMelee)owner).AttackDamage);
    }

    public void Update(float deltaTime)
    {
        if(!owner.IsOnAttackRange)
        {
            owner.stateMachine.ChangeState(State.IDLE);
            return;
        }

        if(attackTimer <= 0)
        {
            owner.PlayAnimation(EnemyAnimations.Attack);
            attackTimer = owner.AttackCoolDown;
        }
        else
        {
            attackTimer -= deltaTime;
        }

        owner.FacePlayer();
    }
}
