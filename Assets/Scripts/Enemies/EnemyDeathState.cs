using UnityEngine;
using Utilities.StateMachine;

public class EnemyDeathState : IState
{
    private EnemyBase owner;

    public MonoBehaviour Owner { get => owner; set => owner = (EnemyBase) value; }

    public void OnStateEnter()
    {
        owner.PlayAnimation(EnemyAnimations.Death);
    }

    public void OnStateExit()
    {
        
    }

    public void Update(float TimeDeltaTime)
    {
        
    }
}
