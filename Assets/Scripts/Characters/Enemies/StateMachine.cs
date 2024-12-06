using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private string initialState;

    private Dictionary<string, IEnemyState> states = new Dictionary<string, IEnemyState>();


    private void Start()
    {
        // Get states
        IEnemyState[] statesArray = GetComponents<IEnemyState>();

        foreach (IEnemyState state in statesArray)
        {
            states.Add(state.stateName, state); // Add states to dictionary
        }

        ChangeState(initialState);
    }


    public void ChangeState(string stateName)
    {
        if (!states.ContainsKey(stateName)) return;

        gameObject.SendMessage("DesactivateState");

        if (stateName == "initial")
        {
            states[initialState].ActivateState();
        }
        else
        {
            states[stateName].ActivateState();
        }
    }
}
