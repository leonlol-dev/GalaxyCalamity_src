using UnityEngine;

public abstract class TurretBaseState
{
    public abstract void EnterState(TurretStateMachine turret);
    public abstract void UpdateState(TurretStateMachine turret);
    public abstract void OnCollisionEnter(TurretStateMachine turret, Collider collider);
    public abstract void OnTriggerEnter(TurretStateMachine turret, Collider other);
}
