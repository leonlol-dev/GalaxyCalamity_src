using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{

    public StateMachine(IState startingState) => ChangeState(startingState);

    public IState CurrentState
    {
        get;
        private set;
    }

    public void ChangeState (IState state)
    {
        //Exit Current State
        CurrentState?.Exit();

        //Set new State
        CurrentState = state;

        //Enter new state
        CurrentState?.Enter();
    }

    public void Tick()
    {
        //Get the new state if the conditions are met to make a transition.
        IState nextState = CurrentState.ProcessTransitions();

        if (nextState != null)
        {
            ChangeState(nextState);
        }
    }
}

    
