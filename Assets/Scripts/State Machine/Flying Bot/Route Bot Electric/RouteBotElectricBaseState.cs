using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RouteBotElectricBaseState 
{
    public abstract void Start(RouteBotElectricStateMachine rbot);
    public abstract void EnterState(RouteBotElectricStateMachine rbot);
    public abstract void UpdateState(RouteBotElectricStateMachine rbot);
}
