using UnityEngine;
using Utilities.StateMachine;

public class EnemyIdleState : IState
{
    private EnemyBase owner;

    public MonoBehaviour Owner { get => owner; set => owner = (EnemyBase) value; }

    public void OnStateEnter()
    {
        owner.PlayAnimation(EnemyAnimations.Idle);
    }

    public void OnStateExit()
    {
        
    }

    public void Update(float TimeDeltaTime)
    {
        if(owner.target != null)
        {
            owner.stateMachine.ChangeState(owner.GetStateAfterPlayerDetection());
            return;
        }

        
    }
}
