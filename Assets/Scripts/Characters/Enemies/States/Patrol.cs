using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent( typeof(NavMeshAgent) )]
public class Patrol : MonoBehaviour, IEnemyState
{
    [SerializeField] private Vector3[] waypoints;
    [Space]
    public UnityEvent onWaypoint;
    public UnityEvent onStateIsActivated;
    
    public string stateName { private set; get; }
    public bool isActive { private set; get; }

    private NavMeshAgent navMeshAgent;
    private int waypointIndex;


    private void Awake()
    {
        stateName = "patrol";

        navMeshAgent = GetComponent<NavMeshAgent>();
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
        float range = 10f;

        waypoints = new Vector3[5];

        for (int i = 0; i < 5; i++)
        {
            Vector3 randDirection = Random.insideUnitSphere * range;
            randDirection += origin;

            NavMeshHit navHit;
            NavMesh.SamplePosition (randDirection, out navHit, range, -1);

            waypoints[i] = navHit.position;
        }
    }
}
