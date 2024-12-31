using System.Collections;
using UnityEngine;

namespace Characters.Enemy.States
{
    public class Hit : EnemyState
    {
        public override void StartState(EnemyStateMachine stateMachine)
        {
            base.StartState(stateMachine);

            stateMachine.SwitchState(EnemyStateType.LastState);
        }
    }
}
