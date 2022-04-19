using UnityEngine;

[System.Serializable]
public abstract class TurretBaseState
{
    public abstract void Start(TurretStateMachine turret);
    public abstract void EnterState(TurretStateMachine turret);
    public abstract void UpdateState(TurretStateMachine turret);
    public abstract void FixedUpdateState(TurretStateMachine turret);
    public abstract void OnCollisionEnter(TurretStateMachine turret, Collider collider);
    public abstract void OnTriggerEnter(TurretStateMachine turret, Collider other);
    public abstract void OnTriggerExit(TurretStateMachine turret, Collider other);
}
