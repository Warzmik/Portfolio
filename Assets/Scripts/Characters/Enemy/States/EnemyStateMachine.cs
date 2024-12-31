using System.Collections.Generic;
using UnityEngine;

namespace Characters.Enemy.States
{
    public class EnemyStateMachine : MonoBehaviour
    {
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

            currentstate = EnemyStateType.Init;
            lastState = currentstate;
        }


        private void Start()
        {
            SwitchState(EnemyStateType.Init);
        }


        public void SwitchState(EnemyStateType stateType) 
        {
            if (stateType == EnemyStateType.LastState)
            {
                statesList[currentstate].ExitState(this);
                currentstate = lastState;
                statesList[lastState].StartState(this);
            }
            else
            {
                statesList[currentstate].ExitState(this);
                lastState = currentstate;
                currentstate = stateType;
                statesList[currentstate].StartState(this);
            }
        } 
    }
}
