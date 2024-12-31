using Characters.Enemy.States;
using Characters.Player;
using UnityEditorInternal;
using UnityEngine;

namespace Characters.Enemy
{
    [RequireComponent(typeof(EnemyStateMachine))]
    public class CheckHit : MonoBehaviour, IDamageable
    {
        [SerializeField] private Animator animator;
        [SerializeField] private EnemyData enemyData;

        private EnemyStateMachine stateMachine;

        public float life { get ; private set; }


        private void Awake()
        {
            stateMachine = GetComponent<EnemyStateMachine>();
            life = enemyData.life;
        }


        public void Hit(float attackForce)
        {
            animator.SetTrigger("Hit");

            life -= attackForce;

            if (life <= 0)
            {
                stateMachine.SwitchState(EnemyStateType.Die);
            }
        }
    }
}
