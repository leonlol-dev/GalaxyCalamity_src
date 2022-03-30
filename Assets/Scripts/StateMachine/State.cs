using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class State : MonoBehaviour, IState
{

    public List<StateTransition> transitions = new List<StateTransition>();
    public UnityEvent onEnter = new UnityEvent();
    public UnityEvent onExit = new UnityEvent();

    public IState ProcessTransitions()
    {
        foreach (var transition in transitions)
        {
            if(transition.ShouldTransition())
            {
                return transition.NextState;
            }
        }

        return null;
    }

    public void Enter() => gameObject.SetActive(true);
    public void Exit() => gameObject.SetActive(false);
}
