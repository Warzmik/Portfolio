using System.Collections.Generic;
using UnityEngine;

namespace Characters.Enemy.States
{
    public class EnemyStateMachine : MonoBehaviour
    {
        [SerializeField] private EnemyStateType initialState;
        [SerializeField] private Animator animator;

        private Dictionary<EnemyStateType, EnemyState> statesList = new Dictionary<EnemyStateType, EnemyState>();
        private EnemyStateType currentstate;
        private EnemyStateType lastState;


        private void Awake()
        {
            EnemyState[] allStates = GetComponents<EnemyState>();

            foreach (EnemyState state in allStates)
            {
                statesList.Add(state.stateType, state);
            }

            currentstate = initialState;
            lastState = currentstate;
        }


        private void Start()
        {
            SwitchState(initialState);
        }


        public void SwitchState(EnemyStateType stateType) 
        {
            statesList[currentstate].ExitState(this);
            lastState = currentstate;
            currentstate = stateType;
            statesList[currentstate].StartState(this);

            animator.SetTrigger(stateType.ToString());
        } 
    }
}
