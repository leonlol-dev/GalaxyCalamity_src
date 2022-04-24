using UnityEngine;

public abstract class SpiderBaseState
{
    public abstract void Start(SpiderStateMachine spider);
    public abstract void EnterState(SpiderStateMachine spider);
    public abstract void UpdateState(SpiderStateMachine spider);
    public abstract void FixedUpdateState(SpiderStateMachine spider);
    public abstract void OnTriggerEnter(SpiderStateMachine spider, Collider collider);

}
