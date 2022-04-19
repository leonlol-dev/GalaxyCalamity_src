using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlyBotBaseState 
{
    public abstract void Start(FlyBotStateMachineScripted fbot);
    public abstract void EnterState(FlyBotStateMachineScripted fbot);
    public abstract void UpdateState(FlyBotStateMachineScripted fbot);
    public abstract void FixedUpdateState(FlyBotStateMachineScripted fbot);
    public abstract void OnCollisionEnter(FlyBotStateMachineScripted fbot, Collider collider);
    public abstract void OnTriggerEnter(FlyBotStateMachineScripted fbot, Collider other);
    public abstract void OnTriggerExit(FlyBotStateMachineScripted   fbot, Collider other);
}
