using Utilities.StateMachine;

public class EnemyShooterStateMachine : GenericStateMachine<EnemyBase>
{
    public EnemyShooterStateMachine(EnemyBase Owner) : base(Owner)
    {
        this.States.Add(State.IDLE, new EnemyIdleState());
        this.States.Add(State.CHASING, new EnemyChasingState());
        this.States.Add(State.SHOOTING, new EnemyShootingState());
        this.States.Add(State.AVOIDING, new EnemyShooterAvoidanceState());
        this.States.Add(State.ATTACKING, new EnemyShootingState());
        this.States.Add(State.DEATH, new EnemyDeathState());
        SetOwner();
    }
}
