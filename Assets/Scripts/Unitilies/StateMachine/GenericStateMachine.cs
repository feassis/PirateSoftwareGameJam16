using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

namespace Utilities.StateMachine
{
    public class GenericStateMachine<T> where T : MonoBehaviour
    {
        protected T Owner;
        public IState currentState { get; protected set; }
        protected Dictionary<States, IState> States = new Dictionary<States, IState>();

        public GenericStateMachine(T Owner) => this.Owner = Owner;

        public void Update() => currentState?.Update();

        protected void ChangeState(IState newState)
        {
            currentState?.OnStateExit();
            currentState = newState;
            currentState?.OnStateEnter();
        }

        public void ChangeState(States newState) => ChangeState(States[newState]);

        protected void SetOwner()
        {
            foreach (IState state in States.Values)
            {
                state.Owner = Owner;
            }
        }
    }
}