using UnityEngine;
using Utilities.StateMachine;

public class EnemyChasingState : IState
{
    private EnemyBase owner;

    public MonoBehaviour Owner { get => owner; set => owner = (EnemyBase) value; }

    public void OnStateEnter()
    {
        owner.PlayAnimation(EnemyAnimations.Walking);
    }

    public void OnStateExit()
    {
        owner.ResetNavAgent();
    }

    public void Update(float deltaTime)
    {
        if(owner.target == null)
        {
            owner.stateMachine.ChangeState(State.IDLE);
            return;
        }

        if(owner.IsOnAttackRange)
        {
            owner.stateMachine.ChangeState(State.ATTACKING);
            return;
        }

        owner.MoveTo(owner.target.transform.position, deltaTime);
        owner.RotatesToMovePosition();
    }
}
