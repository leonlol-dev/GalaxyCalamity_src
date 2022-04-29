using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WormBaseState
{
    public abstract void Start(WormStateMachine worm);
    public abstract void EnterState(WormStateMachine worm);
    public abstract void UpdateState(WormStateMachine worm);
    public abstract void FixedUpdateState(WormStateMachine worm);
    public abstract void OnTriggerEnter(WormStateMachine worm, Collider collider);
    

}
