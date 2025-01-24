using static UnityEngine.UI.GridLayoutGroup;

public class EnemyShooterChasingState : EnemyChasingState
{
    public virtual void Update(float deltaTime)
    {
        if (owner.target == null)
        {
            owner.stateMachine.ChangeState(Utilities.StateMachine.State.IDLE);
            return;
        }

        if (owner.IsOnAttackRange)
        {
            owner.stateMachine.ChangeState(Utilities.StateMachine.State.SHOOTING);
            return;
        }

        owner.MoveTo(owner.target.transform.position, deltaTime);
        owner.RotatesToMovePosition();
    }
}