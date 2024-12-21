using UnityEngine;
using UnityEngine.Events;

namespace Characters.Enemy.States
{
    public class Hit : EnemyState
    {
        public UnityEvent onHit;


        public override void StartState(EnemyStateMachine stateMachine)
        {
            base.StartState(stateMachine);

            onHit?.Invoke();
        }
    }
}
