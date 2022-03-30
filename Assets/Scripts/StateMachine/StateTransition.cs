using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTransition 
{
    public State nextState = null;
    public List<StateTransitionCondition> conditions = new List<StateTransitionCondition>();

    public State NextState => nextState;

    public bool ShouldTransition()
    {
        foreach (var condition in conditions)
        {
            if(!condition.isMet())
            {
                return false;
            }
        }

        return true;
    }
}

