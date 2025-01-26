using UnityEngine;
using Utilities.StateMachine;

public class EnemyGranadierThrowingState : IState
{
    protected EnemyGranadier owner;

    public MonoBehaviour Owner { get => owner; set => owner = (EnemyGranadier)value; }

    public void OnStateEnter()
    {
        owner.PlayAnimation(EnemyAnimations.Idle);
    }

    public void OnStateExit()
    {
       
    }

    public void Update(float TimeDeltaTime)
    {
        if (((EnemyGranadier)owner).CanThrowGranade())
        {
            ((EnemyGranadier)owner).ThrowGranade();
            return;
        }

        if (((EnemyGranadier)owner).IsOnAvoidanceRange)
        {
            owner.stateMachine.ChangeState(State.AVOIDING);
        }
    }
}

