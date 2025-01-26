using UnityEngine;
using Utilities.StateMachine;

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
