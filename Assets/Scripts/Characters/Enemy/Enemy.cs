using Characters.Enemy.States;
using UnityEngine;

namespace Characters.Enemy 
{
    public class EnemyController : MonoBehaviour
    {
        private EnemyStateMachine stateMachine;


        private void Awake()
        {
            stateMachine = GetComponent<EnemyStateMachine>();
        }


        public void InRange(bool inRange)
        {
            if(inRange)
            {
                stateMachine.SwitchState(EnemyStateType.Combat);
            }
            else
            {
                stateMachine.SwitchState(EnemyStateType.LastState);
            }
        }


        public void SetTarget(bool isTarget)
        {

        }
    }
}