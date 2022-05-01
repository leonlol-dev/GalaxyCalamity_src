using UnityEngine;

public abstract class FlyingBotBaseState
{
    public abstract void Start(FlyingBotStateMachine fbot);
    public abstract void EnterState(FlyingBotStateMachine fbot);
    public abstract void UpdateState(FlyingBotStateMachine fbot);
    public abstract void FixedUpdateState(FlyingBotStateMachine fbot);
    public abstract void OnCollisionEnter(FlyingBotStateMachine fbot, Collider collider);
    public abstract void OnTriggerEnter(FlyingBotStateMachine fbot, Collider other);
    public abstract void OnTriggerExit(FlyingBotStateMachine fbot, Collider other);
}

