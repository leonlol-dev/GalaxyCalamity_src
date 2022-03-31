using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineBehaviour : MonoBehaviour
{

    public State startingState = null;

    private StateMachine stateMachine;
    private StateMachine StateMachine
    {
        get
        {
            if (stateMachine != null)
            {
                return stateMachine;
            }
            stateMachine = new StateMachine(startingState);
            return stateMachine;
        }
    }

    private void Update() => StateMachine.Tick();
    public void ChangeState(State state) => StateMachine.ChangeState(state);

}

