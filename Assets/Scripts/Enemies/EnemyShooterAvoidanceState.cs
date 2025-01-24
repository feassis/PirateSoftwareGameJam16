using UnityEngine;
using Utilities.StateMachine;

public class EnemyShooterAvoidanceState : IState
{
    private EnemyBase owner;

    public MonoBehaviour Owner { get => owner; set => owner = (EnemyBase)value; }

    public void OnStateEnter()
    {
        
    }

    public void OnStateExit()
    {
        owner.ResetNavAgent();
    }

    public void Update(float TimeDeltaTime)
    {
        if (owner.target == null)
        {
            owner.stateMachine.ChangeState(State.IDLE);
            return;
        }

        if (((EnemyShooter)owner).EnemyWeaponController.CanShoot())
        {
            owner.stateMachine.ChangeState(State.SHOOTING); 
            return;
        }

        owner.MoveAwayFromTheTarget(owner.target.transform, TimeDeltaTime);
    }
}
