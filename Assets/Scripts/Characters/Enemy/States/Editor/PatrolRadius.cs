using UnityEditor;
using UnityEngine;
using Characters.Enemy.States;


[CustomEditor(typeof(Patrol))]
public class PatrolRadius : Editor
{
    private bool once;
    private Vector3 componentPosition = Vector3.zero;


    private void OnSceneGUI()
    {
        Patrol component = (Patrol) target;
        
        if (!Application.isPlaying)
        {
            componentPosition = component.transform.position;
            once = false;
        }
        else
        {
            if (!once)
            {
                componentPosition = component.transform.position;
                once = true;
            }
        }

        componentPosition.y = 0f;

        Handles.color = Color.red;
        Handles.DrawWireDisc(componentPosition, Vector3.up, component.patrolRadius);
    }
}


