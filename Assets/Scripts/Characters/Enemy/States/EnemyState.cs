using UnityEngine;

namespace Characters.Enemy.States
{
    public class EnemyState : MonoBehaviour
    {
        [field:SerializeField] public EnemyStateType stateType {  get; private set; }
        public bool isActive { get; private set; }


        public virtual void StartState(EnemyStateMachine stateMachine) 
        {
            isActive = true;
        }


        public virtual void ExitState(EnemyStateMachine stateMachine) 
        { 
            isActive = false;
        }
    }
}

