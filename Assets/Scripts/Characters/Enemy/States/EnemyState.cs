using UnityEngine;
using UnityEngine.Events;

namespace Characters.Enemy.States
{
    public class EnemyState : MonoBehaviour
    {
        [SerializeField] protected Animator animator;
        [field:SerializeField] public EnemyStateType stateType {  get; private set; }
        public bool isActive { get; private set; }

        [Space]

        public UnityEvent onStateStart;
        public UnityEvent onStateEnd;


        public virtual void StartState(EnemyStateMachine stateMachine) 
        {
            isActive = true;
            onStateStart?.Invoke();
        }


        public virtual void ExitState(EnemyStateMachine stateMachine) 
        { 
            isActive = false;
            onStateEnd?.Invoke();
        }
    }
}

