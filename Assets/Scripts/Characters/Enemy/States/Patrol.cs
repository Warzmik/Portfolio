using System.Collections;
using UnityEngine;

namespace Characters.Enemy.States
{
    public class Patrol : EnemyState
    {
        [SerializeField] private EnemyData enemyData;
        [field: SerializeField] public float patrolRadius { get; private set; }

        private new Rigidbody rigidbody;
        private LayerMask layerMask;
        private Vector3[] waypoints = new Vector3[5];
        private int instanceId;


        private void Awake()
        {
            layerMask = LayerMask.GetMask("Enemy");
            instanceId = gameObject.GetInstanceID();
        }


        private void OnEnable()
        {
            rigidbody = GetComponent<Rigidbody>();

            RandomWaypoints();
        }


        private IEnumerator MoveToWaypoint()
        {
            bool isInDestination = false;

            Vector3 waypoint = waypoints[Random.Range(0, waypoints.Length)];
            waypoint.y = 0f;
            Vector3 direction = (waypoint - transform.position).normalized;
            direction.y = 0f;

            animator.SetBool("Idle", false);
            animator.SetBool("Move", true);

            while (!isInDestination)
            {
                RaycastHit hit;

                rigidbody.linearVelocity = direction * enemyData.moveForce;
                transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

                if (Physics.Raycast(waypoint, Vector3.up, out hit, Mathf.Infinity, layerMask))
                {
                    if (hit.collider.gameObject.GetInstanceID() != instanceId) continue;

                    animator.SetBool("Idle", true);
                    animator.SetBool("Move", false);

                    isInDestination = true;

                    yield return new WaitForSeconds(Random.Range(2f, 3f));
                }

                yield return null;
            }

            StartCoroutine("MoveToWaypoint");
        }


        public override void StartState(EnemyStateMachine stateMachine)
        {
            base.StartState(stateMachine);

            StartCoroutine("MoveToWaypoint");
        }


        public override void ExitState(EnemyStateMachine stateMachine)
        {
            base.ExitState(stateMachine);

            StopAllCoroutines();

            animator.SetBool("Move", false);
            animator.SetBool("Idle", false);
        }


        private void RandomWaypoints()
        {
            for (int i = 0; i < waypoints.Length; i++)
            {
                Vector3 randomPosition = transform.position + Random.insideUnitSphere * patrolRadius;
                randomPosition.y = 0f;

                waypoints[i] = randomPosition;
            }
        }
    }
}

