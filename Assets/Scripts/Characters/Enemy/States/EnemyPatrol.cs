using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Characters.Enemy 
{
    [RequireComponent( typeof(NavMeshAgent) )]
    public class EnemyPatrol : MonoBehaviour, IEnemyState
    {
        [SerializeField] private Vector3[] waypoints;
        [SerializeField] private float movementRange = 10f;
        [Space]
        public UnityEvent onWaypoint;
        public UnityEvent onStateIsActivated;
        
        public string stateName { private set; get; }
        public bool isActive { private set; get; }

        private NavMeshAgent navMeshAgent;
        private int waypointIndex;
        private Vector3 initialPosition;


        private void Awake()
        {
            stateName = "patrol";

            navMeshAgent = GetComponent<NavMeshAgent>();
        }


        private void OnEnable()
        {
            initialPosition = transform.position;
        }


        private void Update()
        {
            if (!isActive) return;

            if (navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete && navMeshAgent.remainingDistance < 0.1)
            {
                waypointIndex ++;
                StartCoroutine(MoveToWaypoint());
            }
        }


        private IEnumerator MoveToWaypoint()
        {
            if (waypointIndex >= waypoints.Length)
            {
                waypointIndex = 0;
            }

            Vector3 point = waypoints[waypointIndex];

            navMeshAgent.destination = point;

            yield return null;
        }


        public void ActivateState()
        {
            isActive = true;
            onStateIsActivated?.Invoke();

            RandomizeWaypoints();
            StartCoroutine(MoveToWaypoint());
        }


        public void DesactivateState()
        {
            isActive = false;

            StopAllCoroutines();
        }


        // Mehtod for randomize the waypoints
        private void RandomizeWaypoints() 
        {
            Vector3 origin = transform.position;

            waypoints = new Vector3[5];

            for (int i = 0; i < 5; i++)
            {
                Vector3 randDirection = Random.insideUnitSphere * movementRange;
                randDirection += origin;

                NavMeshHit navHit;
                NavMesh.SamplePosition (randDirection, out navHit, movementRange, -1);

                waypoints[i] = navHit.position;
            }
        }


        // Draw a circle in the editor, show the movement range
        private void OnDrawGizmos()
        {
            if (isActive)
            {
                Handles.DrawWireDisc(initialPosition ,Vector3.up, movementRange);
            }
            else
            {
                Handles.DrawWireDisc(transform.position ,Vector3.up, movementRange);
            }
        }
    }
}

