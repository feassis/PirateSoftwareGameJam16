using Utilities.StateMachine;

public class EnemyMeleeStateMachine : GenericStateMachine<EnemyBase>
{
    public EnemyMeleeStateMachine(EnemyBase Owner) : base(Owner)
    {
        this.States.Add(State.IDLE, new EnemyIdleState());
        this.States.Add(State.CHASING, new EnemyChasingState());
        this.States.Add(State.ATTACKING, new EnemyAttackState());
        SetOwner();
    }
}
