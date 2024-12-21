using UnityEngine;
using Characters.Enemy.States;

namespace Characters.Enemy 
{
    [RequireComponent(typeof(EnemyStateMachine))]
    public class Enemy : MonoBehaviour, IEnemy
    {
        private EnemyStateMachine stateMachine;


        private void Awake()
        { 
            stateMachine = GetComponent<EnemyStateMachine>();
        }


        public void InRange(bool inRange)
        {

        }


        public void SetTarget(bool isTarget)
        {

        }

        
        public void Hit(float attackForce)
        {
            stateMachine.SwitchState(EnemyStateType.Hit);
        }


        public Transform GetTransform()
        {
            return transform;
        }
    }
}