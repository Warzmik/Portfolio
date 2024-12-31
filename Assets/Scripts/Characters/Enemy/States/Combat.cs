using System.Collections;
using UnityEngine;
using Characters.Player;

namespace Characters.Enemy.States
{
    public class Combat : EnemyState
    {
        [SerializeField] private PlayerData playerData;
        [SerializeField] private EnemyData enemyData;

        private new Rigidbody rigidbody;
        private bool isAttacking;
        private LayerMask layerMask;


        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            layerMask = LayerMask.GetMask("Enemy");
        }


        public override void StartState(EnemyStateMachine stateMachine)
        {
            base.StartState(stateMachine);

            animator.SetBool("Combat", true);
            StartCoroutine("CombatMovement");
            StartCoroutine("AttackProbability");
        }


        public override void ExitState(EnemyStateMachine stateMachine)
        {
            base.ExitState(stateMachine);

            animator.SetBool("Combat", false);
            StopAllCoroutines();
        }


        private void Update()
        {
            if (!isActive) return;

            Vector3 playerPosition = playerData.currentPosition;
            playerPosition.y = 1f;

            transform.LookAt(playerPosition);
        }


        private Vector3 RandomPosition(Vector3 center, float radius)
        {
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

            float x = center.x + radius * Mathf.Cos(angle);
            float z = center.z + radius * Mathf.Sin(angle);

            return new Vector3(x, center.y, z);
        }


        private IEnumerator CombatMovement()
        {
            bool isInDestination = false;
            Vector3 position = RandomPosition(playerData.currentPosition, 3);
            Vector3 direction = (position - transform.position).normalized;

            animator.SetBool("Move", true);

            while (!isInDestination)
            {
                RaycastHit hit;

                rigidbody.linearVelocity = direction * enemyData.moveForce;

                if (Physics.Raycast(position, Vector3.up, out hit, Mathf.Infinity, layerMask))
                {
                    if (hit.collider.gameObject.GetInstanceID() != gameObject.GetInstanceID()) continue;

                    isInDestination = true;

                    animator.SetBool("Move", false);
                }

                yield return null;
            }

            yield return new WaitForSeconds(Random.Range(0f, 1f));

            StartCoroutine("CombatMovement");
        }



        private IEnumerator AttackProbability()
        {
            float probabilityValue = Random.Range(0f, 1f);

            if (probabilityValue > 0.5f)
            {
                isAttacking = true;

                animator.SetTrigger("Attack");
            }

            yield return new WaitForSeconds(Random.Range(1f, 2f));

            isAttacking = false;

            StartCoroutine("AttackProbability");
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageable damageable) && isAttacking)
            {
                damageable.Hit(enemyData.attackForce);
            }
        }
    }
}
